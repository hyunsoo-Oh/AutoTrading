namespace KisRestAPI.Models.Realtime
{
    /// <summary>
    /// 국내주식 실시간호가 데이터 [실시간-004]
    ///
    /// TR_ID: H0STASP0
    /// ^로 구분된 59개 필드를 순서대로 파싱하여 담는다.
    ///
    /// 왜 모든 필드를 string으로 선언하는가?
    /// - 실시간 데이터는 ^로 구분된 텍스트 스트림이다.
    /// - 숫자 변환은 사용 시점(View/Presenter)에서 필요한 것만 수행한다.
    /// - 파싱 단계에서 변환 오류가 발생하면 전체 프레임을 잃게 되므로
    ///   안전하게 문자열로 보존한다.
    /// </summary>
    public class RealtimeAspData
    {
        // ===== 종목 / 시간 기본 =====

        /// <summary>[0] 유가증권 단축 종목코드</summary>
        public string StockCode { get; set; } = string.Empty;

        /// <summary>[1] 영업 시간 (HHMMSS)</summary>
        public string BsopHour { get; set; } = string.Empty;

        /// <summary>[2] 시간 구분 코드 (0:장중 A:장후예상 B:장전예상 C:9시이후예상/VI D:시간외단일가예상)</summary>
        public string HourClsCode { get; set; } = string.Empty;

        // ===== 매도호가 1~10 =====

        /// <summary>[3] 매도호가1</summary>
        public string AskP1 { get; set; } = "0";

        /// <summary>[4] 매도호가2</summary>
        public string AskP2 { get; set; } = "0";

        /// <summary>[5] 매도호가3</summary>
        public string AskP3 { get; set; } = "0";

        /// <summary>[6] 매도호가4</summary>
        public string AskP4 { get; set; } = "0";

        /// <summary>[7] 매도호가5</summary>
        public string AskP5 { get; set; } = "0";

        /// <summary>[8] 매도호가6</summary>
        public string AskP6 { get; set; } = "0";

        /// <summary>[9] 매도호가7</summary>
        public string AskP7 { get; set; } = "0";

        /// <summary>[10] 매도호가8</summary>
        public string AskP8 { get; set; } = "0";

        /// <summary>[11] 매도호가9</summary>
        public string AskP9 { get; set; } = "0";

        /// <summary>[12] 매도호가10</summary>
        public string AskP10 { get; set; } = "0";

        // ===== 매수호가 1~10 =====

        /// <summary>[13] 매수호가1</summary>
        public string BidP1 { get; set; } = "0";

        /// <summary>[14] 매수호가2</summary>
        public string BidP2 { get; set; } = "0";

        /// <summary>[15] 매수호가3</summary>
        public string BidP3 { get; set; } = "0";

        /// <summary>[16] 매수호가4</summary>
        public string BidP4 { get; set; } = "0";

        /// <summary>[17] 매수호가5</summary>
        public string BidP5 { get; set; } = "0";

        /// <summary>[18] 매수호가6</summary>
        public string BidP6 { get; set; } = "0";

        /// <summary>[19] 매수호가7</summary>
        public string BidP7 { get; set; } = "0";

        /// <summary>[20] 매수호가8</summary>
        public string BidP8 { get; set; } = "0";

        /// <summary>[21] 매수호가9</summary>
        public string BidP9 { get; set; } = "0";

        /// <summary>[22] 매수호가10</summary>
        public string BidP10 { get; set; } = "0";

        // ===== 매도호가 잔량 1~10 =====

        /// <summary>[23] 매도호가 잔량1</summary>
        public string AskPRsqn1 { get; set; } = "0";

        /// <summary>[24] 매도호가 잔량2</summary>
        public string AskPRsqn2 { get; set; } = "0";

        /// <summary>[25] 매도호가 잔량3</summary>
        public string AskPRsqn3 { get; set; } = "0";

        /// <summary>[26] 매도호가 잔량4</summary>
        public string AskPRsqn4 { get; set; } = "0";

        /// <summary>[27] 매도호가 잔량5</summary>
        public string AskPRsqn5 { get; set; } = "0";

        /// <summary>[28] 매도호가 잔량6</summary>
        public string AskPRsqn6 { get; set; } = "0";

        /// <summary>[29] 매도호가 잔량7</summary>
        public string AskPRsqn7 { get; set; } = "0";

        /// <summary>[30] 매도호가 잔량8</summary>
        public string AskPRsqn8 { get; set; } = "0";

        /// <summary>[31] 매도호가 잔량9</summary>
        public string AskPRsqn9 { get; set; } = "0";

        /// <summary>[32] 매도호가 잔량10</summary>
        public string AskPRsqn10 { get; set; } = "0";

        // ===== 매수호가 잔량 1~10 =====

        /// <summary>[33] 매수호가 잔량1</summary>
        public string BidPRsqn1 { get; set; } = "0";

        /// <summary>[34] 매수호가 잔량2</summary>
        public string BidPRsqn2 { get; set; } = "0";

        /// <summary>[35] 매수호가 잔량3</summary>
        public string BidPRsqn3 { get; set; } = "0";

        /// <summary>[36] 매수호가 잔량4</summary>
        public string BidPRsqn4 { get; set; } = "0";

        /// <summary>[37] 매수호가 잔량5</summary>
        public string BidPRsqn5 { get; set; } = "0";

        /// <summary>[38] 매수호가 잔량6</summary>
        public string BidPRsqn6 { get; set; } = "0";

        /// <summary>[39] 매수호가 잔량7</summary>
        public string BidPRsqn7 { get; set; } = "0";

        /// <summary>[40] 매수호가 잔량8</summary>
        public string BidPRsqn8 { get; set; } = "0";

        /// <summary>[41] 매수호가 잔량9</summary>
        public string BidPRsqn9 { get; set; } = "0";

        /// <summary>[42] 매수호가 잔량10</summary>
        public string BidPRsqn10 { get; set; } = "0";

        // ===== 총 잔량 =====

        /// <summary>[43] 총 매도호가 잔량</summary>
        public string TotalAskPRsqn { get; set; } = "0";

        /// <summary>[44] 총 매수호가 잔량</summary>
        public string TotalBidPRsqn { get; set; } = "0";

        /// <summary>[45] 시간외 총 매도호가 잔량</summary>
        public string OvtmTotalAskPRsqn { get; set; } = "0";

        /// <summary>[46] 시간외 총 매수호가 잔량</summary>
        public string OvtmTotalBidPRsqn { get; set; } = "0";

        // ===== 예상 체결 =====

        /// <summary>[47] 예상 체결가 (동시호가 등 특정 조건하에서만 발생)</summary>
        public string AntcCnpr { get; set; } = "0";

        /// <summary>[48] 예상 체결량 (동시호가 등 특정 조건하에서만 발생)</summary>
        public string AntcCnqn { get; set; } = "0";

        /// <summary>[49] 예상 거래량 (동시호가 등 특정 조건하에서만 발생)</summary>
        public string AntcVol { get; set; } = "0";

        /// <summary>[50] 예상 체결 대비 (동시호가 등 특정 조건하에서만 발생)</summary>
        public string AntcCntgVrss { get; set; } = "0";

        /// <summary>[51] 예상 체결 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        public string AntcCntgVrssSign { get; set; } = string.Empty;

        /// <summary>[52] 예상 체결 전일 대비율</summary>
        public string AntcCntgPrdyCtrt { get; set; } = "0";

        // ===== 누적 거래량 =====

        /// <summary>[53] 누적 거래량</summary>
        public string AcmlVol { get; set; } = "0";

        // ===== 잔량 증감 =====

        /// <summary>[54] 총 매도호가 잔량 증감</summary>
        public string TotalAskPRsqnIcdc { get; set; } = "0";

        /// <summary>[55] 총 매수호가 잔량 증감</summary>
        public string TotalBidPRsqnIcdc { get; set; } = "0";

        /// <summary>[56] 시간외 총 매도호가 증감</summary>
        public string OvtmTotalAskPIcdc { get; set; } = "0";

        /// <summary>[57] 시간외 총 매수호가 증감</summary>
        public string OvtmTotalBidPIcdc { get; set; } = "0";

        // ===== 매매 구분 =====

        /// <summary>[58] 주식 매매 구분 코드 (사용 X — 삭제된 값)</summary>
        public string StckDealClsCode { get; set; } = string.Empty;
    }
}
