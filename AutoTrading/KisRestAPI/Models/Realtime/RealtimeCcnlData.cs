namespace KisRestAPI.Models.Realtime
{
    /// <summary>
    /// 국내주식 실시간체결가 데이터 [실시간-003]
    ///
    /// TR_ID: H0STCNT0
    /// ^로 구분된 46개 필드를 순서대로 파싱하여 담는다.
    /// </summary>
    public class RealtimeCcnlData
    {
        // ===== 종목 / 체결 기본 =====

        /// <summary>[0] 유가증권 단축 종목코드</summary>
        public string StockCode { get; set; } = string.Empty;

        /// <summary>[1] 주식 체결 시간 (HHMMSS)</summary>
        public string CntgHour { get; set; } = string.Empty;

        /// <summary>[2] 주식 현재가 (체결가격)</summary>
        public string CurrentPrice { get; set; } = "0";

        /// <summary>[3] 전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>[4] 전일 대비</summary>
        public string PrdyVrss { get; set; } = "0";

        /// <summary>[5] 전일 대비율</summary>
        public string PrdyCtrt { get; set; } = "0";

        /// <summary>[6] 가중 평균 주식 가격</summary>
        public string WghnAvrgStckPrc { get; set; } = "0";

        // ===== 시가 / 고가 / 저가 =====

        /// <summary>[7] 주식 시가</summary>
        public string StckOprc { get; set; } = "0";

        /// <summary>[8] 주식 최고가</summary>
        public string StckHgpr { get; set; } = "0";

        /// <summary>[9] 주식 최저가</summary>
        public string StckLwpr { get; set; } = "0";

        // ===== 호가 =====

        /// <summary>[10] 매도호가1</summary>
        public string AskPrice1 { get; set; } = "0";

        /// <summary>[11] 매수호가1</summary>
        public string BidPrice1 { get; set; } = "0";

        // ===== 거래량 / 거래대금 =====

        /// <summary>[12] 체결 거래량</summary>
        public string CntgVol { get; set; } = "0";

        /// <summary>[13] 누적 거래량</summary>
        public string AcmlVol { get; set; } = "0";

        /// <summary>[14] 누적 거래 대금</summary>
        public string AcmlTrPbmn { get; set; } = "0";

        // ===== 매도/매수 체결 건수 =====

        /// <summary>[15] 매도 체결 건수</summary>
        public string SellCntgCount { get; set; } = "0";

        /// <summary>[16] 매수 체결 건수</summary>
        public string BuyCntgCount { get; set; } = "0";

        /// <summary>[17] 순매수 체결 건수</summary>
        public string NetBuyCntgCount { get; set; } = "0";

        /// <summary>[18] 체결강도</summary>
        public string Cttr { get; set; } = "0";

        /// <summary>[19] 총 매도 수량</summary>
        public string TotalSellQty { get; set; } = "0";

        /// <summary>[20] 총 매수 수량</summary>
        public string TotalBuyQty { get; set; } = "0";

        // ===== 체결 구분 / 매수비율 =====

        /// <summary>[21] 체결구분 (1:매수(+) 3:장전 5:매도(-))</summary>
        public string CcldDvsn { get; set; } = string.Empty;

        /// <summary>[22] 매수비율</summary>
        public string BuyRate { get; set; } = "0";

        /// <summary>[23] 전일 거래량 대비 등락율</summary>
        public string PrdyVolVrssRate { get; set; } = "0";

        // ===== 시가/고가/저가 대비 =====

        /// <summary>[24] 시가 시간</summary>
        public string OprcHour { get; set; } = string.Empty;

        /// <summary>[25] 시가대비구분 (1~5)</summary>
        public string OprcVrssPrprSign { get; set; } = string.Empty;

        /// <summary>[26] 시가대비</summary>
        public string OprcVrssPrpr { get; set; } = "0";

        /// <summary>[27] 최고가 시간</summary>
        public string HgprHour { get; set; } = string.Empty;

        /// <summary>[28] 고가대비구분 (1~5)</summary>
        public string HgprVrssPrprSign { get; set; } = string.Empty;

        /// <summary>[29] 고가대비</summary>
        public string HgprVrssPrpr { get; set; } = "0";

        /// <summary>[30] 최저가 시간</summary>
        public string LwprHour { get; set; } = string.Empty;

        /// <summary>[31] 저가대비구분 (1~5)</summary>
        public string LwprVrssPrprSign { get; set; } = string.Empty;

        /// <summary>[32] 저가대비</summary>
        public string LwprVrssPrpr { get; set; } = "0";

        // ===== 장운영 / 거래정지 =====

        /// <summary>[33] 영업 일자 (YYYYMMDD)</summary>
        public string BsopDate { get; set; } = string.Empty;

        /// <summary>[34] 신 장운영 구분 코드 (2자리)</summary>
        public string NewMkopClsCode { get; set; } = string.Empty;

        /// <summary>[35] 거래정지 여부 (Y/N)</summary>
        public string TrhtYn { get; set; } = "N";

        // ===== 호가 잔량 =====

        /// <summary>[36] 매도호가 잔량1</summary>
        public string AskQty1 { get; set; } = "0";

        /// <summary>[37] 매수호가 잔량1</summary>
        public string BidQty1 { get; set; } = "0";

        /// <summary>[38] 총 매도호가 잔량</summary>
        public string TotalAskQty { get; set; } = "0";

        /// <summary>[39] 총 매수호가 잔량</summary>
        public string TotalBidQty { get; set; } = "0";

        // ===== 기타 =====

        /// <summary>[40] 거래량 회전율</summary>
        public string VolTnrt { get; set; } = "0";

        /// <summary>[41] 전일 동시간 누적 거래량</summary>
        public string PrdySameHourAcmlVol { get; set; } = "0";

        /// <summary>[42] 전일 동시간 누적 거래량 비율</summary>
        public string PrdySameHourAcmlVolRate { get; set; } = "0";

        /// <summary>[43] 시간 구분 코드 (0:장중 A:장후예상 B:장전예상 C:VI D:시간외단일가)</summary>
        public string HourClsCode { get; set; } = string.Empty;

        /// <summary>[44] 임의종료구분코드</summary>
        public string MrktTrtmClsCode { get; set; } = string.Empty;

        /// <summary>[45] 정적VI발동기준가</summary>
        public string ViStndPrc { get; set; } = "0";
    }
}
