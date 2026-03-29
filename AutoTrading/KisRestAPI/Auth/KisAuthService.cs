using KisRestAPI.Configuration;
using KisRestAPI.Common;
using KisRestAPI.Common.Http;
using KisRestAPI.Models.Auth;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace KisRestAPI.Auth
{
    /// <summary>
    /// 한국투자증권 API 인증 서비스 클래스
    ///
    /// 왜 mode-specific 오버로드가 필요한가?
    /// - 기존에는 _kisTradingService.CurrentEnvironment를 읽어 환경을 결정했다.
    /// - 이 전역 상태는 토큰 갱신 중 다른 스레드에서 바뀔 수 있어 위험하다.
    /// - mode 파라미터를 명시적으로 전달하면 전역 상태 변경 없이 안전하게 동작한다.
    /// </summary>
    public class KisAuthService : IAuthService
    {
        /// <summary>HTTP 통신 전용 객체</summary>
        private readonly HttpClient _httpClient;

        /// <summary>모의/실전 환경별 API 접속 정보</summary>
        private readonly ApiSettings _settings;

        /// <summary>현재 활성 거래 모드(Mock / Live)를 관리하는 서비스</summary>
        private readonly IKisTradingService _kisTradingService;

        // ===== Mock/Live 토큰을 환경별로 분리 캐시 =====
        private readonly Dictionary<KisTradingMode, TokenResponse> _tokenCache = new();

        public KisAuthService(HttpClient httpClient, ApiSettings settings, IKisTradingService kisTradingService)
        {
            _httpClient = httpClient;
            _settings = settings;
            _kisTradingService = kisTradingService;
        }

        /// <summary>
        /// 동시에 여러 요청이 들어도 토큰을 한 번만 발급하도록 잠금
        /// </summary>
        private readonly SemaphoreSlim _tokenLock = new(1, 1);

        /// <summary>공통 JSON 옵션</summary>
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        // =====================================================================
        // ===== 토큰 발급 =====
        // =====================================================================

        /// <summary>
        /// 현재 전역 환경 기준으로 토큰 발급 또는 재사용 (기존 호환)
        /// </summary>
        public async Task<TokenResponse?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            return await GetAccessTokenAsync(_kisTradingService.CurrentEnvironment, cancellationToken);
        }

        /// <summary>
        /// 특정 환경(Mock/Live)의 토큰을 발급/반환한다.
        /// 전역 환경을 변경하지 않으므로 동시성 안전하다.
        ///
        /// 왜 mode를 직접 받는가?
        /// - RefreshAllTokensAsync 등에서 환경을 순회하며 갱신할 때
        ///   전역 상태를 바꾸지 않고도 각 환경의 토큰을 안전하게 갱신할 수 있다.
        /// </summary>
        public async Task<TokenResponse?> GetAccessTokenAsync(KisTradingMode mode, CancellationToken cancellationToken = default)
        {
            // 1) 해당 환경의 캐시된 토큰이 유효하면 바로 반환
            if (_tokenCache.TryGetValue(mode, out TokenResponse? cached) && IsTokenValid(cached))
            {
                return cached;
            }

            // 2) 동시에 여러 스레드가 토큰 요청을 해도 한 번만 발급
            await _tokenLock.WaitAsync(cancellationToken);

            try
            {
                // 락 대기 중 다른 작업이 이미 토큰을 발급했을 수 있으므로 다시 검사
                if (_tokenCache.TryGetValue(mode, out cached) && IsTokenValid(cached))
                {
                    return cached;
                }

                ApiEndpointSettings modeSettings = _settings.GetCurrent(mode);

                string appKey = modeSettings.AppKey.Trim();
                string appSecret = modeSettings.AppSecret.Trim();
                string baseUrl = NormalizeBaseUrl(modeSettings.BaseUrl);

                var requestBody = new TokenRequest
                {
                    GrantType = "client_credentials",
                    AppKey = appKey,
                    AppSecret = appSecret
                };

                string requestUrl = $"{baseUrl}/oauth2/tokenP";

                using var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
                {
                    Content = CreateJsonContent(requestBody)
                };

                using var response = await _httpClient.SendAsync(request, cancellationToken);
                string responseText = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    throw BuildKisException("토큰 발급 실패", requestUrl, (int)response.StatusCode, responseText);
                }

                var token = JsonSerializer.Deserialize<TokenResponse>(responseText, _jsonOptions);

                // access_token이 빈값이면 정상 토큰 응답이 아니다.
                if (token == null || string.IsNullOrWhiteSpace(token.AccessToken))
                {
                    throw BuildKisException("토큰 응답 파싱 실패", requestUrl, (int)response.StatusCode, responseText);
                }

                // ===== 해당 환경 키로만 저장 → 다른 환경 토큰을 덮어쓰지 않는다 =====
                _tokenCache[mode] = token;
                return token;
            }
            finally
            {
                _tokenLock.Release();
            }
        }

        // =====================================================================
        // ===== HashKey 생성 =====
        // =====================================================================

        /// <summary>
        /// 현재 전역 환경 기준으로 HashKey 생성 (기존 호환)
        /// </summary>
        public async Task<string> GetHashKeyAsync<T>(T requestBody, CancellationToken cancellationToken = default)
        {
            return await GetHashKeyAsync(requestBody, _kisTradingService.CurrentEnvironment, cancellationToken);
        }

        /// <summary>
        /// 특정 환경(Mock/Live) 기준으로 HashKey를 생성한다.
        ///
        /// 왜 mode를 직접 받는가?
        /// - 주문 서비스에서 CaptureContext()로 스냅샷된 환경 기준으로
        ///   HashKey를 생성해야 안전하다.
        /// - 전역 환경이 바뀌어도 이미 진행 중인 주문에 영향이 없다.
        /// </summary>
        public async Task<string> GetHashKeyAsync<T>(T requestBody, KisTradingMode mode, CancellationToken cancellationToken = default)
        {
            if (requestBody == null)
            {
                throw new ArgumentNullException(nameof(requestBody), "HashKey를 위한 요청 Body가 null 입니다.");
            }

            ApiEndpointSettings modeSettings = _settings.GetCurrent(mode);

            string baseUrl = NormalizeBaseUrl(modeSettings.BaseUrl);
            string appKey = modeSettings.AppKey.Trim();
            string appSecret = modeSettings.AppSecret.Trim();
            string requestUrl = $"{baseUrl}/uapi/hashkey";

            using var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = CreateJsonContent(requestBody)
            };

            request.Headers.TryAddWithoutValidation("appkey", appKey);
            request.Headers.TryAddWithoutValidation("appsecret", appSecret);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json")
            {
                CharSet = "utf-8"
            };

            using var response = await _httpClient.SendAsync(request, cancellationToken);
            string responseText = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw BuildKisException("HashKey 발급 실패", requestUrl, (int)response.StatusCode, responseText);
            }

            var hashResponse = JsonSerializer.Deserialize<HashKeyResponse>(responseText, _jsonOptions);

            if (hashResponse == null || string.IsNullOrWhiteSpace(hashResponse.Hash))
            {
                throw BuildKisException("HashKey 응답 파싱 실패", requestUrl, (int)response.StatusCode, responseText);
            }

            return hashResponse.Hash;
        }

        /// <summary>
        /// 캐시된 토큰을 강제로 무효화한다.
        /// </summary>
        public void InvalidateToken()
        {
            _tokenCache.Remove(_kisTradingService.CurrentEnvironment);
        }

        // =====================================================================
        // ===== WebSocket Approval Key 발급 =====
        // =====================================================================

        /// <summary>환경별 Approval Key 캐시</summary>
        private readonly Dictionary<KisTradingMode, string> _approvalKeyCache = new();

        /// <summary>
        /// 현재 전역 환경 기준으로 Approval Key 발급/반환
        /// </summary>
        public async Task<string> GetApprovalKeyAsync(CancellationToken cancellationToken = default)
        {
            return await GetApprovalKeyAsync(_kisTradingService.CurrentEnvironment, cancellationToken);
        }

        /// <summary>
        /// 특정 환경(Mock/Live)의 Approval Key를 발급/반환한다.
        ///
        /// 왜 별도 캐시인가?
        /// - Approval Key는 Access Token과 별개의 인증 키다.
        /// - WebSocket 접속 시에만 사용되며, 발급 엔드포인트도 다르다.
        /// - 주의: 요청 Body에서 "appsecret"이 아닌 "secretkey"를 사용한다.
        /// </summary>
        public async Task<string> GetApprovalKeyAsync(KisTradingMode mode, CancellationToken cancellationToken = default)
        {
            // 캐시된 키가 있으면 재사용
            if (_approvalKeyCache.TryGetValue(mode, out string? cached) &&
                !string.IsNullOrWhiteSpace(cached))
            {
                return cached;
            }

            ApiEndpointSettings modeSettings = _settings.GetCurrent(mode);

            string baseUrl = NormalizeBaseUrl(modeSettings.BaseUrl);
            string requestUrl = $"{baseUrl}/oauth2/Approval";

            var requestBody = new ApprovalKeyRequest
            {
                GrantType = "client_credentials",
                AppKey = modeSettings.AppKey.Trim(),
                SecretKey = modeSettings.AppSecret.Trim()
            };

            using var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = CreateJsonContent(requestBody)
            };

            using var response = await _httpClient.SendAsync(request, cancellationToken);
            string responseText = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw BuildKisException("Approval Key 발급 실패", requestUrl, (int)response.StatusCode, responseText);
            }

            var approvalResponse = JsonSerializer.Deserialize<ApprovalKeyResponse>(responseText, _jsonOptions);

            if (approvalResponse == null || string.IsNullOrWhiteSpace(approvalResponse.ApprovalKey))
            {
                throw BuildKisException("Approval Key 응답 파싱 실패", requestUrl, (int)response.StatusCode, responseText);
            }

            _approvalKeyCache[mode] = approvalResponse.ApprovalKey;
            return approvalResponse.ApprovalKey;
        }

        /// <summary>
        /// 해당 토큰의 유효여부 검사
        /// </summary>
        private bool IsTokenValid(TokenResponse? token)
        {
            if (token == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(token.AccessToken))
            {
                return false;
            }

            if (!DateTime.TryParse(token.AccessTokenExpired, out DateTime expiredAt))
            {
                return false;
            }

            return DateTime.Now < expiredAt.AddMinutes(-1);
        }

        private static string NormalizeBaseUrl(string baseUrl)
        {
            return baseUrl.Trim().TrimEnd('/');
        }

        private HttpContent CreateJsonContent<T>(T body)
        {
            string json = JsonSerializer.Serialize(body, _jsonOptions);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private Exception BuildKisException(string title, string requestUrl, int statusCode, string responseText)
        {
            var error = JsonSerializer.Deserialize<KisErrorResponse>(responseText, _jsonOptions);

            if (error != null &&
                (!string.IsNullOrWhiteSpace(error.ErrorCode) ||
                 !string.IsNullOrWhiteSpace(error.ErrorDescription) ||
                 !string.IsNullOrWhiteSpace(error.Message1)))
            {
                return new Exception(
                    $"{title} | HTTP={statusCode} | URL={requestUrl} | error_code={error.ErrorCode} | error_description={error.ErrorDescription} | msg_cd={error.MessageCode} | msg1={error.Message1}");
            }

            return new Exception(
                $"{title} | HTTP={statusCode} | URL={requestUrl} | Body={responseText}");
        }
    }
}
