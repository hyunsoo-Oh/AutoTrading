using KisRestAPI.Common;
using KisRestAPI.Configuration;
using KisRestAPI.Models.Auth;
using KisRestAPI.Models.Orders;
using KisRestAPI.Auth;
using KisRestAPI.Common.Http;

namespace KisRestAPI.Orders
{
    /// <summary>
    /// 한국투자 현금 주문 서비스
    ///
    /// 왜 POST 주문에 hashkey가 필요한가?
    /// - 한국투자증권 REST API는 POST 요청 시 body 기반 hashkey를 헤더에 포함해야 한다.
    /// - hashkey가 누락되면 서버에서 거부하거나 예기치 못한 응답이 올 수 있다.
    /// - CaptureContext()로 스냅샷된 환경 기준으로 hashkey를 생성하여 안전성을 확보한다.
    /// </summary>
    public sealed class KisOrderService : KisHttpServiceBase, IOrderService
    {
        private readonly IAuthService _authService;

        public KisOrderService(
            HttpClient httpClient,
            IKisTradingService kisTradingService,
            IAuthService authService)
            : base(httpClient, kisTradingService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        // =====================================================================
        // ===== 주식주문(현금) — POST =====
        // hashkey 필수: 실제 전송할 request body 기준으로 생성
        // =====================================================================

        public async Task<OrderCashResponse> OrderCashAsync(
            OrderCashRequest request,
            OrderSide orderSide,
            CancellationToken cancellationToken = default)
        {
            // ===== 안전장치: 요청 검증 =====
            OrderCashRequestValidator.Validate(request);

            // ===== 환경 스냅샷 — 이후 모든 설정은 ctx 기준 =====
            KisRequestContext ctx = CaptureContext();
            string baseUrl = NormalizeBaseUrl(ctx.Settings.BaseUrl);

            // ===== URL 생성 =====
            string requestUrl = OrderCashUrlBuilder.Build(baseUrl);

            // ===== 접근토큰 확보 =====
            TokenResponse token = await _authService.GetAccessTokenAsync(ctx.Mode, cancellationToken);

            // ===== 주문용 TR ID =====
            string trId = InquireOrderTrIdProvider.Get(ctx.Mode, orderSide);

            // ===== 주문 Header =====
            Dictionary<string, string> headers = OrderCashHeaderBuilder.Build(
                accessToken: token.AccessToken,
                appKey: ctx.Settings.AppKey,
                appSecret: ctx.Settings.AppSecret,
                trId: trId,
                custType: "P");

            // ===== HashKey 생성 — POST 주문은 반드시 포함 =====
            // 실제 전송할 request body 기준으로 hashkey를 생성하여 헤더에 추가한다.
            // hashkey가 누락되면 한국투자증권 서버에서 요청을 거부할 수 있다.
            string hashKey = await _authService.GetHashKeyAsync(request, ctx.Mode, cancellationToken);
            headers["hashkey"] = hashKey;

            // ===== 공통 POST 호출 =====
            string responseText = await SendPostAsync(
                requestUrl: requestUrl,
                headers: headers,
                body: request,
                apiName: "주식주문(현금)",
                cancellationToken: cancellationToken);

            // ===== 응답 DTO 변환 =====
            OrderCashResponse response =
                DeserializeResponse<OrderCashResponse>(responseText, "주식주문(현금)");

            // ===== 업무 성공/실패 판정 =====
            EnsureOrderSuccess(response);

            return response;
        }

        /// <summary>
        /// 주문 응답의 업무 성공 여부를 검사
        /// </summary>
        private static void EnsureOrderSuccess(OrderCashResponse response)
        {
            if (response == null)
            {
                throw new InvalidOperationException("주문 응답이 null입니다.");
            }

            // ===== 한국투자 주문 응답 기준 =====
            // RtCd == "0" 이면 성공, 그 외는 실패로 처리
            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"주문 요청은 HTTP 수준에서는 성공했지만 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            // ===== 성공이어도 핵심 결과값 검증 =====
            if (response.Output == null)
            {
                throw new InvalidOperationException(
                    $"주문은 성공으로 보였지만 output이 없습니다. msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            if (string.IsNullOrWhiteSpace(response.Output.Odno))
            {
                throw new InvalidOperationException(
                    $"주문은 성공으로 보였지만 주문번호(ODNO)가 없습니다. msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }
        }

        // =====================================================================
        // ===== 주식주문 정정취소 — POST =====
        // hashkey 필수: 실제 전송할 request body 기준으로 생성
        // 이미 체결된 건은 정정/취소 불가하므로 호출 전에 가능 수량을 확인해야 한다.
        // =====================================================================

        public async Task<OrderCashResponse> OrderRvsecnclAsync(
            OrderRvsecnclRequest request,
            OrderRvsecnclDvsnCd dvsnCd,
            CancellationToken cancellationToken = default)
        {
            // ===== 안전장치: enum → 코드 문자열 변환 후 요청에 주입 =====
            request.RvseCnclDvsnCd = dvsnCd.ToCodeString();

            // ===== 안전장치: 요청 검증 =====
            OrderRvsecnclRequestValidator.Validate(request);

            // ===== 환경 스냅샷 =====
            KisRequestContext ctx = CaptureContext();
            string baseUrl = NormalizeBaseUrl(ctx.Settings.BaseUrl);

            // ===== URL 생성 =====
            string requestUrl = OrderRvsecnclUrlBuilder.Build(baseUrl);

            // ===== 접근토큰 확보 =====
            TokenResponse token = await _authService.GetAccessTokenAsync(ctx.Mode, cancellationToken);

            // ===== 정정취소용 TR ID =====
            string trId = OrderRvsecnclTrIdProvider.Get(ctx.Mode);

            // ===== Header 생성 =====
            Dictionary<string, string> headers = OrderRvsecnclHeaderBuilder.Build(
                accessToken: token.AccessToken,
                appKey: ctx.Settings.AppKey,
                appSecret: ctx.Settings.AppSecret,
                trId: trId,
                custType: "P");

            // ===== HashKey 생성 — POST 주문은 반드시 포함 =====
            string hashKey = await _authService.GetHashKeyAsync(request, ctx.Mode, cancellationToken);
            headers["hashkey"] = hashKey;

            // ===== 공통 POST 호출 =====
            string responseText = await SendPostAsync(
                requestUrl: requestUrl,
                headers: headers,
                body: request,
                apiName: "주식주문(정정취소)",
                cancellationToken: cancellationToken);

            // ===== 응답 DTO 변환 =====
            OrderCashResponse response =
                DeserializeResponse<OrderCashResponse>(responseText, "주식주문(정정취소)");

            // ===== 업무 성공/실패 판정 =====
            EnsureOrderSuccess(response);

            return response;
        }

        /// <summary>
        /// HTTP 200이어도 업무상 실패가 내려올 수 있으므로,
        /// 필요 시 여기서 rt_cd 등을 검사한다.
        /// </summary>
        protected override void EnsureBusinessSuccess(string apiName, string requestUrl, string responseText)
        {
        }

        // =====================================================================
        // ===== 주식정정취소가능주문조회 — GET =====
        // 정정/취소 주문 전에 반드시 호출하여 가능 수량(psbl_qty)을 확인한다.
        // ※ 실전투자 전용 — 모의투자 미지원
        // =====================================================================

        public async Task<InquirePsblRvsecnclResponse> InquirePsblRvsecnclAsync(
            InquirePsblRvsecnclRequest request,
            CancellationToken cancellationToken = default)
        {
            InquirePsblRvsecnclRequestValidator.Validate(request);

            KisRequestContext ctx = CaptureContext();
            string baseUrl = NormalizeBaseUrl(ctx.Settings.BaseUrl);
            string requestUrl = InquirePsblRvsecnclUrlBuilder.Build(baseUrl, request);

            TokenResponse token = await _authService.GetAccessTokenAsync(ctx.Mode, cancellationToken);
            string trId = InquirePsblRvsecnclTrIdProvider.Get(ctx.Mode);

            Dictionary<string, string> headers = InquirePsblRvsecnclHeaderBuilder.Build(
                accessToken: token.AccessToken,
                appKey: ctx.Settings.AppKey,
                appSecret: ctx.Settings.AppSecret,
                trId: trId,
                custType: "P");

            string responseText = await SendGetAsync(
                requestUrl: requestUrl,
                headers: headers,
                apiName: "주식정정취소가능주문조회",
                cancellationToken: cancellationToken);

            InquirePsblRvsecnclResponse response =
                DeserializeResponse<InquirePsblRvsecnclResponse>(responseText, "주식정정취소가능주문조회");

            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"주식정정취소가능주문조회가 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            return response;
        }

        // =====================================================================
        // ===== 매수가능조회 — GET =====
        // 매수 주문 전에 호출하여 가능 금액/수량을 확인한다.
        // 실전/모의 모두 지원.
        // =====================================================================

        public async Task<InquirePsblOrderResponse> InquirePsblOrderAsync(
            InquirePsblOrderRequest request,
            CancellationToken cancellationToken = default)
        {
            InquirePsblOrderRequestValidator.Validate(request);

            KisRequestContext ctx = CaptureContext();
            string baseUrl = NormalizeBaseUrl(ctx.Settings.BaseUrl);
            string requestUrl = InquirePsblOrderUrlBuilder.Build(baseUrl, request);

            TokenResponse token = await _authService.GetAccessTokenAsync(ctx.Mode, cancellationToken);
            string trId = InquirePsblOrderTrIdProvider.Get(ctx.Mode);

            Dictionary<string, string> headers = InquirePsblOrderHeaderBuilder.Build(
                accessToken: token.AccessToken,
                appKey: ctx.Settings.AppKey,
                appSecret: ctx.Settings.AppSecret,
                trId: trId,
                custType: "P");

            string responseText = await SendGetAsync(
                requestUrl: requestUrl,
                headers: headers,
                apiName: "매수가능조회",
                cancellationToken: cancellationToken);

            InquirePsblOrderResponse response =
                DeserializeResponse<InquirePsblOrderResponse>(responseText, "매수가능조회");

            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"매수가능조회가 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            return response;
        }

        // =====================================================================
        // ===== 매도가능수량조회 — GET =====
        // 매도 주문 전에 호출하여 실제 주문 가능 수량을 확인한다.
        // ※ 실전투자 전용 — 모의투자 미지원
        // =====================================================================

        public async Task<InquirePsblSellResponse> InquirePsblSellAsync(
            InquirePsblSellRequest request,
            CancellationToken cancellationToken = default)
        {
            InquirePsblSellRequestValidator.Validate(request);

            KisRequestContext ctx = CaptureContext();
            string baseUrl = NormalizeBaseUrl(ctx.Settings.BaseUrl);
            string requestUrl = InquirePsblSellUrlBuilder.Build(baseUrl, request);

            TokenResponse token = await _authService.GetAccessTokenAsync(ctx.Mode, cancellationToken);
            string trId = InquirePsblSellTrIdProvider.Get(ctx.Mode);

            Dictionary<string, string> headers = InquirePsblSellHeaderBuilder.Build(
                accessToken: token.AccessToken,
                appKey: ctx.Settings.AppKey,
                appSecret: ctx.Settings.AppSecret,
                trId: trId,
                custType: "P");

            string responseText = await SendGetAsync(
                requestUrl: requestUrl,
                headers: headers,
                apiName: "매도가능수량조회",
                cancellationToken: cancellationToken);

            InquirePsblSellResponse response =
                DeserializeResponse<InquirePsblSellResponse>(responseText, "매도가능수량조회");

            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"매도가능수량조회가 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            return response;
        }
    }
}
