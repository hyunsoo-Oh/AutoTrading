using AutoTrading.Services.KoreaInvest.Common;

namespace AutoTrading.Services.KoreaInvest.Accounts
{
    /// <summary>
    /// 투자계좌자산현황조회 API의 TR ID를 제공하는 정적 클래스
    ///
    /// ※ 이 API는 실전투자 전용이다. 모의투자는 지원하지 않는다.
    /// 모의투자 환경에서 호출하면 즉시 예외를 발생시켜 잘못된 호출을 방지한다.
    /// </summary>
    public static class InquireAccountBalanceTrIdProvider
    {
        private const string LiveTrId = "CTRP6548R";

        /// <summary>
        /// 현재 거래 모드에 맞는 TR ID를 반환
        /// </summary>
        /// <exception cref="NotSupportedException">모의투자 환경에서 호출 시 발생</exception>
        public static string Get(KisTradingMode tradingMode)
        {
            if (tradingMode != KisTradingMode.Live)
            {
                throw new NotSupportedException(
                    "투자계좌자산현황조회(CTRP6548R)는 실전투자만 지원합니다. " +
                    "모의투자 환경에서는 호출할 수 없습니다.");
            }

            return LiveTrId;
        }
    }
}
