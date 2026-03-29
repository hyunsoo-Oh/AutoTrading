using System.Net.WebSockets;
using System.Text;

namespace KisRestAPI.Common.WebSocket
{
    /// <summary>
    /// 한국투자증권 WebSocket 연결 관리 클래스
    ///
    /// 책임:
    /// - WebSocket 연결 / 해제
    /// - 텍스트 메시지 송신
    /// - 백그라운드 수신 루프 운영
    /// - 수신된 원문을 이벤트로 전달
    ///
    /// 메시지 라우팅과 구독 관리는 상위 서비스(KisRealtimeService)의 책임이다.
    /// </summary>
    internal sealed class KisWebSocketClient : IDisposable
    {
        private ClientWebSocket? _ws;
        private CancellationTokenSource? _receiveCts;
        private Task? _receiveTask;

        /// <summary>수신 버퍼 크기 (16KB — 체결 데이터 여러 건이 올 수 있으므로 여유 있게)</summary>
        private const int BufferSize = 16 * 1024;

        // ===== 이벤트 =====

        /// <summary>WebSocket으로부터 텍스트 메시지를 수신했을 때 발생</summary>
        public event Action<string>? MessageReceived;

        /// <summary>연결이 끊어졌을 때 발생 (재연결 판단은 상위 서비스의 책임)</summary>
        public event Action? Disconnected;

        // ===== 상태 =====

        public bool IsConnected =>
            _ws?.State == WebSocketState.Open;

        // =====================================================================
        // ===== 연결 =====
        // =====================================================================

        public async Task ConnectAsync(string url, CancellationToken cancellationToken = default)
        {
            if (IsConnected)
                return;

            _ws?.Dispose();
            _ws = new ClientWebSocket();

            await _ws.ConnectAsync(new Uri(url), cancellationToken);

            // ===== 백그라운드 수신 루프 시작 =====
            _receiveCts = new CancellationTokenSource();
            _receiveTask = Task.Run(() => ReceiveLoopAsync(_receiveCts.Token));
        }

        // =====================================================================
        // ===== 송신 =====
        // =====================================================================

        public async Task SendAsync(string message, CancellationToken cancellationToken = default)
        {
            if (_ws == null || _ws.State != WebSocketState.Open)
                throw new InvalidOperationException("WebSocket이 연결되지 않은 상태에서 송신할 수 없습니다.");

            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await _ws.SendAsync(
                new ArraySegment<byte>(bytes),
                WebSocketMessageType.Text,
                endOfMessage: true,
                cancellationToken);
        }

        // =====================================================================
        // ===== 수신 루프 =====
        // =====================================================================

        private async Task ReceiveLoopAsync(CancellationToken ct)
        {
            var buffer = new byte[BufferSize];
            var messageBuilder = new StringBuilder();

            try
            {
                while (!ct.IsCancellationRequested && _ws?.State == WebSocketState.Open)
                {
                    var segment = new ArraySegment<byte>(buffer);
                    WebSocketReceiveResult result = await _ws.ReceiveAsync(segment, ct);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        // ===== 서버가 연결 종료를 요청 =====
                        try
                        {
                            await _ws.CloseOutputAsync(
                                WebSocketCloseStatus.NormalClosure, "서버 종료", CancellationToken.None);
                        }
                        catch { /* 이미 닫힌 경우 무시 */ }
                        break;
                    }

                    messageBuilder.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));

                    if (result.EndOfMessage)
                    {
                        string message = messageBuilder.ToString();
                        messageBuilder.Clear();

                        // ===== 상위 서비스로 전달 =====
                        MessageReceived?.Invoke(message);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 정상 종료 — 무시
            }
            catch (WebSocketException)
            {
                // 연결 끊김 — Disconnected 이벤트로 알림
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WS] 수신 루프 오류: {ex.Message}");
            }
            finally
            {
                Disconnected?.Invoke();
            }
        }

        // =====================================================================
        // ===== 종료 =====
        // =====================================================================

        public async Task CloseAsync()
        {
            // ===== 수신 루프 취소 =====
            _receiveCts?.Cancel();

            // ===== WebSocket 정상 종료 =====
            if (_ws?.State == WebSocketState.Open)
            {
                try
                {
                    using var timeoutCts = new CancellationTokenSource(3000);
                    await _ws.CloseAsync(
                        WebSocketCloseStatus.NormalClosure, "클라이언트 종료", timeoutCts.Token);
                }
                catch
                {
                    // 이미 닫혔거나 타임아웃 → 무시
                }
            }

            // ===== 수신 루프 완료 대기 =====
            if (_receiveTask != null)
            {
                try { await _receiveTask; }
                catch { /* 무시 */ }
            }
        }

        public void Dispose()
        {
            _receiveCts?.Cancel();
            _receiveCts?.Dispose();
            _ws?.Dispose();
        }
    }
}
