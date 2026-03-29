using AutoTrading.Configuration;
using AutoTrading.Features.Models.Api.Auth;
using AutoTrading.Features.Models.Api.Orders;
using AutoTrading.Services.KoreaInvest.Auth;
using AutoTrading.Services.KoreaInvest.Common.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 한국투자 현금 주문 서비스
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

        public async Task<OrderCashResponse> OrderCashAsync(
            OrderCashRequest request,
            OrderSide orderSide,
            CancellationToken cancellationToken = default)
        {
            // ===== 안전장치: 요청 검증 =====
            OrderCashRequestValidator.Validate(request);

            // ===== 현재 환경 설정 =====
            ApiEndpointSettings settings = GetCurrentSettings();
            string baseUrl = NormalizeBaseUrl(settings.BaseUrl);

            // ===== URL 생성 =====
            string requestUrl = OrderCashUrlBuilder.Build(baseUrl);

            // ===== 접근토큰 확보 =====
            TokenResponse token = await _authService.GetAccessTokenAsync(cancellationToken);

            // ===== 주문용 TR ID =====
            string trId = InquireOrderTrIdProvider.Get(_kisTradingService.CurrentEnvironment, orderSide);

            // ===== 주문 Header =====
            Dictionary<string, string> headers = OrderCashHeaderBuilder.Build(
                accessToken: token.AccessToken,
                appKey: settings.AppKey,
                appSecret: settings.AppSecret,
                trId: trId,
                custType: "P");

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
        /// <param name="response"></param>
        /// <exception cref="InvalidOperationException"></exception>
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
        // ===== 주식주문 정정취소 =====
        // 원주문에 대해 정정(가격/구분 변경) 또는 취소를 수행한다.
        // 이미 체결된 건은 정정/취소 불가하므로 호출 전에 가능 수량을 확인해야 한다.
        // =====================================================================

        public async Task<OrderCashResponse> OrderRvsecnclAsync(
            OrderRvsecnclRequest request,
            OrderRvsecnclDvsnCd dvsnCd,
            CancellationToken cancellationToken = default)
        {
            // ===== 안전장치: enum → 코드 문자열 변환 후 요청에 주입 =====
            // Presenter에서 문자열을 직접 넣지 않도록 enum을 통해 안전하게 변환한다.
            request.RvseCnclDvsnCd = dvsnCd.ToCodeString();

            // ===== 안전장치: 요청 검증 =====
            OrderRvsecnclRequestValidator.Validate(request);

            // ===== 현재 환경 설정 =====
            ApiEndpointSettings settings = GetCurrentSettings();
            string baseUrl = NormalizeBaseUrl(settings.BaseUrl);

            // ===== URL 생성 =====
            string requestUrl = OrderRvsecnclUrlBuilder.Build(baseUrl);

            // ===== 접근토큰 확보 =====
            TokenResponse token = await _authService.GetAccessTokenAsync(cancellationToken);

            // ===== 정정취소용 TR ID =====
            string trId = OrderRvsecnclTrIdProvider.Get(_kisTradingService.CurrentEnvironment);

            // ===== Header 생성 =====
            Dictionary<string, string> headers = OrderRvsecnclHeaderBuilder.Build(
                accessToken: token.AccessToken,
                appKey: settings.AppKey,
                appSecret: settings.AppSecret,
                trId: trId,
                custType: "P");

            // ===== 공통 POST 호출 =====
            string responseText = await SendPostAsync(
                requestUrl: requestUrl,
                headers: headers,
                body: request,
                apiName: "주식주문(정정취소)",
                cancellationToken: cancellationToken);

            // ===== 응답 DTO 변환 =====
            // 정정취소 응답 구조는 현금 주문 응답(OrderCashResponse)과 동일하다.
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
            // 아직 Response DTO 구조를 확정하지 않았다면 일단 비워둬도 된다.
            // 다음 단계에서 OrderCashResponse의 rt_cd / msg_cd / msg1 기준으로 강화하면 된다.
        }

        // =====================================================================
        // ===== 주식정정취소가능주문조회 =====
        // 정정/취소 주문 전에 반드시 호출하여 가능 수량(psbl_qty)을 확인한다.
        // ※ 실전투자 전용 — 모의투자 미지원
        // =====================================================================

        public async Task<InquirePsblRvsecnclResponse> InquirePsblRvsecnclAsync(
            InquirePsblRvsecnclRequest request,
            CancellationToken cancellationToken = default)
        {
            // ===== 안전장치: 요청 검증 =====
            InquirePsblRvsecnclRequestValidator.Validate(request);

            // ===== 현재 환경 설정 =====
            ApiEndpointSettings settings = GetCurrentSettings();
            string baseUrl = NormalizeBaseUrl(settings.BaseUrl);

            // ===== URL 생성 (QueryString 포함) =====
            string requestUrl = InquirePsblRvsecnclUrlBuilder.Build(baseUrl, request);

            // ===== 접근토큰 확보 =====
            TokenResponse token = await _authService.GetAccessTokenAsync(cancellationToken);

            // ===== 실전 전용 TR ID =====
            // 모의투자 환경에서 호출하면 TrIdProvider에서 NotSupportedException이 발생한다.
            string trId = InquirePsblRvsecnclTrIdProvider.Get(_kisTradingService.CurrentEnvironment);

            // ===== Header 생성 =====
            Dictionary<string, string> headers = InquirePsblRvsecnclHeaderBuilder.Build(
                accessToken: token.AccessToken,
                appKey: settings.AppKey,
                appSecret: settings.AppSecret,
                trId: trId,
                custType: "P");

            // ===== 공통 GET 호출 =====
            string responseText = await SendGetAsync(
                requestUrl: requestUrl,
                headers: headers,
                apiName: "주식정정취소가능주문조회",
                cancellationToken: cancellationToken);

            // ===== 응답 DTO 변환 =====
            InquirePsblRvsecnclResponse response =
                DeserializeResponse<InquirePsblRvsecnclResponse>(responseText, "주식정정취소가능주문조회");

            // ===== 업무 성공/실패 판정 =====
            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"주식정정취소가능주문조회가 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            return response;
        }

        // =====================================================================
        // ===== 매수가능조회 =====
        // 매수 주문 전에 호출하여 가능 금액/수량을 확인한다.
        // 실전/모의 모두 지원.
        // =====================================================================

        public async Task<InquirePsblOrderResponse> InquirePsblOrderAsync(
            InquirePsblOrderRequest request,
            CancellationToken cancellationToken = default)
        {
            // ===== 안전장치: 요청 검증 =====
            InquirePsblOrderRequestValidator.Validate(request);

            // ===== 현재 환경 설정 =====
            ApiEndpointSettings settings = GetCurrentSettings();
            string baseUrl = NormalizeBaseUrl(settings.BaseUrl);

            // ===== URL 생성 (QueryString 포함) =====
            string requestUrl = InquirePsblOrderUrlBuilder.Build(baseUrl, request);

            // ===== 접근토큰 확보 =====
            TokenResponse token = await _authService.GetAccessTokenAsync(cancellationToken);

            // ===== 실전/모의 TR ID 선택 =====
            string trId = InquirePsblOrderTrIdProvider.Get(_kisTradingService.CurrentEnvironment);

            // ===== Header 생성 =====
            Dictionary<string, string> headers = InquirePsblOrderHeaderBuilder.Build(
                accessToken: token.AccessToken,
                appKey: settings.AppKey,
                appSecret: settings.AppSecret,
                trId: trId,
                custType: "P");

            // ===== 공통 GET 호출 =====
            string responseText = await SendGetAsync(
                requestUrl: requestUrl,
                headers: headers,
                apiName: "매수가능조회",
                cancellationToken: cancellationToken);

            // ===== 응답 DTO 변환 =====
            InquirePsblOrderResponse response =
                DeserializeResponse<InquirePsblOrderResponse>(responseText, "매수가능조회");

            // ===== 업무 성공/실패 판정 =====
            if (response.RtCd != "0")
            {
                throw new InvalidOperationException(
                    $"매수가능조회가 업무적으로 실패했습니다. " +
                    $"rt_cd={response.RtCd}, msg_cd={response.MsgCd}, msg1={response.Msg1}");
            }

            return response;
        }

        // =====================================================================
        // ===== 매도가능수량조회 =====
        // 매도 주문 전에 호출하여 실제 주문 가능 수량을 확인한다.
        // ※ 실전투자 전용 — 모의투자 미지원
        // =====================================================================

        public async Task<InquirePsblSellResponse> InquirePsblSellAsync(
            InquirePsblSellRequest request,
            CancellationToken cancellationToken = default)
        {
            // ===== 안전장치: 요청 검증 =====
            InquirePsblSellRequestValidator.Validate(request);

            // ===== 현재 환경 설정 =====
            ApiEndpointSettings settings = GetCurrentSettings();
            string baseUrl = NormalizeBaseUrl(settings.BaseUrl);

            // ===== URL 생성 (QueryString 포함) =====
            string requestUrl = InquirePsblSellUrlBuilder.Build(baseUrl, request);

            // ===== 접근토큰 확보 =====
            TokenResponse token = await _authService.GetAccessTokenAsync(cancellationToken);

            // ===== 실전 전용 TR ID =====
            // 모의투자 환경에서 호출하면 TrIdProvider에서 NotSupportedException이 발생한다.
            string trId = InquirePsblSellTrIdProvider.Get(_kisTradingService.CurrentEnvironment);

            // ===== Header 생성 =====
            Dictionary<string, string> headers = InquirePsblSellHeaderBuilder.Build(
                accessToken: token.AccessToken,
                appKey: settings.AppKey,
                appSecret: settings.AppSecret,
                trId: trId,
                custType: "P");

            // ===== 공통 GET 호출 =====
            string responseText = await SendGetAsync(
                requestUrl: requestUrl,
                headers: headers,
                apiName: "매도가능수량조회",
                cancellationToken: cancellationToken);

            // ===== 응답 DTO 변환 =====
            InquirePsblSellResponse response =
                DeserializeResponse<InquirePsblSellResponse>(responseText, "매도가능수량조회");

            // ===== 업무 성공/실패 판정 =====
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
