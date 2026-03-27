using AutoTrading.Services.KoreaInvest.Common.Http;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 매도가능수량조회 API용 헤더 생성 클래스
    /// </summary>
    public static class InquirePsblSellHeaderBuilder
    {
        public static Dictionary<string, string> Build(
            string accessToken,
            string appKey,
            string appSecret,
            string trId,
            string custType = "P")
        {
            return KisHttpHeaderBuilder.BuildCommon(
                accessToken: accessToken,
                appKey: appKey,
                appSecret: appSecret,
                custType: custType,
                extraHeaders: new Dictionary<string, string>
                {
                    // ===== 매도가능수량조회 전용 TR ID =====
                    ["tr_id"] = trId
                    // ※ tr_cont를 이용한 다음 조회 불가 API이므로 tr_cont 생략
                });
        }
    }
}
