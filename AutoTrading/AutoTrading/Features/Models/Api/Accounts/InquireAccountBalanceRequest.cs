namespace AutoTrading.Features.Models.Api.Accounts
{
    /// <summary>
    /// 투자계좌자산현황조회 Query Parameter DTO
    ///
    /// 왜 이 API가 필요한가?
    /// - 주식잔고조회(InquireBalance)는 보유 종목별 현황을 보여준다.
    /// - 이 API는 주식, 펀드, 채권, 해외주식 등 자산 종류별 비중과 금액을 한눈에 보여준다.
    /// - 포트폴리오 자산 배분 현황을 파악하고 리밸런싱 판단에 활용할 수 있다.
    ///
    /// ※ 실전투자 전용 API — 모의투자 미지원
    /// </summary>
    public sealed class InquireAccountBalanceRequest
    {
        /// <summary>종합계좌번호 앞 8자리</summary>
        public string CANO { get; set; } = string.Empty;

        /// <summary>계좌상품코드 뒤 2자리 (보통 "01")</summary>
        public string ACNT_PRDT_CD { get; set; } = "01";

        /// <summary>
        /// 조회구분1 — API 명세상 공란 입력
        /// </summary>
        public string INQR_DVSN_1 { get; set; } = string.Empty;

        /// <summary>
        /// 기준가이전일자 적용 여부 — API 명세상 공란 입력
        /// </summary>
        public string BSPR_BF_DT_APLY_YN { get; set; } = string.Empty;
    }
}
