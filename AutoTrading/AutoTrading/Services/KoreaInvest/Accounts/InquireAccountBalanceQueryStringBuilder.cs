using AutoTrading.Features.Models.Api.Accounts;

namespace AutoTrading.Services.KoreaInvest.Accounts
{
    /// <summary>
    /// 투자계좌자산현황조회 요청 DTO를 QueryString으로 변환하는 클래스
    /// </summary>
    public static class InquireAccountBalanceQueryStringBuilder
    {
        public static string Build(InquireAccountBalanceRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var queryParameters = new Dictionary<string, string?>
            {
                ["CANO"]              = request.CANO,
                ["ACNT_PRDT_CD"]      = request.ACNT_PRDT_CD,
                ["INQR_DVSN_1"]       = request.INQR_DVSN_1,
                ["BSPR_BF_DT_APLY_YN"] = request.BSPR_BF_DT_APLY_YN,
            };

            return string.Join("&",
                queryParameters.Select(p =>
                    $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
        }
    }
}
