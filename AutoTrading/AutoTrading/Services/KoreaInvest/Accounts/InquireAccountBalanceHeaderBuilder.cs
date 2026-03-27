using AutoTrading.Services.KoreaInvest.Common.Http;

namespace AutoTrading.Services.KoreaInvest.Accounts
{
    /// <summary>
    /// 투자계좌자산현황조회 API용 헤더 생성 클래스
    ///
    /// tr_cont를 지원하는 이유:
    /// - 응답 tr_cont가 'M'이면 다음 페이지 데이터가 존재한다.
    /// - 다음 페이지 조회 시 요청 tr_cont에 'N'을 전달한다.
    /// </summary>
    public static class InquireAccountBalanceHeaderBuilder
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
                    // ===== 연속 조회 =====
                    // 최초 조회는 공란, 다음 페이지 조회 시 "N" 전달
                    ["tr_cont"] = trCont ?? string.Empty
                });
        }
    }
}
