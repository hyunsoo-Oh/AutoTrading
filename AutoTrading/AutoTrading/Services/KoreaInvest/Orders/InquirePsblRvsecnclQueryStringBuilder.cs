using AutoTrading.Features.Models.Api.Orders;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 주식정정취소가능주문조회 요청 DTO를 QueryString으로 변환하는 클래스
    /// </summary>
    public static class InquirePsblRvsecnclQueryStringBuilder
    {
        public static string Build(InquirePsblRvsecnclRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var queryParameters = new Dictionary<string, string?>
            {
                ["CANO"]          = request.CANO,
                ["ACNT_PRDT_CD"]  = request.ACNT_PRDT_CD,
                ["CTX_AREA_FK100"] = request.CTX_AREA_FK100,
                ["CTX_AREA_NK100"] = request.CTX_AREA_NK100,
                ["INQR_DVSN_1"]   = request.INQR_DVSN_1,
                ["INQR_DVSN_2"]   = request.INQR_DVSN_2,
            };

            return string.Join("&",
                queryParameters.Select(p =>
                    $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
        }
    }
}
