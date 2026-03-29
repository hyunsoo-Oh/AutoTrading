using KisRestAPI.Common;
using KisRestAPI.Common.Http;
using KisRestAPI.Models.Market;

namespace KisRestAPI.Market
{
    // =====================================================================
    // ===== 국내업종 현재지수 API 빌더 통합 파일 =====
    // [v1_국내주식-063] GET /uapi/domestic-stock/v1/quotations/inquire-index-price
    //
    // [유의사항]
    // - 실전 계좌 전용 API ? 모의투자 미지원
    // - FID_COND_MRKT_DIV_CODE : "U" (업종) 고정
    // =====================================================================

    // ===== 요청 검증 =====
    internal static class InquireIndexPriceRequestValidator
    {
        public static void Validate(InquireIndexPriceRequest request, KisTradingMode mode)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.FID_INPUT_ISCD))
                throw new ArgumentException("업종코드(FID_INPUT_ISCD)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.FID_COND_MRKT_DIV_CODE))
                throw new ArgumentException("시장 분류 코드(FID_COND_MRKT_DIV_CODE)가 비어 있습니다.");

            // ===== 모의투자 환경 차단 =====
            // 국내업종 현재지수 API는 실전 계좌 전용이다.
            if (mode == KisTradingMode.Mock)
                throw new InvalidOperationException("국내업종 현재지수 API는 모의투자를 지원하지 않습니다. 실전 계좌로 전환 후 사용하세요.");
        }
    }

    // ===== QueryString 생성 =====
    internal static class InquireIndexPriceQueryStringBuilder
    {
        public static string Build(InquireIndexPriceRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var parameters = new Dictionary<string, string?>
            {
                ["FID_COND_MRKT_DIV_CODE"] = request.FID_COND_MRKT_DIV_CODE,
                ["FID_INPUT_ISCD"]          = request.FID_INPUT_ISCD
            };

            return string.Join("&",
                parameters.Select(p =>
                    $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
        }
    }

    // ===== URL 생성 =====
    internal static class InquireIndexPriceUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/quotations/inquire-index-price";

        public static string Build(string baseUrl, InquireIndexPriceRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("BaseUrl이 비어 있을 수 없습니다.", nameof(baseUrl));
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            string normalizedBaseUrl = baseUrl.TrimEnd('/');
            string queryString = InquireIndexPriceQueryStringBuilder.Build(request);
            return $"{normalizedBaseUrl}{Path}?{queryString}";
        }
    }

    // ===== TR ID 제공 ? 실전 전용 (모의 미지원) =====
    internal static class InquireIndexPriceTrIdProvider
    {
        private const string TrId = "FHPUP02100000";

        public static string Get(KisTradingMode _) => TrId;
    }

    // ===== HTTP 헤더 생성 =====
    internal static class InquireIndexPriceHeaderBuilder
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
