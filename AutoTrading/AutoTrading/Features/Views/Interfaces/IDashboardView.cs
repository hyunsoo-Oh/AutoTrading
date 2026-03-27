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
    }
}
