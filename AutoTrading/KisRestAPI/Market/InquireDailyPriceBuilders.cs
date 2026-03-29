using KisRestAPI.Common;
using KisRestAPI.Common.Http;
using KisRestAPI.Models.Market;

namespace KisRestAPI.Market
{
    // =====================================================================
    // ===== 주식현재가 일자별 API 빌더 통합 파일 =====
    // [v1_국내주식-010] GET /uapi/domestic-stock/v1/quotations/inquire-daily-price
    //
    // 일/주/월별 주가(최근 30일/주/월)를 조회한다.
    // TR ID는 실전/모의 모두 FHKST01010400으로 동일하다.
    // =====================================================================

    // ===== 요청 검증 =====
    internal static class InquireDailyPriceRequestValidator
    {
        public static void Validate(InquireDailyPriceRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.FID_COND_MRKT_DIV_CODE))
                throw new ArgumentException("시장 분류 코드(FID_COND_MRKT_DIV_CODE)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.FID_INPUT_ISCD))
                throw new ArgumentException("종목코드(FID_INPUT_ISCD)가 비어 있습니다.");
            if (request.FID_PERIOD_DIV_CODE is not ("D" or "W" or "M"))
                throw new ArgumentException("기간 분류 코드(FID_PERIOD_DIV_CODE)는 D/W/M 중 하나여야 합니다.");
            if (request.FID_ORG_ADJ_PRC is not ("0" or "1"))
                throw new ArgumentException("수정주가 원주가 가격(FID_ORG_ADJ_PRC)은 0 또는 1이어야 합니다.");
        }
    }

    // ===== QueryString 생성 =====
    internal static class InquireDailyPriceQueryStringBuilder
    {
        public static string Build(InquireDailyPriceRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var parameters = new Dictionary<string, string?>
            {
                ["FID_COND_MRKT_DIV_CODE"] = request.FID_COND_MRKT_DIV_CODE,
                ["FID_INPUT_ISCD"]          = request.FID_INPUT_ISCD,
                ["FID_PERIOD_DIV_CODE"]     = request.FID_PERIOD_DIV_CODE,
                ["FID_ORG_ADJ_PRC"]         = request.FID_ORG_ADJ_PRC
            };

            return string.Join("&",
                parameters.Select(p =>
                    $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
        }
    }

    // ===== URL 생성 =====
    internal static class InquireDailyPriceUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/quotations/inquire-daily-price";

        public static string Build(string baseUrl, InquireDailyPriceRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("BaseUrl이 비어 있을 수 없습니다.", nameof(baseUrl));
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            string normalizedBaseUrl = baseUrl.TrimEnd('/');
            string queryString = InquireDailyPriceQueryStringBuilder.Build(request);
            return $"{normalizedBaseUrl}{Path}?{queryString}";
        }
    }

    // ===== TR ID 제공 ? 실전/모의 모두 동일한 TR ID =====
    internal static class InquireDailyPriceTrIdProvider
    {
        private const string TrId = "FHKST01010400";

        public static string Get(KisTradingMode _) => TrId;
    }

    // ===== HTTP 헤더 생성 =====
    internal static class InquireDailyPriceHeaderBuilder
    {
        public static Dictionary<string, string> Build(
            string accessToken, string appKey, string appSecret,
            string trId, string custType = "P")
        {
            return KisHttpHeaderBuilder.BuildCommon(
                accessToken: accessToken, appKey: appKey, appSecret: appSecret,
                custType: custType,
                extraHeaders: new Dictionary<string, string>
                {
                    ["tr_id"]   = trId,
                    ["tr_cont"] = string.Empty   // 일자별 API는 연속 조회 불가
                });
        }
    }
}
