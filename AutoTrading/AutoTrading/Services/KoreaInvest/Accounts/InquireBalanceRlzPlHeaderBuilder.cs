using AutoTrading.Services.KoreaInvest.Common.Http;

namespace AutoTrading.Services.KoreaInvest.Accounts
{
    /// <summary>
    /// 주식잔고조회_실현손익 API용 헤더 생성 클래스
    ///
    /// tr_cont를 지원하는 이유:
    /// - 이 API는 데이터가 많을 경우 여러 페이지로 나뉘어 응답된다.
    /// - 첫 조회는 tr_cont를 공란으로, 다음 페이지 조회 시 "N"을 보낸다.
    /// </summary>
    public static class InquireBalanceRlzPlHeaderBuilder
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
                    ["tr_id"]   = trId,
                    // ===== 연속 조회 시 사용 =====
                    // 최초 조회 시 공란, 다음 페이지 조회 시 "N" 전달
                    ["tr_cont"] = trCont ?? string.Empty
                });
        }
    }
}
