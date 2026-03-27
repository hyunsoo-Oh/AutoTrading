/// <summary>
/// WebSocket 기반 한국투자증권 실시간 서비스의 공통 기반 클래스
///
/// 왜 이 클래스가 필요한가?
/// - 실시간 시세, 실시간 체결 통보, 실시간 호가 등
///   WebSocket 기반 기능이 모두 동일한 연결/재연결/구독/해제/파싱 흐름을 따른다.
/// - 이 공통 흐름을 한 곳에 모아서, 새 실시간 기능 추가 시
///   도메인 전용 파싱/이벤트만 구현하면 되도록 한다.
///
/// 공통 책임:
/// - WebSocket 연결 (ConnectAsync)
/// - 자동 재연결 (ReconnectAsync — 네트워크 끊김 등)
/// - 구독 요청 전송 (SubscribeAsync)
/// - 구독 해제 요청 전송 (UnsubscribeAsync)
/// - 수신 루프 (ReceiveLoopAsync — 메시지 읽기 + 도메인 파싱 호출)
/// - 연결 해제 (DisconnectAsync)
///
/// 도메인 서비스가 해야 할 것:
/// - 구독 메시지 포맷 생성 (BuildSubscribeMessage / BuildUnsubscribeMessage)
/// - 수신 메시지 파싱 및 이벤트 발행 (OnMessageReceived)
/// - 도메인 전용 이벤트 정의 (예: OnQuoteReceived, OnExecutionNotice 등)
/// </summary>
using AutoTrading.Configuration;
using AutoTrading.Services.KoreaInvest.Common.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace AutoTrading.Services.KoreaInvest.Common.WebSocket
{
    /// <summary>
    /// WebSocket 실시간 서비스의 공통 기반 — 모든 한국투자증권 WebSocket 서비스가 상속한다.
    /// </summary>
    public abstract class KisWebSocketServiceBase : IDisposable
    {
        // ===== 공통 처리 =====
        private ClientWebSocket? _webSocket;
        private CancellationTokenSource? _receiveCts;
        private Task? _receiveTask;
        private bool _disposed;

        protected readonly IKisTradingService KisTradingService;

        /// <summary>
        /// 공통 JSON 직렬화 옵션
        /// </summary>
        protected static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// WebSocket 수신 버퍼 크기
        ///
        /// 왜 8KB인가?
        /// - 한국투자증권 실시간 시세 메시지는 보통 수백 바이트이므로 8KB면 충분하다.
        /// - 너무 작으면 메시지가 분할되어 조립 로직이 필요해진다.
        /// </summary>
        protected virtual int ReceiveBufferSize => 8192;

        /// <summary>
        /// 재연결 시도 간격 (밀리초)
        ///
        /// 왜 3초인가?
        /// - 너무 짧으면 서버에 부담, 너무 길면 실시간성이 떨어진다.
        /// - 도메인 서비스에서 override하여 조정 가능하다.
        /// </summary>
        protected virtual int ReconnectDelayMs => 3000;

        /// <summary>
        /// 최대 재연결 시도 횟수 — 0이면 무한 재시도
        /// </summary>
        protected virtual int MaxReconnectAttempts => 10;

        /// <summary>
        /// 현재 WebSocket 연결 상태
        /// </summary>
        public bool IsConnected =>
            _webSocket?.State == WebSocketState.Open;

        // ===== 공통 처리: 이벤트 =====

        /// <summary>
        /// 연결 상태가 변경될 때 발생한다.
        /// UI에서 연결 상태 표시에 사용할 수 있다.
        /// </summary>
        public event EventHandler<bool>? ConnectionStateChanged;

        /// <summary>
        /// 에러가 발생했을 때 발생한다.
        /// UI에서 에러 표시/로깅에 사용할 수 있다.
        /// </summary>
        public event EventHandler<Exception>? ErrorOccurred;

        protected KisWebSocketServiceBase(IKisTradingService kisTradingService)
        {
            KisTradingService = kisTradingService ?? throw new ArgumentNullException(nameof(kisTradingService));
        }

        // ===== 공통 처리: 연결 =====

        /// <summary>
        /// WebSocket 서버에 연결한다.
        ///
        /// 왜 연결과 수신 루프를 분리하나?
        /// - 연결 성공 후 구독 메시지를 보내야 하므로,
        ///   Connect → Subscribe → ReceiveLoop 순서를 보장하기 위함이다.
        /// </summary>
        /// <param name="cancellationToken">취소 토큰</param>
        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            if (IsConnected)
            {
                return;
            }

            // 기존 연결이 남아있으면 정리한다
            await CleanupWebSocketAsync();

            _webSocket = new ClientWebSocket();

            // ===== 수정 포인트 =====
            // 한국투자증권 WebSocket 연결 시 추가 옵션이 필요하면 여기서 설정한다.
            // 예: _webSocket.Options.SetRequestHeader("appkey", ...);
            // ===== =====

            string wsUrl = BuildWebSocketUrl();
            await _webSocket.ConnectAsync(new Uri(wsUrl), cancellationToken);

            ConnectionStateChanged?.Invoke(this, true);

            // 수신 루프를 백그라운드에서 시작한다
            _receiveCts = new CancellationTokenSource();
            _receiveTask = Task.Run(() => ReceiveLoopAsync(_receiveCts.Token));
        }

        // ===== 공통 처리: 연결 해제 =====

        /// <summary>
        /// WebSocket 연결을 정상적으로 종료한다.
        /// </summary>
        public async Task DisconnectAsync()
        {
            _receiveCts?.Cancel();

            if (_webSocket?.State == WebSocketState.Open)
            {
                try
                {
                    await _webSocket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        "정상 종료",
                        CancellationToken.None);
                }
                catch
                {
                    // 이미 닫혀 있거나 에러 상태일 수 있으므로 무시한다
                }
            }

            // 수신 루프가 완전히 종료될 때까지 대기
            if (_receiveTask != null)
            {
                try
                {
                    await _receiveTask;
                }
                catch (OperationCanceledException)
                {
                    // 정상 취소이므로 무시
                }
            }

            await CleanupWebSocketAsync();

            ConnectionStateChanged?.Invoke(this, false);
        }

        // ===== 공통 처리: 구독 =====

        /// <summary>
        /// 특정 종목/채널에 대한 구독 요청을 보낸다.
        ///
        /// 도메인 서비스에서 BuildSubscribeMessage를 override하여
        /// 한국투자증권 문서에 맞는 구독 메시지 JSON을 만들어야 한다.
        /// </summary>
        /// <param name="trKey">구독 키 (예: 종목코드)</param>
        /// <param name="cancellationToken">취소 토큰</param>
        public async Task SubscribeAsync(string trKey, CancellationToken cancellationToken = default)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("WebSocket이 연결되지 않은 상태에서 구독할 수 없습니다.");
            }

            string message = BuildSubscribeMessage(trKey);
            await SendMessageAsync(message, cancellationToken);
        }

        /// <summary>
        /// 특정 종목/채널에 대한 구독 해제 요청을 보낸다.
        /// </summary>
        /// <param name="trKey">구독 해제할 키 (예: 종목코드)</param>
        /// <param name="cancellationToken">취소 토큰</param>
        public async Task UnsubscribeAsync(string trKey, CancellationToken cancellationToken = default)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("WebSocket이 연결되지 않은 상태에서 구독 해제할 수 없습니다.");
            }

            string message = BuildUnsubscribeMessage(trKey);
            await SendMessageAsync(message, cancellationToken);
        }

        // ===== 공통 처리: 메시지 전송 =====

        /// <summary>
        /// WebSocket으로 텍스트 메시지를 전송한다.
        /// </summary>
        protected async Task SendMessageAsync(string message, CancellationToken cancellationToken = default)
        {
            if (_webSocket?.State != WebSocketState.Open)
            {
                throw new InvalidOperationException("WebSocket이 열려있지 않습니다.");
            }

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await _webSocket.SendAsync(
                new ArraySegment<byte>(buffer),
                WebSocketMessageType.Text,
                endOfMessage: true,
                cancellationToken);
        }

        // ===== 공통 처리: 수신 루프 =====

        /// <summary>
        /// WebSocket 메시지를 지속적으로 수신하는 백그라운드 루프
        ///
        /// 왜 무한 루프인가?
        /// - WebSocket은 서버가 메시지를 푸시하므로, 계속 읽어야 한다.
        /// - CancellationToken으로 종료를 제어한다.
        /// </summary>
        private async Task ReceiveLoopAsync(CancellationToken cancellationToken)
        {
            var buffer = new byte[ReceiveBufferSize];

            try
            {
                while (!cancellationToken.IsCancellationRequested && _webSocket?.State == WebSocketState.Open)
                {
                    // ===== WebSocket 수신 파싱 =====
                    using var messageStream = new MemoryStream();

                    WebSocketReceiveResult result;
                    do
                    {
                        result = await _webSocket.ReceiveAsync(
                            new ArraySegment<byte>(buffer),
                            cancellationToken);

                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            // 서버가 연결을 닫았다
                            await HandleConnectionLostAsync(cancellationToken);
                            return;
                        }

                        messageStream.Write(buffer, 0, result.Count);
                    }
                    while (!result.EndOfMessage);

                    // 전체 메시지가 조립되면 도메인 파싱 호출
                    string messageText = Encoding.UTF8.GetString(
                        messageStream.ToArray(), 0, (int)messageStream.Length);

                    try
                    {
                        OnMessageReceived(messageText);
                    }
                    catch (Exception ex)
                    {
                        // 도메인 파싱 에러가 수신 루프를 죽이지 않도록 보호한다
                        ErrorOccurred?.Invoke(this, ex);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 정상 취소이므로 무시
            }
            catch (WebSocketException ex)
            {
                ErrorOccurred?.Invoke(this, ex);
                await HandleConnectionLostAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(this, ex);
            }
        }

        // ===== 공통 처리: 재연결 =====

        /// <summary>
        /// 연결이 끊어졌을 때 자동 재연결을 시도한다.
        ///
        /// 왜 지수 백오프가 아닌 고정 간격인가?
        /// - 한국투자증권 WebSocket은 보통 일시적 끊김이므로
        ///   단순 고정 간격으로도 충분하다.
        /// - 필요하면 도메인 서비스에서 override하여 백오프 전략을 적용할 수 있다.
        /// </summary>
        private async Task HandleConnectionLostAsync(CancellationToken cancellationToken)
        {
            ConnectionStateChanged?.Invoke(this, false);

            int attempt = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                attempt++;

                if (MaxReconnectAttempts > 0 && attempt > MaxReconnectAttempts)
                {
                    ErrorOccurred?.Invoke(this,
                        new InvalidOperationException(
                            $"WebSocket 재연결 {MaxReconnectAttempts}회 시도 실패 — 연결을 포기합니다."));
                    return;
                }

                try
                {
                    await Task.Delay(ReconnectDelayMs, cancellationToken);
                    await CleanupWebSocketAsync();

                    _webSocket = new ClientWebSocket();
                    string wsUrl = BuildWebSocketUrl();
                    await _webSocket.ConnectAsync(new Uri(wsUrl), cancellationToken);

                    ConnectionStateChanged?.Invoke(this, true);

                    // ===== 수정 포인트 =====
                    // 재연결 성공 후 기존 구독을 복원해야 한다면
                    // OnReconnected()를 override하여 처리한다.
                    // ===== =====
                    await OnReconnectedAsync(cancellationToken);

                    // 재연결 성공 — 수신 루프를 새로 시작한다
                    _receiveCts = new CancellationTokenSource();
                    _receiveTask = Task.Run(() => ReceiveLoopAsync(_receiveCts.Token));
                    return;
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    ErrorOccurred?.Invoke(this,
                        new InvalidOperationException(
                            $"WebSocket 재연결 시도 {attempt}회차 실패: {ex.Message}", ex));
                }
            }
        }

        // ===== 공통 처리: 리소스 정리 =====

        private async Task CleanupWebSocketAsync()
        {
            _receiveCts?.Cancel();
            _receiveCts?.Dispose();
            _receiveCts = null;

            if (_receiveTask != null)
            {
                try { await _receiveTask; } catch { }
                _receiveTask = null;
            }

            _webSocket?.Dispose();
            _webSocket = null;
        }

        // ===== 도메인 전용 처리: 하위 클래스에서 구현 =====

        /// <summary>
        /// WebSocket 연결 URL을 생성한다.
        ///
        /// 도메인 서비스에서 Mock/Live 환경에 맞는 WebSocket URL을 반환해야 한다.
        /// 예: wss://ops.koreainvestment.com:21000 (모의)
        ///     wss://ops.koreainvestment.com:29000 (실전)
        /// </summary>
        protected abstract string BuildWebSocketUrl();

        /// <summary>
        /// 구독 요청 메시지(JSON)를 생성한다.
        ///
        /// 한국투자증권 WebSocket 문서의 구독 요청 포맷에 맞게 구현해야 한다.
        /// </summary>
        /// <param name="trKey">구독 키 (예: 종목코드 "005930")</param>
        /// <returns>전송할 JSON 문자열</returns>
        protected abstract string BuildSubscribeMessage(string trKey);

        /// <summary>
        /// 구독 해제 요청 메시지(JSON)를 생성한다.
        /// </summary>
        /// <param name="trKey">해제할 키</param>
        /// <returns>전송할 JSON 문자열</returns>
        protected abstract string BuildUnsubscribeMessage(string trKey);

        /// <summary>
        /// 서버에서 수신한 메시지를 파싱한다.
        ///
        /// 도메인 서비스에서 메시지 타입을 판별하고,
        /// 적절한 이벤트를 발행하는 로직을 구현해야 한다.
        /// </summary>
        /// <param name="message">수신된 원문 메시지</param>
        protected abstract void OnMessageReceived(string message);

        /// <summary>
        /// 재연결 성공 후 호출된다.
        ///
        /// 기존 구독을 복원해야 하는 도메인 서비스에서 override한다.
        /// 기본 구현은 아무것도 하지 않는다.
        /// </summary>
        protected virtual Task OnReconnectedAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        // ===== 공통 처리: IDisposable =====

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _receiveCts?.Cancel();
                _receiveCts?.Dispose();
                _webSocket?.Dispose();
            }

            _disposed = true;
        }
    }
}
