using AutoTrading.Features.Models.Api.Accounts;

namespace AutoTrading.Services.KoreaInvest.Accounts
{
    /// <summary>
    /// 주식잔고조회_실현손익 API URL 생성기
    /// </summary>
    public static class InquireBalanceRlzPlUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/trading/inquire-balance-rlz-pl";

        public static string Build(string baseUrl, InquireBalanceRlzPlRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException("BaseUrl이 비어 있습니다.", nameof(baseUrl));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            string queryString = InquireBalanceRlzPlQueryStringBuilder.Build(request);
            return $"{baseUrl.TrimEnd('/')}{Path}?{queryString}";
        }
    }
}
