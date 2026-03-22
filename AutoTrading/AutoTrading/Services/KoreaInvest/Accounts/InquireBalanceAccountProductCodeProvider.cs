using AutoTrading.Services.KoreaInvest.Common;

namespace AutoTrading.Services.KoreaInvest.Accounts
{
    /// <summary>
    /// 계좌상품코드(ACNT_PRDT_CD)를 거래 모드에 따라 반환
    /// 실전투자: "01" / 모의투자: "50"
    /// </summary>
    public static class InquireBalanceAccountProductCodeProvider
    {
        /// <summary>실전투자 상품코드</summary>
        private const string LiveProductCode = "01";
        /// <summary>모의투자 상품코드</summary>
        private const string MockProductCode = "50";

        /// <summary>
        /// 현재 거래 모드에 맞는 상품코드를 반환
        /// </summary>
        /// <param name="tradingMode">현재 거래 모드</param>
        /// <returns>ACNT_PRDT_CD 문자열</returns>
        public static string Get(KiaTradingMode tradingMode)
        {
            return tradingMode == KiaTradingMode.Live
                ? LiveProductCode
                : MockProductCode;
        }
    }
}
