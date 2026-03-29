using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 현금 주문 API URL 생성기
    /// </summary>
    public static class OrderCashUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/trading/order-cash";

        public static string Build(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException("BaseUrl이 비어 있습니다.", nameof(baseUrl));
            }

            return $"{baseUrl.TrimEnd('/')}{Path}";
        }
    }
}
