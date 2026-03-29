using KisRestAPI.Common;
using KisRestAPI.Common.Http;
using KisRestAPI.Models.Market;

namespace KisRestAPI.Market
{
    // =====================================================================
    // ===== 국내주식기간별시세 API 빌더 통합 파일 =====
    // [v1_국내주식-016] GET /uapi/domestic-stock/v1/quotations/inquire-daily-itemchartprice
    //
    // 일/주/월/년봉 캔들 데이터를 최대 100건 조회한다.
    // TR ID는 실전/모의 모두 FHKST03010100으로 동일하다.
    // =====================================================================

    // ===== 요청 검증 =====
    internal static class InquireDailyItemChartPriceRequestValidator
    {
        public static void Validate(InquireDailyItemChartPriceRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.FID_COND_MRKT_DIV_CODE))
                throw new ArgumentException("시장 분류 코드(FID_COND_MRKT_DIV_CODE)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.FID_INPUT_ISCD))
                throw new ArgumentException("종목코드(FID_INPUT_ISCD)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.FID_INPUT_DATE_1))
                throw new ArgumentException("조회 시작일자(FID_INPUT_DATE_1)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.FID_INPUT_DATE_2))
                throw new ArgumentException("조회 종료일자(FID_INPUT_DATE_2)가 비어 있습니다.");
            if (request.FID_PERIOD_DIV_CODE is not ("D" or "W" or "M" or "Y"))
                throw new ArgumentException("기간 분류 코드(FID_PERIOD_DIV_CODE)는 D/W/M/Y 중 하나여야 합니다.");
            if (request.FID_ORG_ADJ_PRC is not ("0" or "1"))
                throw new ArgumentException("수정주가 원주가 가격(FID_ORG_ADJ_PRC)은 0 또는 1이어야 합니다.");
        }
    }

    // ===== QueryString 생성 =====
    internal static class InquireDailyItemChartPriceQueryStringBuilder
    {
        public static string Build(InquireDailyItemChartPriceRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var parameters = new Dictionary<string, string?>
            {
                ["FID_COND_MRKT_DIV_CODE"] = request.FID_COND_MRKT_DIV_CODE,
                ["FID_INPUT_ISCD"]          = request.FID_INPUT_ISCD,
                ["FID_INPUT_DATE_1"]        = request.FID_INPUT_DATE_1,
                ["FID_INPUT_DATE_2"]        = request.FID_INPUT_DATE_2,
                ["FID_PERIOD_DIV_CODE"]     = request.FID_PERIOD_DIV_CODE,
                ["FID_ORG_ADJ_PRC"]         = request.FID_ORG_ADJ_PRC
            };

            return string.Join("&",
                parameters.Select(p =>
                    $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
        }
    }

    // ===== URL 생성 =====
    internal static class InquireDailyItemChartPriceUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/quotations/inquire-daily-itemchartprice";

        public static string Build(string baseUrl, InquireDailyItemChartPriceRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("BaseUrl이 비어 있을 수 없습니다.", nameof(baseUrl));
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            string normalizedBaseUrl = baseUrl.TrimEnd('/');
            string queryString = InquireDailyItemChartPriceQueryStringBuilder.Build(request);
            return $"{normalizedBaseUrl}{Path}?{queryString}";
        }
    }

    // ===== TR ID 제공 ? 실전/모의 모두 동일 =====
    internal static class InquireDailyItemChartPriceTrIdProvider
    {
        private const string TrId = "FHKST03010100";

        public static string Get(KisTradingMode _) => TrId;
    }

    // ===== HTTP 헤더 생성 =====
    internal static class InquireDailyItemChartPriceHeaderBuilder
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
                    ["tr_cont"] = string.Empty   // 연속 조회 불가 API
                });
        }
    }
}
