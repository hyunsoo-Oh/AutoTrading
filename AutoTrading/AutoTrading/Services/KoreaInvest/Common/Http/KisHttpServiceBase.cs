using AutoTrading.Configuration;
using AutoTrading.Features.Models.Api.Auth;
using System.Text;
using System.Text.Json;

namespace AutoTrading.Services.KoreaInvest.Common.Http
{
    /// <summary>
    /// HTTP API 서비스의 공통 기반 — 모든 한국투자증권 HTTP 서비스가 상속
    /// 
    /// 포함하는 공통 책임:
    /// - JSON 직렬화/역직렬화 옵션
    /// - BaseUrl 정규화
    /// - JSON Body 생성
    /// - HttpRequestMessage에 Header 적용
    /// - GET/POST 공통 전송 + 응답 원문 수신
    /// - HTTP 레벨 예외 생성
    /// - 필요 시 업무 레벨 실패 검사용 확장 포인트 제공
    /// </summary>
    public abstract class KisHttpServiceBase
    {
        // ===== 공통 처리 =====
        // 모든 HTTP 서비스가 공유하는 의존성
        protected readonly HttpClient _httpClient;
        protected readonly IKisTradingService _kisTradingService;

        /// <summary>
        /// 공통 JSON 직렬화 옵션
        /// 왜 PropertyNameCaseInsensitive = true 인가?
        /// - 한국투자증권 API 응답의 키가 소문자 snake_case이므로
        ///   DTO의 PascalCase 속성과 자동 매핑시키기 위함이다.
        /// </summary>
        protected static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        protected KisHttpServiceBase(HttpClient httpClient, IKisTradingService kisTradingService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _kisTradingService = kisTradingService ?? throw new ArgumentNullException(nameof(kisTradingService));
        }

        // ===== 공통 처리: 현재 환경 설정 =====

        /// <summary>
        /// 현재 선택된 Mock/Live 환경의 API 설정을 반환
        /// </summary>
        protected ApiEndpointSettings GetCurrentSettings()
        {
            return _kisTradingService.GetCurrentSettings();
        }

        // ===== 공통 처리: URL 정규화 =====

        /// <summary>
        /// BaseUrl 끝의 '/'를 정리
        ///
        /// 왜 필요한가?
        /// - 설정 파일에 https://.../ 처럼 끝에 슬래시가 있으면
        ///   URL 조합 시 //uapi/... 처럼 중복 슬래시가 생길 수 있다.
        /// </summary>
        protected static string NormalizeBaseUrl(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException("BaseUrl이 비어 있습니다.", nameof(baseUrl));
            }

            return baseUrl.Trim().TrimEnd('/');
        }

        // ===== 공통 처리: JSON Body 생성 =====

        /// <summary>
        /// 객체를 JSON 문자열로 직렬화하여 StringContent로 감싼다.
        ///
        /// POST 요청에서 Body를 보낼 때 사용한다.
        /// </summary>
        protected HttpContent CreateJsonContent<T>(T body)
        {
            string json = JsonSerializer.Serialize(body, JsonOptions);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        // ===== 공통 처리: Header 적용 =====

        /// <summary>
        /// Header 딕셔너리를 HttpRequestMessage에 적용한다.
        ///
        /// 왜 TryAddWithoutValidation을 쓰는가?
        /// - content-type 등 일부 헤더는 기본 Add 메서드에서 검증 실패할 수 있으므로
        ///   TryAddWithoutValidation으로 강제 추가한다.
        /// </summary>
        protected static void ApplyHeaders(
            HttpRequestMessage request,
            IReadOnlyDictionary<string, string> headers)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                if (string.Equals(header.Key, "content-type", StringComparison.OrdinalIgnoreCase))
                {
                    // ===== 안전장치 =====
                    // Content-Type은 HttpContent 쪽 책임으로 통일하기 위해 무시
                    continue;
                }

                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        // ===== 공통 처리: GET 요청 =====

        /// <summary>
        /// 공통 GET 요청을 보내고 응답 원문(string)을 반환
        ///
        /// 도메인 서비스는 이 메서드를 호출한 뒤,
        /// 반환된 문자열을 자신의 DTO로 역직렬화하면 된다.
        /// </summary>
        /// <param name="requestUrl">최종 URL (BaseUrl + Path + QueryString)</param>
        /// <param name="headers">인증/TR ID 등 헤더 딕셔너리</param>
        /// <param name="apiName">에러 메시지에 표시할 API 이름 (예: "주식잔고조회")</param>
        /// <param name="cancellationToken">취소 토큰</param>
        /// <returns>HTTP 응답 원문 문자열</returns>
        protected async Task<string> SendGetAsync(
            string requestUrl,
            Dictionary<string, string> headers,
            string apiName,
            CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            // ===== HTTP Header =====
            ApplyHeaders(request, headers);

            using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
            string responseText = await response.Content.ReadAsStringAsync(cancellationToken);

            // ===== 안전장치 =====
            // HTTP 상태코드가 실패면 공통 예외를 생성한다.
            if (!response.IsSuccessStatusCode)
            {
                throw BuildKisException($"{apiName} HTTP 호출 실패", requestUrl, (int)response.StatusCode, responseText);
            }

            // ===== 업무 레벨 실패 처리 =====
            EnsureBusinessSuccess(apiName, requestUrl, responseText);

            return responseText;
        }

        // ===== 공통 처리: POST 요청 =====

        /// <summary>
        /// 공통 POST 요청을 보내고 응답 원문(string)을 반환한다.
        ///
        /// 주문, 토큰 발급 등 Body가 필요한 API에서 사용한다.
        /// </summary>
        /// <param name="requestUrl">최종 URL</param>
        /// <param name="headers">헤더 딕셔너리</param>
        /// <param name="body">JSON으로 직렬화할 요청 Body 객체</param>
        /// <param name="apiName">에러 메시지에 표시할 API 이름</param>
        /// <param name="cancellationToken">취소 토큰</param>
        /// <returns>HTTP 응답 원문 문자열</returns>
        protected async Task<string> SendPostAsync<TBody>(
            string requestUrl,
            Dictionary<string, string> headers,
            TBody body,
            string apiName,
            CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                // ===== HTTP Body =====
                Content = CreateJsonContent(body)
            };

            // ===== HTTP Header =====
            ApplyHeaders(request, headers);

            using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
            string responseText = await response.Content.ReadAsStringAsync(cancellationToken);

            // ===== 안전장치 =====
            if (!response.IsSuccessStatusCode)
            {
                throw BuildKisException($"{apiName} HTTP 호출 실패", requestUrl, (int)response.StatusCode, responseText);
            }

            // ===== 업무 레벨 실패 처리 =====
            EnsureBusinessSuccess(apiName, requestUrl, responseText);

            return responseText;
        }

        // ===== 공통 처리: 응답 역직렬화 =====

        /// <summary>
        /// JSON 응답 원문을 지정된 DTO로 역직렬화한다.
        /// null이면 예외를 던진다.
        ///
        /// 왜 공통으로 분리했는가?
        /// - 역직렬화 + null 검사 패턴이 모든 API에서 반복되므로
        ///   한 곳에서 처리하여 일관성을 유지한다.
        /// </summary>
        protected TResponse DeserializeResponse<TResponse>(string responseText, string apiName)
            where TResponse : class
        {
            try
            {
                TResponse? result = JsonSerializer.Deserialize<TResponse>(responseText, JsonOptions);

                if (result == null)
                {
                    throw new InvalidOperationException($"{apiName} 응답 역직렬화 결과가 null입니다.");
                }

                return result;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException(
                    $"{apiName} 응답 JSON 역직렬화 실패. Body={responseText}", ex);
            }
        }

        /// <summary>
        /// 업무 레벨 성공 여부를 검사하는 확장 포인트
        ///
        /// 왜 필요한가?
        /// - 어떤 API는 HTTP 200이어도 본문 안에서 실패(rt_cd, msg_cd 등)를 내려줄 수 있다.
        /// - 공통 베이스는 기본적으로 아무 것도 하지 않고,
        ///   도메인 서비스가 필요할 때 override하여 사용한다.
        /// </summary>
        protected virtual void EnsureBusinessSuccess(string apiName, string requestUrl, string responseText)
        {
            // 기본 구현은 아무 것도 하지 않는다.
            // 도메인 서비스에서 필요 시 override한다.
        }

        // ===== 공통 처리: 에러 파싱 =====

        /// <summary>
        /// 한국투자증권 응답을 기반으로 더 읽기 쉬운 예외를 만든다.
        ///
        /// 왜 이렇게 하나?
        /// - 단순히 HTTP 코드만 보면 실제 원인 추적이 어렵다.
        /// - error_code / error_description / msg1 등을 같이 보여주면 디버깅이 쉬워진다.
        /// </summary>
        protected Exception BuildKisException(string title, string requestUrl, int statusCode, string responseText)
        {
            try
            {
                var error = JsonSerializer.Deserialize<KisErrorResponse>(responseText, JsonOptions);

                if (error != null &&
                    (!string.IsNullOrWhiteSpace(error.ErrorCode) ||
                     !string.IsNullOrWhiteSpace(error.ErrorDescription) ||
                     !string.IsNullOrWhiteSpace(error.Message1)))
                {
                    return new InvalidOperationException(
                        $"{title} | HTTP={statusCode} | URL={requestUrl}" +
                        $" | error_code={error.ErrorCode}" +
                        $" | error_description={error.ErrorDescription}" +
                        $" | msg_cd={error.MessageCode}" +
                        $" | msg1={error.Message1}");
                }
            }
            catch
            {
                // JSON 파싱 실패 시 원문 그대로 사용 — 아래 fallback으로 넘어간다
            }

            return new InvalidOperationException(
                $"{title} | HTTP={statusCode} | URL={requestUrl} | Body={responseText}");
        }
    }
}
