using AutoTrading.Configuration;
using AutoTrading.Features.Models.Api.Accounts;
using AutoTrading.Features.Models.Api.Auth;
using AutoTrading.Services.KoreaInvest.Auth;
using AutoTrading.Services.KoreaInvest.Common.Http;

namespace AutoTrading.Services.KoreaInvest.Accounts
{
    /// <summary>
    /// 한국투자증권 계좌 관련 API 서비스
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

        /// <summary>
        /// 주식잔고조회 API를 호출합니다.
        /// </summary>
        public async Task<InquireBalanceResponse?> InquireBalanceAsync(
            InquireBalanceRequest request,
            CancellationToken cancellationToken = default)
        {
            // ===== 안전장치: 요청 검증 =====
            InquireBalanceRequestValidator.Validate(request);

            // ===== 현재 환경 설정 =====
            ApiEndpointSettings settings = GetCurrentSettings();
            string baseUrl = NormalizeBaseUrl(settings.BaseUrl);

            // ===== URL 생성 =====
            string requestUrl = InquireBalanceUrlBuilder.Build(baseUrl, request);

            // ===== 접근토큰 확보 =====
            TokenResponse token = await _authService.GetAccessTokenAsync(cancellationToken);

            // ===== 잔고조회용 TR ID =====
            string trId = InquireBalanceTrIdProvider.Get(_kisTradingService.CurrentEnvironment);

            // ===== 잔고조회 Header =====
            Dictionary<string, string> headers = InquireBalanceHeaderBuilder.Build(
                accessToken: token.AccessToken,
                appKey: settings.AppKey,
                appSecret: settings.AppSecret,
                trId: trId,
                custType: "P");

            // ===== 공통 GET 호출 =====
            string responseText = await SendGetAsync(
                requestUrl: requestUrl,
                headers: headers,
                apiName: "주식잔고조회",
                cancellationToken: cancellationToken);

            // ===== 응답 DTO 변환 =====
            InquireBalanceResponse response =
                DeserializeResponse<InquireBalanceResponse>(responseText, "주식잔고조회");

            // ===== 업무 성공/실패 판정 =====
            EnsureBalanceSuccess(response);

            return response;
        }

        /// <summary>
        /// 잔고조회 응답의 업무 성공 여부를 검사
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        private static void EnsureBalanceSuccess(InquireBalanceResponse response)
        {
            if (response == null)
            {
                throw new InvalidOperationException("잔고조회 응답이 null입니다.");
            }

            // ===== 한국투자 잔고조회 응답 기준 =====
            // RtCd == "0" 이면 성공, 그 외는 실패로 처리
            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"잔고조회 요청은 HTTP 수준에서는 성공했지만 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }
        }

        /// <summary>
        /// HTTP 200이어도 업무상 실패가 내려올 수 있으므로,
        /// 필요 시 여기서 rt_cd 등을 검사한다.
        /// </summary>
        protected override void EnsureBusinessSuccess(string apiName, string requestUrl, string responseText)
        {
            // 업무 레벨 검사는 EnsureBalanceSuccess에서 DTO 기반으로 수행한다.
        }

        // =====================================================================
        // ===== 주식잔고조회_실현손익 =====
        // 현재 보유 종목의 평가손익과 당일 매도로 확정된 실현손익을 함께 조회한다.
        // ※ 실전투자 전용 — 모의투자 미지원
        // =====================================================================

        /// <summary>
        /// 주식잔고조회_실현손익 API를 호출한다.
        /// </summary>
        public async Task<InquireBalanceRlzPlResponse> InquireBalanceRlzPlAsync(
            InquireBalanceRlzPlRequest request,
            CancellationToken cancellationToken = default)
        {
            // ===== 안전장치: 요청 검증 =====
            InquireBalanceRlzPlRequestValidator.Validate(request);

            // ===== 현재 환경 설정 =====
            ApiEndpointSettings settings = GetCurrentSettings();
            string baseUrl = NormalizeBaseUrl(settings.BaseUrl);

            // ===== URL 생성 (QueryString 포함) =====
            string requestUrl = InquireBalanceRlzPlUrlBuilder.Build(baseUrl, request);

            // ===== 접근토큰 확보 =====
            TokenResponse token = await _authService.GetAccessTokenAsync(cancellationToken);

            // ===== 실전 전용 TR ID =====
            // 모의투자 환경에서 호출하면 TrIdProvider에서 NotSupportedException이 발생한다.
            string trId = InquireBalanceRlzPlTrIdProvider.Get(_kisTradingService.CurrentEnvironment);

            // ===== Header 생성 =====
            // 최초 조회 시 tr_cont는 공란
            Dictionary<string, string> headers = InquireBalanceRlzPlHeaderBuilder.Build(
                accessToken: token.AccessToken,
                appKey: settings.AppKey,
                appSecret: settings.AppSecret,
                trId: trId,
                custType: "P",
                trCont: string.Empty);

            // ===== 공통 GET 호출 =====
            string responseText = await SendGetAsync(
                requestUrl: requestUrl,
                headers: headers,
                apiName: "주식잔고조회_실현손익",
                cancellationToken: cancellationToken);

            // ===== 응답 DTO 변환 =====
            InquireBalanceRlzPlResponse response =
                DeserializeResponse<InquireBalanceRlzPlResponse>(responseText, "주식잔고조회_실현손익");

            // ===== 업무 성공/실패 판정 =====
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
        // 주식, 펀드, 채권, 해외주식 등 자산 종류별 비중과 합산 현황을 조회한다.
        // ※ 실전투자 전용 — 모의투자 미지원
        // =====================================================================

        /// <summary>
        /// 투자계좌자산현황조회 API를 호출한다.
        /// </summary>
        public async Task<InquireAccountBalanceResponse> InquireAccountBalanceAsync(
            InquireAccountBalanceRequest request,
            CancellationToken cancellationToken = default)
        {
            // ===== 안전장치: 요청 검증 =====
            InquireAccountBalanceRequestValidator.Validate(request);

            // ===== 현재 환경 설정 =====
            ApiEndpointSettings settings = GetCurrentSettings();
            string baseUrl = NormalizeBaseUrl(settings.BaseUrl);

            // ===== URL 생성 (QueryString 포함) =====
            string requestUrl = InquireAccountBalanceUrlBuilder.Build(baseUrl, request);

            // ===== 접근토큰 확보 =====
            TokenResponse token = await _authService.GetAccessTokenAsync(cancellationToken);

            // ===== 실전 전용 TR ID =====
            // 모의투자 환경에서 호출하면 TrIdProvider에서 NotSupportedException이 발생한다.
            string trId = InquireAccountBalanceTrIdProvider.Get(_kisTradingService.CurrentEnvironment);

            // ===== Header 생성 =====
            Dictionary<string, string> headers = InquireAccountBalanceHeaderBuilder.Build(
                accessToken: token.AccessToken,
                appKey: settings.AppKey,
                appSecret: settings.AppSecret,
                trId: trId,
                custType: "P",
                trCont: string.Empty);

            // ===== 공통 GET 호출 =====
            string responseText = await SendGetAsync(
                requestUrl: requestUrl,
                headers: headers,
                apiName: "투자계좌자산현황조회",
                cancellationToken: cancellationToken);

            // ===== 응답 DTO 변환 =====
            InquireAccountBalanceResponse response =
                DeserializeResponse<InquireAccountBalanceResponse>(responseText, "투자계좌자산현황조회");

            // ===== 업무 성공/실패 판정 =====
            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"투자계좌자산현황조회가 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            return response;
        }
    }
}
