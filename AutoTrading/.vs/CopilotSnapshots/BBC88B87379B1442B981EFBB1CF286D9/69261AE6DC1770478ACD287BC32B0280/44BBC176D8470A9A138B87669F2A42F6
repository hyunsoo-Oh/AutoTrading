using AutoTrading.Features.Models.Api.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Services.KoreaInvest.Accounts
{
    /// <summary>
    /// 주식잔고조회 요청 DTO를 QueryString으로 변환하는 클래스
    /// </summary>
    public static class InquireBalanceQueryStringBuilder
    {
        public static string Build(InquireBalanceRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var queryParameters = new Dictionary<string, string?>
            {
                ["CANO"] = request.CANO,
                ["ACNT_PRDT_CD"] = request.ACNT_PRDT_CD,
                ["AFHR_FLPR_YN"] = request.AFHR_FLPR_YN,
                ["OFL_YN"] = request.OFL_YN,
                ["INQR_DVSN"] = request.INQR_DVSN,
                ["UNPR_DVSN"] = request.UNPR_DVSN,
                ["FUND_STTL_ICLD_YN"] = request.FUND_STTL_ICLD_YN,
                ["FNCG_AMT_AUTO_RDPT_YN"] = request.FNCG_AMT_AUTO_RDPT_YN,
                ["PRCS_DVSN"] = request.PRCS_DVSN,
                ["CTX_AREA_FK100"] = request.CTX_AREA_FK100,
                ["CTX_AREA_NK100"] = request.CTX_AREA_NK100
            };

            string queryString = string.Join("&",
               queryParameters.Select(parameter =>
                   $"{Uri.EscapeDataString(parameter.Key)}={Uri.EscapeDataString(parameter.Value ?? string.Empty)}"));

            return queryString;
        }
    }
}
