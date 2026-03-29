using KisRestAPI.Models.Accounts;

namespace AutoTrading.Features.Views.Interfaces
{
    /// <summary>
    /// Dashboard View 인터페이스
    ///
    /// Presenter는 이 인터페이스를 통해서만 UI를 갱신한다.
    /// </summary>
    public interface IDashboardView
    {
        /// <summary>
        /// 잔고조회 결과로 카드 3개를 갱신한다.
        /// </summary>
        /// <param name="totalEvaluation">총평가금액</param>
        /// <param name="deposits">예수금</param>
        /// <param name="profitLoss">평가손익합계</param>
        /// <param name="purchaseAmount">매입금액합계 (수익률 계산용)</param>
        void UpdateBalanceSummary(decimal totalEvaluation, decimal deposits, decimal profitLoss, decimal purchaseAmount);

        /// <summary>
        /// 보유종목 그리드를 갱신한다.
        /// </summary>
        /// <param name="items">보유종목 목록 (output1)</param>
        /// <param name="stockEvalTotal">주식 평가금액 합계 — 비중(%) 계산용 분모</param>
        void UpdateHoldings(IReadOnlyList<InquireBalanceItem> items, decimal stockEvalTotal);

        /// <summary>
        /// StockCard 컨트롤을 갱신한다.
        /// </summary>
        /// <param name="cardId">카드 식별자 (kospi / snp / exchangeRate / interestRate)</param>
        /// <param name="indexName">지수/지표 이름 표시 텍스트</param>
        /// <param name="currentPrice">현재가</param>
        /// <param name="changePrice">전일 대비 변동 금액</param>
        /// <param name="changeRate">전일 대비 변동률 (%)</param>
        /// <param name="sparklinePoints">2주간 일별 종가 — 스파크라인 그래프용</param>
        void UpdateStockCard(
            string cardId,
            string indexName,
            double currentPrice,
            double changePrice,
            double changeRate,
            IReadOnlyList<double> sparklinePoints);
    }
}
