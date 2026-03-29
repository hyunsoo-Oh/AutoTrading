namespace KisRestAPI.Models.Realtime
{
    /// <summary>
    /// 국내주식 실시간회원사 (KRX) 데이터 [실시간-047]
    ///
    /// TR_ID: H0STMBC0 (실전 전용, 모의투자 미지원)
    /// ^로 구분된 78개 필드를 순서대로 파싱하여 담는다.
    ///
    /// 왜 별도 DTO인가?
    /// - 실시간회원사(H0STMBC0)는 거래원(회원사) 정보를 실시간으로 제공한다.
    /// - 매도/매수 상위 5개 회원사의 이름·수량·비중·증감·코드·구분 등을 포함한다.
    /// - 외국계 합산 정보(순매수·비중)도 별도 필드로 제공한다.
    /// - 실전 전용 API이므로 모의투자 환경에서는 구독할 수 없다.
    /// </summary>
    public class RealtimeMbcrData
    {
        // ===== 종목 코드 =====

        /// <summary>[0] 유가증권 단축 종목코드</summary>
        public string StockCode { get; set; } = string.Empty;

        // ===== 매도 회원사명 1~5 (한글) =====

        /// <summary>[1] 매도2회원사명1</summary>
        public string SelnMbcrName1 { get; set; } = string.Empty;

        /// <summary>[2] 매도2회원사명2</summary>
        public string SelnMbcrName2 { get; set; } = string.Empty;

        /// <summary>[3] 매도2회원사명3</summary>
        public string SelnMbcrName3 { get; set; } = string.Empty;

        /// <summary>[4] 매도2회원사명4</summary>
        public string SelnMbcrName4 { get; set; } = string.Empty;

        /// <summary>[5] 매도2회원사명5</summary>
        public string SelnMbcrName5 { get; set; } = string.Empty;

        // ===== 매수 회원사명 1~5 (한글) =====

        /// <summary>[6] 매수회원사명1</summary>
        public string ByovMbcrName1 { get; set; } = string.Empty;

        /// <summary>[7] 매수회원사명2</summary>
        public string ByovMbcrName2 { get; set; } = string.Empty;

        /// <summary>[8] 매수회원사명3</summary>
        public string ByovMbcrName3 { get; set; } = string.Empty;

        /// <summary>[9] 매수회원사명4</summary>
        public string ByovMbcrName4 { get; set; } = string.Empty;

        /// <summary>[10] 매수회원사명5</summary>
        public string ByovMbcrName5 { get; set; } = string.Empty;

        // ===== 총 매도 수량 1~5 =====

        /// <summary>[11] 총매도수량1</summary>
        public string TotalSelnQty1 { get; set; } = "0";

        /// <summary>[12] 총매도수량2</summary>
        public string TotalSelnQty2 { get; set; } = "0";

        /// <summary>[13] 총매도수량3</summary>
        public string TotalSelnQty3 { get; set; } = "0";

        /// <summary>[14] 총매도수량4</summary>
        public string TotalSelnQty4 { get; set; } = "0";

        /// <summary>[15] 총매도수량5</summary>
        public string TotalSelnQty5 { get; set; } = "0";

        // ===== 총 매수 수량 1~5 =====

        /// <summary>[16] 총매수2수량1</summary>
        public string TotalShnuQty1 { get; set; } = "0";

        /// <summary>[17] 총매수2수량2</summary>
        public string TotalShnuQty2 { get; set; } = "0";

        /// <summary>[18] 총매수2수량3</summary>
        public string TotalShnuQty3 { get; set; } = "0";

        /// <summary>[19] 총매수2수량4</summary>
        public string TotalShnuQty4 { get; set; } = "0";

        /// <summary>[20] 총매수2수량5</summary>
        public string TotalShnuQty5 { get; set; } = "0";

        // ===== 매도 거래원 구분 1~5 =====

        /// <summary>[21] 매도거래원구분1</summary>
        public string SelnMbcrGlobYn1 { get; set; } = string.Empty;

        /// <summary>[22] 매도거래원구분2</summary>
        public string SelnMbcrGlobYn2 { get; set; } = string.Empty;

        /// <summary>[23] 매도거래원구분3</summary>
        public string SelnMbcrGlobYn3 { get; set; } = string.Empty;

        /// <summary>[24] 매도거래원구분4</summary>
        public string SelnMbcrGlobYn4 { get; set; } = string.Empty;

        /// <summary>[25] 매도거래원구분5</summary>
        public string SelnMbcrGlobYn5 { get; set; } = string.Empty;

        // ===== 매수 거래원 구분 1~5 =====

        /// <summary>[26] 매수거래원구분1</summary>
        public string ShnuMbcrGlobYn1 { get; set; } = string.Empty;

        /// <summary>[27] 매수거래원구분2</summary>
        public string ShnuMbcrGlobYn2 { get; set; } = string.Empty;

        /// <summary>[28] 매수거래원구분3</summary>
        public string ShnuMbcrGlobYn3 { get; set; } = string.Empty;

        /// <summary>[29] 매수거래원구분4</summary>
        public string ShnuMbcrGlobYn4 { get; set; } = string.Empty;

        /// <summary>[30] 매수거래원구분5</summary>
        public string ShnuMbcrGlobYn5 { get; set; } = string.Empty;

        // ===== 매도 거래원 코드 1~5 =====

        /// <summary>[31] 매도거래원코드1</summary>
        public string SelnMbcrNo1 { get; set; } = string.Empty;

        /// <summary>[32] 매도거래원코드2</summary>
        public string SelnMbcrNo2 { get; set; } = string.Empty;

        /// <summary>[33] 매도거래원코드3</summary>
        public string SelnMbcrNo3 { get; set; } = string.Empty;

        /// <summary>[34] 매도거래원코드4</summary>
        public string SelnMbcrNo4 { get; set; } = string.Empty;

        /// <summary>[35] 매도거래원코드5</summary>
        public string SelnMbcrNo5 { get; set; } = string.Empty;

        // ===== 매수 거래원 코드 1~5 =====

        /// <summary>[36] 매수거래원코드1</summary>
        public string ShnuMbcrNo1 { get; set; } = string.Empty;

        /// <summary>[37] 매수거래원코드2</summary>
        public string ShnuMbcrNo2 { get; set; } = string.Empty;

        /// <summary>[38] 매수거래원코드3</summary>
        public string ShnuMbcrNo3 { get; set; } = string.Empty;

        /// <summary>[39] 매수거래원코드4</summary>
        public string ShnuMbcrNo4 { get; set; } = string.Empty;

        /// <summary>[40] 매수거래원코드5</summary>
        public string ShnuMbcrNo5 { get; set; } = string.Empty;

        // ===== 매도 회원사 비중 1~5 =====

        /// <summary>[41] 매도회원사비중1</summary>
        public string SelnMbcrRlim1 { get; set; } = "0";

        /// <summary>[42] 매도회원사비중2</summary>
        public string SelnMbcrRlim2 { get; set; } = "0";

        /// <summary>[43] 매도회원사비중3</summary>
        public string SelnMbcrRlim3 { get; set; } = "0";

        /// <summary>[44] 매도회원사비중4</summary>
        public string SelnMbcrRlim4 { get; set; } = "0";

        /// <summary>[45] 매도회원사비중5</summary>
        public string SelnMbcrRlim5 { get; set; } = "0";

        // ===== 매수 회원사 비중 1~5 =====

        /// <summary>[46] 매수2회원사비중1</summary>
        public string ShnuMbcrRlim1 { get; set; } = "0";

        /// <summary>[47] 매수2회원사비중2</summary>
        public string ShnuMbcrRlim2 { get; set; } = "0";

        /// <summary>[48] 매수2회원사비중3</summary>
        public string ShnuMbcrRlim3 { get; set; } = "0";

        /// <summary>[49] 매수2회원사비중4</summary>
        public string ShnuMbcrRlim4 { get; set; } = "0";

        /// <summary>[50] 매수2회원사비중5</summary>
        public string ShnuMbcrRlim5 { get; set; } = "0";

        // ===== 매도 수량 증감 1~5 =====

        /// <summary>[51] 매도수량증감1</summary>
        public string SelnQtyIcdc1 { get; set; } = "0";

        /// <summary>[52] 매도수량증감2</summary>
        public string SelnQtyIcdc2 { get; set; } = "0";

        /// <summary>[53] 매도수량증감3</summary>
        public string SelnQtyIcdc3 { get; set; } = "0";

        /// <summary>[54] 매도수량증감4</summary>
        public string SelnQtyIcdc4 { get; set; } = "0";

        /// <summary>[55] 매도수량증감5</summary>
        public string SelnQtyIcdc5 { get; set; } = "0";

        // ===== 매수 수량 증감 1~5 =====

        /// <summary>[56] 매수2수량증감1</summary>
        public string ShnuQtyIcdc1 { get; set; } = "0";

        /// <summary>[57] 매수2수량증감2</summary>
        public string ShnuQtyIcdc2 { get; set; } = "0";

        /// <summary>[58] 매수2수량증감3</summary>
        public string ShnuQtyIcdc3 { get; set; } = "0";

        /// <summary>[59] 매수2수량증감4</summary>
        public string ShnuQtyIcdc4 { get; set; } = "0";

        /// <summary>[60] 매수2수량증감5</summary>
        public string ShnuQtyIcdc5 { get; set; } = "0";

        // ===== 외국계 합산 =====

        /// <summary>[61] 외국계총매도수량</summary>
        public string GlobTotalSelnQty { get; set; } = "0";

        /// <summary>[62] 외국계총매수2수량</summary>
        public string GlobTotalShnuQty { get; set; } = "0";

        /// <summary>[63] 외국계총매도수량증감</summary>
        public string GlobTotalSelnQtyIcdc { get; set; } = "0";

        /// <summary>[64] 외국계총매수2수량증감</summary>
        public string GlobTotalShnuQtyIcdc { get; set; } = "0";

        /// <summary>[65] 외국계순매수수량</summary>
        public string GlobNtbyQty { get; set; } = "0";

        /// <summary>[66] 외국계매도비중</summary>
        public string GlobSelnRlim { get; set; } = "0";

        /// <summary>[67] 외국계매수2비중</summary>
        public string GlobShnuRlim { get; set; } = "0";

        // ===== 매도 영문 회원사명 1~5 =====

        /// <summary>[68] 매도2영문회원사명1</summary>
        public string SelnMbcrEngName1 { get; set; } = string.Empty;

        /// <summary>[69] 매도2영문회원사명2</summary>
        public string SelnMbcrEngName2 { get; set; } = string.Empty;

        /// <summary>[70] 매도2영문회원사명3</summary>
        public string SelnMbcrEngName3 { get; set; } = string.Empty;

        /// <summary>[71] 매도2영문회원사명4</summary>
        public string SelnMbcrEngName4 { get; set; } = string.Empty;

        /// <summary>[72] 매도2영문회원사명5</summary>
        public string SelnMbcrEngName5 { get; set; } = string.Empty;

        // ===== 매수 영문 회원사명 1~5 =====

        /// <summary>[73] 매수영문회원사명1</summary>
        public string ByovMbcrEngName1 { get; set; } = string.Empty;

        /// <summary>[74] 매수영문회원사명2</summary>
        public string ByovMbcrEngName2 { get; set; } = string.Empty;

        /// <summary>[75] 매수영문회원사명3</summary>
        public string ByovMbcrEngName3 { get; set; } = string.Empty;

        /// <summary>[76] 매수영문회원사명4</summary>
        public string ByovMbcrEngName4 { get; set; } = string.Empty;

        /// <summary>[77] 매수영문회원사명5</summary>
        public string ByovMbcrEngName5 { get; set; } = string.Empty;
    }
}
