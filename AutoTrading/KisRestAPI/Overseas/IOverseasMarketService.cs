using KisRestAPI.Models.Overseas;

namespace KisRestAPI.Overseas
{
    /// <summary>
    /// 해외주식 시세 조회 API 기능을 정의하는 서비스 인터페이스
    /// </summary>
    public interface IOverseasMarketService
    {
        /// <summary>
        /// 해외주식 종목/지수/환율 기간별시세(일/주/월/년) 조회 [v1_해외주식-012]
        /// output1 : 현재 기본 정보 / output2 : 기간별 캔들 배열
        ///
        /// ※ 미국주식 조회 시 다우30·나스닥100·S&P500 종목만 조회 가능
        ///    더 많은 미국주식은 해외주식기간별시세 API 사용
        /// </summary>
        Task<InquireOvrsDailyChartPriceResponse> InquireOvrsDailyChartPriceAsync(
            InquireOvrsDailyChartPriceRequest request,
            CancellationToken cancellationToken = default);
    }
}
