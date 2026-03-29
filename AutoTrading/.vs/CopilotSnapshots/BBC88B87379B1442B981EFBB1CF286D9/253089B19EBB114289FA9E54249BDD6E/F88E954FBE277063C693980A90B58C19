using AutoTrading.Services.KoreaInvest.Common.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 주문 API용 헤더 생성 클래스
    /// </summary>
    public static class OrderCashHeaderBuilder
    {
        public static Dictionary<string, string> Build(
            string accessToken,
            string appKey,
            string appSecret,
            string trId,
            string custType = "P",
            string? trCont = null)
        {
            return KisHttpHeaderBuilder.BuildCommon(
                accessToken: accessToken,
                appKey: appKey,
                appSecret: appSecret,
                custType: custType,
                extraHeaders: new Dictionary<string, string>
                {
                    // ===== 도메인 전용 처리 =====
                    ["tr_id"] = trId,

                    // ===== 연속 조회가 필요한 API에서만 의미가 있음 =====
                    // 주문 API에서는 보통 비워도 되지만,
                    // 구조를 맞추기 위해 선택적으로 받을 수 있게 둔다.
                    ["tr_cont"] = trCont ?? string.Empty
                });
        }
    }
}
