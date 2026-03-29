using AutoTrading.Configuration;
using AutoTrading.Features.Models.Api.Auth;
using AutoTrading.Services.KoreaInvest.Common;
using AutoTrading.Services.KoreaInvest.Common.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoTrading.Services.KoreaInvest.Auth
{
    /// <summary>
    /// 한국투자증권 API 인증 서비스 클래스
    /// </summary>
    public class KisAuthService : IAuthService
    {
        /// <summary>
        /// HTTP 통신 전담 객체
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// 현재 선택된 환경(모의 or 실전)의 설정값
        /// BaseUrl, AppKey, AppSecret이 들어있다.
        /// </summary>
        private readonly ApiSettings _settings;

        /// <summary>
        /// 현재 사용 중인 거래 모드(Mock / Live)를 관리하는 서비스
        /// </summary>
        private readonly IKisTradingService _kisTradingService;

        /// <summary>
        /// 마지막으로 받은 토큰을 잠시 저장해두기 위한 필드
        /// 
        /// 현재 예제에서는 단순 보관만 하고,
        /// 이후 단계에서 만료시간 비교까지 붙이면 더 완성도가 높아진다.
        /// </summary>
        // ===== Mock/Live 토큰을 환경별로 독립 캐싱 =====
        // 단일 _cachedToken으로 관리하면 환경 전환 시 기존 토큰을 버리고 재발급해야 한다.
        // 6시간 이내 재발급 제한에 걸리므로, 환경마다 토큰을 따로 보관해야 한다.
        // 전환해도 이미 받은 토큰이 살아있으면 그대로 재사용한다.
        // ===== =====
        private readonly Dictionary<KisTradingMode, TokenResponse> _tokenCache = new();

        public KisAuthService(HttpClient httpClient, ApiSettings settings, IKisTradingService kisTradingService)
        {
            _httpClient = httpClient;
            _settings = settings;
            _kisTradingService = kisTradingService;
        }

        /// <summary>
        /// 현재 거래 모드에 맞는 설정을 가져온다.
        /// </summary>
        private ApiEndpointSettings GetCurrentSettings()
        {
            return _settings.GetCurrent(_kisTradingService.CurrentEnvironment);
        }

        /// <summary>
        /// 동시에 여러 요청이 들어와도 토큰을 한 번만 새로 발급하도록 막기 위한 락
        /// </summary>
        private readonly SemaphoreSlim _tokenLock = new(1, 1);

        /// <summary>
        /// 공통 JSON 옵션
        /// </summary>
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// 접근 토큰 발급 또는 재사용
        /// </summary>
        public async Task<TokenResponse?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            KisTradingMode currentMode = _kisTradingService.CurrentEnvironment;

            // 1) 현재 환경의 캐시된 토큰이 유효하면 바로 재사용
            if (_tokenCache.TryGetValue(currentMode, out TokenResponse? cached) && IsTokenValid(cached))
            {
                return cached;
            }

            // 2) 동시에 여러 곳에서 토큰 요청이 와도 한 번만 발급
            await _tokenLock.WaitAsync(cancellationToken);

            try
            {
                // 락 대기 중 다른 작업이 이미 토큰을 발급했을 수 있으므로 다시 검사
                if (_tokenCache.TryGetValue(currentMode, out cached) && IsTokenValid(cached))
                {
                    return cached;
                }

                var currentSettings = GetCurrentSettings();

                string appKey = currentSettings.AppKey.Trim();
                string appSecret = currentSettings.AppSecret.Trim();
                string baseUrl = NormalizeBaseUrl(currentSettings.BaseUrl);

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

                // access_token이 비어 있으면 정상 토큰 응답이 아니다.
                if (token == null || string.IsNullOrWhiteSpace(token.AccessToken))
                {
                    throw BuildKisException("토큰 응답 파싱 실패", requestUrl, (int)response.StatusCode, responseText);
                }

                // ===== 현재 환경 키로 저장 — 다른 환경 토큰은 건드리지 않는다 =====
                _tokenCache[currentMode] = token;
                return token;
            }
            finally
            {
                _tokenLock.Release();
            }
        }

        /// <summary>
        /// HashKey 생성
        /// 
        /// 왜 requestBody를 그대로 JSON으로 보내는가?
        /// - hashkey는 "주문 API에 보낼 Body 원문" 기준으로 생성해야 한다.
        /// - 즉, 주문용 DTO를 그대로 넘겨서 같은 JSON 기준으로 해쉬를 받는 것이 안전하다.
        /// </summary>
        public async Task<string> GetHashKeyAsync<T>(T requestBody, CancellationToken cancellationToken = default)
        {
            if (requestBody == null)
            {
                throw new ArgumentNullException(nameof(requestBody), "HashKey를 만들 요청 Body가 null 입니다.");
            }

            var currentSettings = GetCurrentSettings();

            string baseUrl = NormalizeBaseUrl(currentSettings.BaseUrl);
            string appKey = currentSettings.AppKey.Trim();
            string appSecret = currentSettings.AppSecret.Trim();
            string requestUrl = $"{baseUrl}/uapi/hashkey";

            // requestBody를 그대로 JSON으로 보내야 한다.
            // 주문 API body와 hashkey 생성 body가 다르면 서버에서 같은 요청으로 보지 않는다.
            using var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = CreateJsonContent(requestBody)
            };

            // 문서 기준 헤더: appkey, appsecret, content-type
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
            // ===== 현재 환경의 토큰만 무효화한다 =====
            // 다른 환경 토큰은 그대로 유지되므로 전환 시 재사용 가능하다.
            // ===== =====
            _tokenCache.Remove(_kisTradingService.CurrentEnvironment);
        }

        /// <summary>
        /// 현재 토큰이 유효한지 검사
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

            // 만료 시각 문자열을 DateTime으로 변환할 수 없으면 안전하게 false 처리
            if (!DateTime.TryParse(token.AccessTokenExpired, out DateTime expiredAt))
            {
                return false;
            }

            // 만료 직전 1분은 여유를 두고 새 토큰을 발급받는다.
            return DateTime.Now < expiredAt.AddMinutes(-1);
        }

        /// <summary>
        /// BaseUrl 끝의 '/' 를 정리한다.
        /// 
        /// 왜 필요한가?
        /// - 설정 파일에 https://.../ 처럼 끝에 슬래시가 있으면
        ///   URL 조합 시 //oauth2/tokenP 처럼 중복 슬래시가 생길 수 있다.
        /// </summary>
        private static string NormalizeBaseUrl(string baseUrl)
        {
            return baseUrl.Trim().TrimEnd('/');
        }

        /// <summary>
        /// JSON 요청 본문 생성 공통 메서드
        /// </summary>
        private HttpContent CreateJsonContent<T>(T body)
        {
            string json = JsonSerializer.Serialize(body, _jsonOptions);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// 한국투자증권 응답을 기반으로 더 읽기 쉬운 예외를 만든다.
        /// 
        /// 왜 이렇게 하나?
        /// - 단순히 HTTP 코드만 보면 실제 원인 추적이 어렵다.
        /// - error_code / error_description / msg1 등을 같이 보여주면 디버깅이 쉬워진다.
        /// </summary>
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
