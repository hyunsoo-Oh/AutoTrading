using KisRestAPI.Common;
using KisRestAPI.Common.Http;
using KisRestAPI.Models.Market;

namespace KisRestAPI.Market
{
    // =====================================================================
    // ===== 금리 종합(국내채권/금리) API 빌더 통합 파일 =====
    // [국내주식-155] GET /uapi/domestic-stock/v1/quotations/comp-interest
    //
    // [유의사항]
    // - 실전 계좌 전용 API ? 모의투자 미지원
    // - 11:30 이후에 신규 데이터가 수신된다.
    // =====================================================================

    // ===== 요청 검증 =====
    internal static class CompInterestRequestValidator
    {
        public static void Validate(CompInterestRequest request, KisTradingMode mode)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.FID_COND_MRKT_DIV_CODE))
                throw new ArgumentException("시장 분류 코드(FID_COND_MRKT_DIV_CODE)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.FID_COND_SCR_DIV_CODE))
                throw new ArgumentException("화면 분류 코드(FID_COND_SCR_DIV_CODE)가 비어 있습니다.");

            // ===== 모의투자 환경 차단 =====
            // 금리 종합 API는 실전 계좌 전용이다.
            if (mode == KisTradingMode.Mock)
                throw new InvalidOperationException("금리 종합 API는 모의투자를 지원하지 않습니다. 실전 계좌로 전환 후 사용하세요.");
        }
    }

    // ===== QueryString 생성 =====
    internal static class CompInterestQueryStringBuilder
    {
        public static string Build(CompInterestRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var parameters = new Dictionary<string, string?>
            {
                ["FID_COND_MRKT_DIV_CODE"] = request.FID_COND_MRKT_DIV_CODE,
                ["FID_COND_SCR_DIV_CODE"]  = request.FID_COND_SCR_DIV_CODE,
                ["FID_DIV_CLS_CODE"]       = request.FID_DIV_CLS_CODE,
                ["FID_DIV_CLS_CODE1"]      = request.FID_DIV_CLS_CODE1
            };

            return string.Join("&",
                parameters.Select(p =>
                    $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
        }
    }

    // ===== URL 생성 =====
    internal static class CompInterestUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/quotations/comp-interest";

        public static string Build(string baseUrl, CompInterestRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("BaseUrl이 비어 있을 수 없습니다.", nameof(baseUrl));
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            string normalizedBaseUrl = baseUrl.TrimEnd('/');
            string queryString = CompInterestQueryStringBuilder.Build(request);
            return $"{normalizedBaseUrl}{Path}?{queryString}";
        }
    }

    // ===== TR ID 제공 ? 실전 전용 (모의 미지원) =====
    internal static class CompInterestTrIdProvider
    {
        private const string TrId = "FHPST07020000";

        public static string Get(KisTradingMode _) => TrId;
    }

    // ===== HTTP 헤더 생성 =====
    internal static class CompInterestHeaderBuilder
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
