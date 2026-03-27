using AutoTrading.Features.Models.Api.Orders;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 매수가능조회 API URL 생성기
    /// </summary>
    public static class InquirePsblOrderUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/trading/inquire-psbl-order";

        public static string Build(string baseUrl, InquirePsblOrderRequest request)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException("BaseUrl이 비어 있습니다.", nameof(baseUrl));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            string queryString = InquirePsblOrderQueryStringBuilder.Build(request);
            return $"{baseUrl.TrimEnd('/')}{Path}?{queryString}";
        }
    }
}
