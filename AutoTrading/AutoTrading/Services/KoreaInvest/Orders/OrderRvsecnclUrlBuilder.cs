namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 주식주문 정정취소 API URL 생성기
    ///
    /// 엔드포인트: /uapi/domestic-stock/v1/trading/order-rvsecncl
    /// 현금 주문(order-cash)과 경로가 다르므로 별도 빌더로 분리한다.
    /// </summary>
    public static class OrderRvsecnclUrlBuilder
    {
        private const string Path = "/uapi/domestic-stock/v1/trading/order-rvsecncl";

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
