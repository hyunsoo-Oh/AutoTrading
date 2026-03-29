using KisRestAPI.Common;
using KisRestAPI.Common.Http;
using KisRestAPI.Models.Market;

namespace KisRestAPI.Market
{
    // =====================================================================
    // ===== 주식당일분봉조회 API 빌더 통합 파일 =====
    // [v1_국내주식-022] GET /uapi/domestic-stock/v1/quotations/inquire-time-itemchartprice
    //
    // 당일 분봉 데이터를 최대 30건 조회한다.
    // TR ID는 실전/모의 모두 FHKST03010200으로 동일하다.
    // =====================================================================

    // ===== 요청 검증 =====
    internal static class InquireTimeItemChartPriceRequestValidator
    {
        public static void Validate(InquireTimeItemChartPriceRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.FID_COND_MRKT_DIV_CODE))
                throw new ArgumentException("시장 분류 코드(FID_COND_MRKT_DIV_CODE)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.FID_INPUT_ISCD))
                throw new ArgumentException("종목코드(FID_INPUT_ISCD)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.FID_INPUT_HOUR_1))
                throw new ArgumentException("입력 시간(FID_INPUT_HOUR_1)이 비어 있습니다.");
        }
    }

    // ===== QueryString 생성 =====
    internal static class InquireTimeItemChartPriceQueryStringBuilder
    {
        public static string Build(InquireTimeItemChartPriceRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var parameters = new Dictionary<string, string?>
            {
                ["FID_COND_MRKT_DIV_CODE"] = request.FID_COND_MRKT_DIV_CODE,
                ["FID_INPUT_ISCD"]          = request.FID_INPUT_ISCD,
                ["FID_INPUT_HOUR_1"]        = request.FID_INPUT_HOUR_1,
                ["FID_PW_DATA_INCU_YN"]     = request.FID_PW_DATA_INCU_YN,
                ["FID_ETC_CLS_CODE"]        = request.FID_ETC_CLS_CODE
            };

            return string.Join("&",
                parameters.Select(p =>
                    $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
        }
    }

    // ===== URL 생성 =====
    internal static class InquireTimeItemChartPriceUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/quotations/inquire-time-itemchartprice";

        public static string Build(string baseUrl, InquireTimeItemChartPriceRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("BaseUrl이 비어 있을 수 없습니다.", nameof(baseUrl));
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            string normalizedBaseUrl = baseUrl.TrimEnd('/');
            string queryString = InquireTimeItemChartPriceQueryStringBuilder.Build(request);
            return $"{normalizedBaseUrl}{Path}?{queryString}";
        }
    }

    // ===== TR ID 제공 ? 실전/모의 모두 동일 =====
    internal static class InquireTimeItemChartPriceTrIdProvider
    {
        private const string TrId = "FHKST03010200";

        public static string Get(KisTradingMode _) => TrId;
    }

    // ===== HTTP 헤더 생성 =====
    internal static class InquireTimeItemChartPriceHeaderBuilder
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
