using KisRestAPI.Common;
using KisRestAPI.Common.Http;
using KisRestAPI.Models.Market;

namespace KisRestAPI.Market
{
    // =====================================================================
    // ===== 주식현재가 체결 API 빌더 통합 파일 =====
    // [v1_국내주식-009] GET /uapi/domestic-stock/v1/quotations/inquire-ccnl
    //
    // Validator / QueryStringBuilder / UrlBuilder / TrIdProvider /
    // HeaderBuilder를 하나로 모은다.
    // =====================================================================

    // ===== 요청 검증 =====
    internal static class InquireCcnlRequestValidator
    {
        public static void Validate(InquireCcnlRequest request)
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
    internal static class InquireCcnlQueryStringBuilder
    {
        public static string Build(InquireCcnlRequest request)
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
    internal static class InquireCcnlUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/quotations/inquire-ccnl";

        public static string Build(string baseUrl, InquireCcnlRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("BaseUrl이 비어 있을 수 없습니다.", nameof(baseUrl));
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            string normalizedBaseUrl = baseUrl.TrimEnd('/');
            string queryString = InquireCcnlQueryStringBuilder.Build(request);
            return $"{normalizedBaseUrl}{Path}?{queryString}";
        }
    }

    // ===== TR ID 제공 — 주식현재가 체결은 실전/모의 동일 =====
    internal class InquireCcnlTrIdProvider
    {
        private const string TrId = "FHKST01010300";

        public static string Get(KisTradingMode tradingMode)
        {
            return TrId;
        }
    }

    // ===== HTTP 헤더 생성 =====
    internal static class InquireCcnlHeaderBuilder
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
