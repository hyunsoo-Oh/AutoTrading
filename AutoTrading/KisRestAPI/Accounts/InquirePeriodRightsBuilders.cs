using KisRestAPI.Common;
using KisRestAPI.Common.Http;
using KisRestAPI.Models.Accounts;

namespace KisRestAPI.Accounts
{
    // =====================================================================
    // ===== 기간별당사권리현황조회 API 빌더 통합 파일 =====
    // Validator / QueryStringBuilder / UrlBuilder / TrIdProvider /
    // HeaderBuilder를 하나로 모은다.
    // =====================================================================

    // ===== 요청 검증 — 날짜 필수 검증 포함 =====
    internal static class InquirePeriodRightsRequestValidator
    {
        public static void Validate(InquirePeriodRightsRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.CANO))
                throw new ArgumentException("계좌번호(CANO)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.ACNT_PRDT_CD))
                throw new ArgumentException("계좌상품코드(ACNT_PRDT_CD)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.INQR_STRT_DT))
                throw new ArgumentException("조회시작일자(INQR_STRT_DT)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.INQR_END_DT))
                throw new ArgumentException("조회종료일자(INQR_END_DT)가 비어 있습니다.");
        }
    }

    // ===== QueryString 생성 =====
    internal static class InquirePeriodRightsQueryStringBuilder
    {
        public static string Build(InquirePeriodRightsRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var queryParameters = new Dictionary<string, string?>
            {
                ["INQR_DVSN"]        = request.INQR_DVSN,
                ["CUST_RNCNO25"]     = request.CUST_RNCNO25,
                ["HMID"]             = request.HMID,
                ["CANO"]             = request.CANO,
                ["ACNT_PRDT_CD"]     = request.ACNT_PRDT_CD,
                ["INQR_STRT_DT"]     = request.INQR_STRT_DT,
                ["INQR_END_DT"]      = request.INQR_END_DT,
                ["RGHT_TYPE_CD"]     = request.RGHT_TYPE_CD,
                ["PDNO"]             = request.PDNO,
                ["PRDT_TYPE_CD"]     = request.PRDT_TYPE_CD,
                ["CTX_AREA_NK100"]   = request.CTX_AREA_NK100,
                ["CTX_AREA_FK100"]   = request.CTX_AREA_FK100,
            };

            return string.Join("&",
                queryParameters.Select(p =>
                    $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
        }
    }

    // ===== URL 생성 =====
    internal static class InquirePeriodRightsUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/trading/period-rights";

        public static string Build(string baseUrl, InquirePeriodRightsRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("BaseUrl이 비어 있습니다.", nameof(baseUrl));
            if (request == null) throw new ArgumentNullException(nameof(request));

            string queryString = InquirePeriodRightsQueryStringBuilder.Build(request);
            return $"{baseUrl.TrimEnd('/')}{Path}?{queryString}";
        }
    }

    // ===== TR ID 제공 — 실전투자 전용 (모의투자 미지원) =====
    internal static class InquirePeriodRightsTrIdProvider
    {
        private const string LiveTrId = "CTRGA011R";

        public static string Get(KisTradingMode tradingMode)
        {
            if (tradingMode != KisTradingMode.Live)
            {
                throw new NotSupportedException(
                    "기간별당사권리현황조회(CTRGA011R)는 실전투자만 지원합니다. " +
                    "모의투자 환경에서는 호출할 수 없습니다.");
            }
            return LiveTrId;
        }
    }

    // ===== HTTP 헤더 생성 =====
    internal static class InquirePeriodRightsHeaderBuilder
    {
        public static Dictionary<string, string> Build(
            string accessToken, string appKey, string appSecret,
            string trId, string custType = "P", string? trCont = null)
        {
            return KisHttpHeaderBuilder.BuildCommon(
                accessToken: accessToken, appKey: appKey, appSecret: appSecret,
                custType: custType,
                extraHeaders: new Dictionary<string, string>
                {
                    ["tr_id"]   = trId,
                    ["tr_cont"] = trCont ?? string.Empty
                });
        }
    }
}
