using KisRestAPI.Common;
using KisRestAPI.Configuration;
using KisRestAPI.Models.Accounts;
using KisRestAPI.Models.Auth;
using KisRestAPI.Auth;
using KisRestAPI.Common.Http;

namespace KisRestAPI.Accounts
{
    /// <summary>
    /// 한국투자증권 계좌 조회 API 서비스
    ///
    /// 왜 CaptureContext()를 사용하는가?
    /// - 각 메서드 진입 시점에 환경(Mock/Live)을 스냅샷으로 고정한다.
    /// - 비동기 await 도중에 전역 환경이 바뀌어도 해당 요청은 영향을 받지 않는다.
    /// </summary>
    public sealed class KisAccountService : KisHttpServiceBase, IAccountService
    {
        private readonly IAuthService _authService;

        public KisAccountService(
            HttpClient httpClient,
            IKisTradingService kisTradingService,
            IAuthService authService)
            : base(httpClient, kisTradingService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        // =====================================================================
        // ===== 주식잔고조회 =====
        // =====================================================================

        public async Task<InquireBalanceResponse?> InquireBalanceAsync(
            InquireBalanceRequest request,
            CancellationToken cancellationToken = default)
        {
            InquireBalanceRequestValidator.Validate(request);

            // ===== 환경 스냅샷 — 이후 모든 설정은 ctx 기준 =====
            KisRequestContext ctx = CaptureContext();
            string baseUrl = NormalizeBaseUrl(ctx.Settings.BaseUrl);
            string requestUrl = InquireBalanceUrlBuilder.Build(baseUrl, request);

            TokenResponse token = await _authService.GetAccessTokenAsync(ctx.Mode, cancellationToken);
            string trId = InquireBalanceTrIdProvider.Get(ctx.Mode);

            Dictionary<string, string> headers = InquireBalanceHeaderBuilder.Build(
                accessToken: token.AccessToken, appKey: ctx.Settings.AppKey,
                appSecret: ctx.Settings.AppSecret, trId: trId, custType: "P");

            string responseText = await SendGetAsync(
                requestUrl: requestUrl, headers: headers,
                apiName: "주식잔고조회", cancellationToken: cancellationToken);

            InquireBalanceResponse response =
                DeserializeResponse<InquireBalanceResponse>(responseText, "주식잔고조회");

            EnsureBalanceSuccess(response);
            return response;
        }

        private static void EnsureBalanceSuccess(InquireBalanceResponse response)
        {
            if (response == null)
                throw new InvalidOperationException("잔고조회 응답이 null입니다.");

            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"잔고조회 요청은 HTTP 레벨에서는 성공했지만 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }
        }

        protected override void EnsureBusinessSuccess(string apiName, string requestUrl, string responseText)
        {
        }

        // =====================================================================
        // ===== 주식잔고조회_실현손익 =====
        // =====================================================================

        public async Task<InquireBalanceRlzPlResponse> InquireBalanceRlzPlAsync(
            InquireBalanceRlzPlRequest request,
            CancellationToken cancellationToken = default)
        {
            InquireBalanceRlzPlRequestValidator.Validate(request);

            KisRequestContext ctx = CaptureContext();
            string baseUrl = NormalizeBaseUrl(ctx.Settings.BaseUrl);
            string requestUrl = InquireBalanceRlzPlUrlBuilder.Build(baseUrl, request);

            TokenResponse token = await _authService.GetAccessTokenAsync(ctx.Mode, cancellationToken);
            string trId = InquireBalanceRlzPlTrIdProvider.Get(ctx.Mode);

            Dictionary<string, string> headers = InquireBalanceRlzPlHeaderBuilder.Build(
                accessToken: token.AccessToken, appKey: ctx.Settings.AppKey,
                appSecret: ctx.Settings.AppSecret, trId: trId, custType: "P", trCont: string.Empty);

            string responseText = await SendGetAsync(
                requestUrl: requestUrl, headers: headers,
                apiName: "주식잔고조회_실현손익", cancellationToken: cancellationToken);

            InquireBalanceRlzPlResponse response =
                DeserializeResponse<InquireBalanceRlzPlResponse>(responseText, "주식잔고조회_실현손익");

            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"주식잔고조회_실현손익이 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            return response;
        }

        // =====================================================================
        // ===== 투자계좌자산현황조회 =====
        // =====================================================================

        public async Task<InquireAccountBalanceResponse> InquireAccountBalanceAsync(
            InquireAccountBalanceRequest request,
            CancellationToken cancellationToken = default)
        {
            InquireAccountBalanceRequestValidator.Validate(request);

            KisRequestContext ctx = CaptureContext();
            string baseUrl = NormalizeBaseUrl(ctx.Settings.BaseUrl);
            string requestUrl = InquireAccountBalanceUrlBuilder.Build(baseUrl, request);

            TokenResponse token = await _authService.GetAccessTokenAsync(ctx.Mode, cancellationToken);
            string trId = InquireAccountBalanceTrIdProvider.Get(ctx.Mode);

            Dictionary<string, string> headers = InquireAccountBalanceHeaderBuilder.Build(
                accessToken: token.AccessToken, appKey: ctx.Settings.AppKey,
                appSecret: ctx.Settings.AppSecret, trId: trId, custType: "P", trCont: string.Empty);

            string responseText = await SendGetAsync(
                requestUrl: requestUrl, headers: headers,
                apiName: "투자계좌자산현황조회", cancellationToken: cancellationToken);

            InquireAccountBalanceResponse response =
                DeserializeResponse<InquireAccountBalanceResponse>(responseText, "투자계좌자산현황조회");

            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"투자계좌자산현황조회가 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            return response;
        }

        // =====================================================================
        // ===== 기간별손익일별합산조회 =====
        // 지정된 기간 동안의 일별 매매손익을 합산하여 조회한다.
        // ※ 실전투자 전용 — 모의투자 미지원
        // =====================================================================

        public async Task<InquirePeriodProfitLossResponse> InquirePeriodProfitLossAsync(
            InquirePeriodProfitLossRequest request,
            CancellationToken cancellationToken = default)
        {
            InquirePeriodProfitLossRequestValidator.Validate(request);

            KisRequestContext ctx = CaptureContext();
            string baseUrl = NormalizeBaseUrl(ctx.Settings.BaseUrl);
            string requestUrl = InquirePeriodProfitLossUrlBuilder.Build(baseUrl, request);

            TokenResponse token = await _authService.GetAccessTokenAsync(ctx.Mode, cancellationToken);
            string trId = InquirePeriodProfitLossTrIdProvider.Get(ctx.Mode);

            Dictionary<string, string> headers = InquirePeriodProfitLossHeaderBuilder.Build(
                accessToken: token.AccessToken, appKey: ctx.Settings.AppKey,
                appSecret: ctx.Settings.AppSecret, trId: trId, custType: "P", trCont: string.Empty);

            string responseText = await SendGetAsync(
                requestUrl: requestUrl, headers: headers,
                apiName: "기간별손익일별합산조회", cancellationToken: cancellationToken);

            InquirePeriodProfitLossResponse response =
                DeserializeResponse<InquirePeriodProfitLossResponse>(responseText, "기간별손익일별합산조회");

            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"기간별손익일별합산조회가 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            return response;
        }

        // =====================================================================
        // ===== 기간별매매손익현황조회 =====
        // 지정된 기간 동안의 개별 매매손익을 조회한다.
        // ※ 실전투자 전용 — 모의투자 미지원
        // =====================================================================

        public async Task<InquirePeriodTradeProfitResponse> InquirePeriodTradeProfitAsync(
            InquirePeriodTradeProfitRequest request,
            CancellationToken cancellationToken = default)
        {
            InquirePeriodTradeProfitRequestValidator.Validate(request);

            KisRequestContext ctx = CaptureContext();
            string baseUrl = NormalizeBaseUrl(ctx.Settings.BaseUrl);
            string requestUrl = InquirePeriodTradeProfitUrlBuilder.Build(baseUrl, request);

            TokenResponse token = await _authService.GetAccessTokenAsync(ctx.Mode, cancellationToken);
            string trId = InquirePeriodTradeProfitTrIdProvider.Get(ctx.Mode);

            Dictionary<string, string> headers = InquirePeriodTradeProfitHeaderBuilder.Build(
                accessToken: token.AccessToken, appKey: ctx.Settings.AppKey,
                appSecret: ctx.Settings.AppSecret, trId: trId, custType: "P", trCont: string.Empty);

            string responseText = await SendGetAsync(
                requestUrl: requestUrl, headers: headers,
                apiName: "기간별매매손익현황조회", cancellationToken: cancellationToken);

            InquirePeriodTradeProfitResponse response =
                DeserializeResponse<InquirePeriodTradeProfitResponse>(responseText, "기간별매매손익현황조회");

            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"기간별매매손익현황조회가 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            return response;
        }

        // =====================================================================
        // ===== 주식통합증거금 현황 =====
        // ※ 실전투자 전용 — 모의투자 미지원
        // =====================================================================

        public async Task<InquireIntgrMarginResponse> InquireIntgrMarginAsync(
            InquireIntgrMarginRequest request,
            CancellationToken cancellationToken = default)
        {
            InquireIntgrMarginRequestValidator.Validate(request);

            KisRequestContext ctx = CaptureContext();
            string baseUrl = NormalizeBaseUrl(ctx.Settings.BaseUrl);
            string requestUrl = InquireIntgrMarginUrlBuilder.Build(baseUrl, request);

            TokenResponse token = await _authService.GetAccessTokenAsync(ctx.Mode, cancellationToken);
            string trId = InquireIntgrMarginTrIdProvider.Get(ctx.Mode);

            Dictionary<string, string> headers = InquireIntgrMarginHeaderBuilder.Build(
                accessToken: token.AccessToken, appKey: ctx.Settings.AppKey,
                appSecret: ctx.Settings.AppSecret, trId: trId, custType: "P", trCont: string.Empty);

            string responseText = await SendGetAsync(
                requestUrl: requestUrl, headers: headers,
                apiName: "주식통합증거금현황", cancellationToken: cancellationToken);

            InquireIntgrMarginResponse response =
                DeserializeResponse<InquireIntgrMarginResponse>(responseText, "주식통합증거금현황");

            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"주식통합증거금현황이 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            return response;
        }

        // =====================================================================
        // ===== 기간별당사권리현황조회 =====
        // ※ 실전투자 전용 — 모의투자 미지원
        // =====================================================================

        public async Task<InquirePeriodRightsResponse> InquirePeriodRightsAsync(
            InquirePeriodRightsRequest request,
            CancellationToken cancellationToken = default)
        {
            InquirePeriodRightsRequestValidator.Validate(request);

            KisRequestContext ctx = CaptureContext();
            string baseUrl = NormalizeBaseUrl(ctx.Settings.BaseUrl);
            string requestUrl = InquirePeriodRightsUrlBuilder.Build(baseUrl, request);

            TokenResponse token = await _authService.GetAccessTokenAsync(ctx.Mode, cancellationToken);
            string trId = InquirePeriodRightsTrIdProvider.Get(ctx.Mode);

            Dictionary<string, string> headers = InquirePeriodRightsHeaderBuilder.Build(
                accessToken: token.AccessToken, appKey: ctx.Settings.AppKey,
                appSecret: ctx.Settings.AppSecret, trId: trId, custType: "P", trCont: string.Empty);

            string responseText = await SendGetAsync(
                requestUrl: requestUrl, headers: headers,
                apiName: "기간별당사권리현황조회", cancellationToken: cancellationToken);

            InquirePeriodRightsResponse response =
                DeserializeResponse<InquirePeriodRightsResponse>(responseText, "기간별당사권리현황조회");

            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"기간별당사권리현황조회가 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            return response;
        }
    }
}
