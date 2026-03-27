using AutoTrading.Features.Models.Api.Orders;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 매도가능수량조회 API URL 생성기
    /// </summary>
    public static class InquirePsblSellUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/trading/inquire-psbl-sell";

        public static string Build(string baseUrl, InquirePsblSellRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException("BaseUrl이 비어 있습니다.", nameof(baseUrl));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            string queryString = InquirePsblSellQueryStringBuilder.Build(request);
            return $"{baseUrl.TrimEnd('/')}{Path}?{queryString}";
        }
    }
}
