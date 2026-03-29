using AutoTrading.Services.KoreaInvest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    public class InquireOrderTrIdProvider
    {
        private const string MockBuyTrId = "VTTC0012U";
        private const string LiveBuyTrId = "TTTC0012U";
        private const string MockSellTrId = "VTTC0011U";
        private const string LiveSellTrId = "TTTC0011U"; 

        public static string Get(KisTradingMode tradingMode, OrderSide order)
        {
            return tradingMode == KisTradingMode.Live
                ? order == OrderSide.Buy ? LiveBuyTrId : LiveSellTrId
                : order == OrderSide.Buy ? MockBuyTrId : MockSellTrId;
        }
    }
}
