using AutoTrading.Services.KoreaInvest.Common;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 주식주문 정정취소 API의 TR ID를 제공하는 정적 클래스
    ///
    /// 왜 TR ID를 분리하는가?
    /// - 한국투자증권 API는 실전/모의에 따라 TR ID가 다르다.
    /// - 정정취소는 매수/매도 구분 없이 단일 TR ID를 사용한다.
    /// - 실전: TTTC0013U, 모의: VTTC0013U
    /// </summary>
    public static class OrderRvsecnclTrIdProvider
    {
        private const string LiveTrId = "TTTC0013U";
        private const string MockTrId = "VTTC0013U";

        /// <summary>
        /// 현재 거래 모드에 맞는 TR ID를 반환
        /// </summary>
        public static string Get(KisTradingMode tradingMode)
        {
            return tradingMode == KisTradingMode.Live ? LiveTrId : MockTrId;
        }
    }
}
