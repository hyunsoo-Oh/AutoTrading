using KisRestAPI.Common;
using KisRestAPI.Common.Http;
using KisRestAPI.Models.Market;

namespace KisRestAPI.Market
{
    // =====================================================================
    // ===== ETF/ETN 현재가 API 빌더 통합 파일 =====
    // [v1_국내주식-068] GET /uapi/etfetn/v1/quotations/inquire-price
    //
    // [유의사항]
    // - 실전 계좌 전용 API ? 모의투자 미지원
    // - 모의 환경에서 호출 시 예외를 던져 오발주를 방지한다.
    // =====================================================================

    // ===== 요청 검증 =====
    internal static class InquireEtfPriceRequestValidator
    {
        public static void Validate(InquireEtfPriceRequest request, KisTradingMode mode)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.fid_input_iscd))
                throw new ArgumentException("종목코드(fid_input_iscd)가 비어 있습니다.");
            if (string.IsNullOrWhiteSpace(request.fid_cond_mrkt_div_code))
                throw new ArgumentException("시장 분류 코드(fid_cond_mrkt_div_code)가 비어 있습니다.");

            // ===== 모의투자 환경 차단 =====
            // ETF/ETN 현재가 API는 실전 계좌 전용이다.
            // 모의 환경에서 호출하면 서버가 오류를 반환하므로 클라이언트 단에서 미리 막는다.
            if (mode == KisTradingMode.Mock)
                throw new InvalidOperationException("ETF/ETN 현재가 API는 모의투자를 지원하지 않습니다. 실전 계좌로 전환 후 사용하세요.");
        }
    }

    // ===== QueryString 생성 =====
    internal static class InquireEtfPriceQueryStringBuilder
    {
        public static string Build(InquireEtfPriceRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var parameters = new Dictionary<string, string?>
            {
                ["fid_input_iscd"]          = request.fid_input_iscd,
                ["fid_cond_mrkt_div_code"]  = request.fid_cond_mrkt_div_code
            };

            return string.Join("&",
                parameters.Select(p =>
                    $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
        }
    }

    // ===== URL 생성 =====
    internal static class InquireEtfPriceUrlBuilder
    {
        private const string Path = "/uapi/etfetn/v1/quotations/inquire-price";

        public static string Build(string baseUrl, InquireEtfPriceRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("BaseUrl이 비어 있을 수 없습니다.", nameof(baseUrl));
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            string normalizedBaseUrl = baseUrl.TrimEnd('/');
            string queryString = InquireEtfPriceQueryStringBuilder.Build(request);
            return $"{normalizedBaseUrl}{Path}?{queryString}";
        }
    }

    // ===== TR ID 제공 ? 실전 전용 (모의 미지원) =====
    internal static class InquireEtfPriceTrIdProvider
    {
        private const string TrId = "FHPST02400000";

        public static string Get(KisTradingMode _) => TrId;
    }

    // ===== HTTP 헤더 생성 =====
    internal static class InquireEtfPriceHeaderBuilder
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
