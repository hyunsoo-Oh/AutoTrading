using KisRestAPI.Auth;
using KisRestAPI.Common;
using KisRestAPI.Common.Http;
using KisRestAPI.Models.Auth;
using KisRestAPI.Models.Overseas;

namespace KisRestAPI.Overseas
{
    /// <summary>
    /// 한국투자증권 해외주식 시세 조회 API 서비스
    ///
    /// 왜 CaptureContext()를 사용하는가?
    /// - 각 메서드 진입 시점에 환경(Mock/Live)을 스냅샷으로 고정한다.
    /// - 비동기 await 도중에 전역 환경이 바뀌어도 해당 요청은 영향을 받지 않는다.
    /// </summary>
    public sealed class KisOverseasMarketService : KisHttpServiceBase, IOverseasMarketService
    {
        private readonly IAuthService _authService;

        public KisOverseasMarketService(
            HttpClient httpClient,
            IKisTradingService kisTradingService,
            IAuthService authService)
            : base(httpClient, kisTradingService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        // =====================================================================
        // ===== 해외주식 종목/지수/환율 기간별시세 [v1_해외주식-012] =====
        // =====================================================================

        public async Task<InquireOvrsDailyChartPriceResponse> InquireOvrsDailyChartPriceAsync(
            InquireOvrsDailyChartPriceRequest request,
            CancellationToken cancellationToken = default)
        {
            InquireOvrsDailyChartPriceRequestValidator.Validate(request);

            // ===== 환경 스냅샷 ? 이후 모든 설정은 ctx 기준 =====
            KisRequestContext ctx = CaptureContext();
            string baseUrl    = NormalizeBaseUrl(ctx.Settings.BaseUrl);
            string requestUrl = InquireOvrsDailyChartPriceUrlBuilder.Build(baseUrl, request);

            TokenResponse token = await _authService.GetAccessTokenAsync(ctx.Mode, cancellationToken);
            string trId = InquireOvrsDailyChartPriceTrIdProvider.Get(ctx.Mode);

            Dictionary<string, string> headers = InquireOvrsDailyChartPriceHeaderBuilder.Build(
                accessToken: token.AccessToken, appKey: ctx.Settings.AppKey,
                appSecret: ctx.Settings.AppSecret, trId: trId, custType: "P");

            string responseText = await SendGetAsync(
                requestUrl: requestUrl, headers: headers,
                apiName: "해외주식기간별시세", cancellationToken: cancellationToken);

            InquireOvrsDailyChartPriceResponse response =
                DeserializeResponse<InquireOvrsDailyChartPriceResponse>(responseText, "해외주식기간별시세");

            EnsureOvrsDailyChartPriceSuccess(response);
            return response;
        }

        private static void EnsureOvrsDailyChartPriceSuccess(InquireOvrsDailyChartPriceResponse response)
        {
            if (response is null)
                throw new InvalidOperationException("해외주식기간별시세 응답이 null입니다.");

            if (response.RtCd != "0")
                throw new InvalidOperationException(
                    $"해외주식기간별시세 요청은 HTTP 레벨에서는 성공했지만 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
        }

        protected override void EnsureBusinessSuccess(string apiName, string requestUrl, string responseText)
        {
        }
    }
}
