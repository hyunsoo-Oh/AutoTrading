using AutoTrading.Configuration;
using AutoTrading.Features.Models.Api.Accounts;
using AutoTrading.Services.KoreaInvest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoTrading.Services.KoreaInvest.Accounts
{
    /// <summary>
    /// 한국투자증권 계좌 관련 API 서비스
    /// </summary>
    public class KiaAccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly IKiaTradingService _kiaTradingService;
        private readonly InquireBalanceHeaderBuilder _headerBuilder;

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public KiaAccountService(
            HttpClient httpClient,
            IKiaTradingService kiaTradingService,
            InquireBalanceHeaderBuilder headerBuilder)
        {
            _httpClient = httpClient;
            _kiaTradingService = kiaTradingService;
            _headerBuilder = headerBuilder;
        }

        /// <summary>
        /// 주식잔고조회 API를 호출합니다.
        /// </summary>
        public async Task<InquireBalanceResponse?> InquireBalanceAsync(
            InquireBalanceRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // 현재 설정에서 BaseUrl을 가져옵니다.
            // 현재 구조에서는 ApiSettings.TradingMode 기준으로 Mock/Live가 선택됩니다.
            ApiEndpointSettings currentSettings = _kiaTradingService.GetCurrentSettings();

            // 1) 최종 URL 생성
            string requestUrl = InquireBalanceUrlBuilder.Build(currentSettings.BaseUrl, request);

            // 2) Header 생성
            Dictionary<string, string> headers = await _headerBuilder.BuildAsync(cancellationToken);

            // 3) HttpRequestMessage 생성
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            // 4) Header 적용
            ApplyHeaders(httpRequest, headers);

            // 5) 전송
            using HttpResponseMessage response = await _httpClient.SendAsync(httpRequest, cancellationToken);

            // 6) 응답 원문 읽기
            string responseText = await response.Content.ReadAsStringAsync(cancellationToken);

            // 디버깅 초기 단계에서는 원문을 꼭 확인하는 습관이 중요합니다.
            // 실사용 시에는 Console 대신 프로젝트 로그 시스템으로 바꾸세요.
            Console.WriteLine("===== 주식잔고조회 응답 원문 시작 =====");
            Console.WriteLine(responseText);
            Console.WriteLine("===== 주식잔고조회 응답 원문 끝 =====");

            // 7) HTTP 실패면 바로 예외
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    $"주식잔고조회 HTTP 호출 실패\nURL: {requestUrl}\nStatusCode: {(int)response.StatusCode}\nResponse: {responseText}");
            }

            // 8) DTO로 역직렬화
            InquireBalanceResponse? result =
                JsonSerializer.Deserialize<InquireBalanceResponse>(responseText, _jsonOptions);

            if (result == null)
            {
                throw new InvalidOperationException("주식잔고조회 응답 역직렬화 결과가 null 입니다.");
            }

            // 9) 한국투자증권 API 내부 성공/실패 코드 검사
            if (!string.Equals(result.RtCd, "0", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"주식잔고조회 API 실패\nRtCd: {result.RtCd}\nMsgCd: {result.MsgCd}\nMsg1: {result.Msg1}");
            }

            return result;
        }

        /// <summary>
        /// Header 딕셔너리를 HttpRequestMessage에 적용합니다.
        /// </summary>
        private static void ApplyHeaders(
            HttpRequestMessage requestMessage,
            Dictionary<string, string> headers)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                // GET 요청에서는 content-type이 Content에 붙지 않으므로
                // TryAddWithoutValidation으로 일반 헤더처럼 넣어둡니다.
                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }
    }
}
