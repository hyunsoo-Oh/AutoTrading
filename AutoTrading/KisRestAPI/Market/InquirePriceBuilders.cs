using KisRestAPI.Common;
using KisRestAPI.Common.Http;
using KisRestAPI.Models.Market;

namespace KisRestAPI.Market
{
    // =====================================================================
    // ===== 주식현재가 시세 API 빌더 통합 파일 =====
    // [v1_국내주식-008] GET /uapi/domestic-stock/v1/quotations/inquire-price
    //
    // Validator / QueryStringBuilder / UrlBuilder / TrIdProvider /
    // HeaderBuilder를 하나로 모은다.
    // =====================================================================

    // ===== 요청 검증 =====
    internal static class InquirePriceRequestValidator
    {
        public static void Validate(InquirePriceRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.FID_COND_MRKT_DIV_CODE))
                throw new ArgumentException("시장 분류 코드(FID_COND_MRKT_DIV_CODE)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.FID_INPUT_ISCD))
                throw new ArgumentException("종목코드(FID_INPUT_ISCD)가 비어 있습니다.");
        }
    }

    // ===== QueryString 생성 =====
    internal static class InquirePriceQueryStringBuilder
    {
        public static string Build(InquirePriceRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var queryParameters = new Dictionary<string, string?>
            {
                ["FID_COND_MRKT_DIV_CODE"] = request.FID_COND_MRKT_DIV_CODE,
                ["FID_INPUT_ISCD"] = request.FID_INPUT_ISCD
            };

            return string.Join("&",
               queryParameters.Select(p =>
                   $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
        }
    }

    // ===== URL 생성 =====
    internal static class InquirePriceUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/quotations/inquire-price";

        public static string Build(string baseUrl, InquirePriceRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("BaseUrl이 비어 있을 수 없습니다.", nameof(baseUrl));
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            string normalizedBaseUrl = baseUrl.TrimEnd('/');
            string queryString = InquirePriceQueryStringBuilder.Build(request);
            return $"{normalizedBaseUrl}{Path}?{queryString}";
        }
    }

    // ===== TR ID 제공 — 주식현재가 시세는 실전/모의 동일 =====
    internal class InquirePriceTrIdProvider
    {
        // 주식현재가 시세 API는 실전/모의 모두 동일한 TR ID를 사용한다.
        private const string TrId = "FHKST01010100";

        public static string Get(KisTradingMode tradingMode)
        {
            return TrId;
        }
    }

    // ===== HTTP 헤더 생성 =====
    internal static class InquirePriceHeaderBuilder
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
                    ["tr_id"] = trId,
                    ["tr_cont"] = trCont ?? string.Empty
                });
        }
    }
}
