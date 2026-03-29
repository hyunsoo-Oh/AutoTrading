using KisRestAPI.Common;
using KisRestAPI.Common.Http;
using KisRestAPI.Models.Overseas;

namespace KisRestAPI.Overseas
{
    // =====================================================================
    // ===== 해외주식 종목/지수/환율 기간별시세 API 빌더 통합 파일 =====
    // [v1_해외주식-012] GET /uapi/overseas-price/v1/quotations/inquire-daily-chartprice
    //
    // TR ID는 실전/모의 모두 FHKST03030100으로 동일하다.
    // =====================================================================

    // ===== 요청 검증 =====
    internal static class InquireOvrsDailyChartPriceRequestValidator
    {
        private static readonly HashSet<string> ValidMarketCodes = new() { "N", "X", "I", "S" };
        private static readonly HashSet<string> ValidPeriodCodes = new() { "D", "W", "M", "Y" };

        public static void Validate(InquireOvrsDailyChartPriceRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (!ValidMarketCodes.Contains(request.FID_COND_MRKT_DIV_CODE))
                throw new ArgumentException("시장 분류 코드(FID_COND_MRKT_DIV_CODE)는 N/X/I/S 중 하나여야 합니다.");
            if (string.IsNullOrWhiteSpace(request.FID_INPUT_ISCD))
                throw new ArgumentException("종목코드(FID_INPUT_ISCD)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.FID_INPUT_DATE_1))
                throw new ArgumentException("시작일자(FID_INPUT_DATE_1)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.FID_INPUT_DATE_2))
                throw new ArgumentException("종료일자(FID_INPUT_DATE_2)가 비어 있습니다.");
            if (!ValidPeriodCodes.Contains(request.FID_PERIOD_DIV_CODE))
                throw new ArgumentException("기간 분류 코드(FID_PERIOD_DIV_CODE)는 D/W/M/Y 중 하나여야 합니다.");
        }
    }

    // ===== QueryString 생성 =====
    internal static class InquireOvrsDailyChartPriceQueryStringBuilder
    {
        public static string Build(InquireOvrsDailyChartPriceRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var parameters = new Dictionary<string, string?>
            {
                ["FID_COND_MRKT_DIV_CODE"] = request.FID_COND_MRKT_DIV_CODE,
                ["FID_INPUT_ISCD"]          = request.FID_INPUT_ISCD,
                ["FID_INPUT_DATE_1"]        = request.FID_INPUT_DATE_1,
                ["FID_INPUT_DATE_2"]        = request.FID_INPUT_DATE_2,
                ["FID_PERIOD_DIV_CODE"]     = request.FID_PERIOD_DIV_CODE
            };

            return string.Join("&",
                parameters.Select(p =>
                    $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
        }
    }

    // ===== URL 생성 =====
    internal static class InquireOvrsDailyChartPriceUrlBuilder
    {
        private const string Path = "/uapi/overseas-price/v1/quotations/inquire-daily-chartprice";

        public static string Build(string baseUrl, InquireOvrsDailyChartPriceRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("BaseUrl이 비어 있을 수 없습니다.", nameof(baseUrl));
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            string normalizedBaseUrl = baseUrl.TrimEnd('/');
            string queryString = InquireOvrsDailyChartPriceQueryStringBuilder.Build(request);
            return $"{normalizedBaseUrl}{Path}?{queryString}";
        }
    }

    // ===== TR ID 제공 ? 실전/모의 모두 동일 =====
    internal static class InquireOvrsDailyChartPriceTrIdProvider
    {
        private const string TrId = "FHKST03030100";

        public static string Get(KisTradingMode _) => TrId;
    }

    // ===== HTTP 헤더 생성 =====
    internal static class InquireOvrsDailyChartPriceHeaderBuilder
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
