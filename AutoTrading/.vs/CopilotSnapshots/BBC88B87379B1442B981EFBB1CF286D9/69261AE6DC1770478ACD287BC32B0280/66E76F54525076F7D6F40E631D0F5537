using AutoTrading.Services.KoreaInvest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Services.KoreaInvest.Accounts
{
    /// <summary>
    /// 주식잔고조회 API의 TR ID를 현재 거래 모드에 따라 반환
    /// </summary>
    public class InquireBalanceTrIdProvider
    {
        private const string MockTrId = "VTTC8434R";
        private const string LiveTrId = "TTTC8434R";

        /// <summary>
        /// 현재 거래 모드에 맞는 주식잔고조회 TR ID를 반환합니다.
        /// </summary>
        /// <param name="tradingMode">현재 거래 모드</param>
        /// <returns>TR ID 문자열</returns>
        public static string Get(KisTradingMode tradingMode)
        {
            return tradingMode == KisTradingMode.Live
                ? LiveTrId
                : MockTrId;
        }
    }
}
