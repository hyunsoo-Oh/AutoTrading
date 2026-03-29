using AutoTrading.Features.Models.Api.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Services.KoreaInvest.Accounts
{
    /// <summary>
    /// 주식잔고조회 API의 최종 요청 URL을 만드는 클래스
    /// </summary>
    public static class InquireBalanceUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/trading/inquire-balance";

        public static string Build(string baseUrl, InquireBalanceRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException("BaseUrl은 비어 있을 수 없습니다.", nameof(baseUrl));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            string normalizedBaseUrl = baseUrl.TrimEnd('/');
            string queryString = InquireBalanceQueryStringBuilder.Build(request);

            return $"{normalizedBaseUrl}{Path}?{queryString}";
        }
    }
}
