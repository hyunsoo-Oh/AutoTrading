namespace KisRestAPI.Models.Realtime
{
    /// <summary>
    /// 국내주식 실시간체결통보 데이터 [실시간-005]
    ///
    /// TR_ID: H0STCNI0 (실전) / H0STCNI9 (모의)
    /// ^로 구분된 26개 필드를 순서대로 파싱하여 담는다.
    ///
    /// 왜 체결통보는 다른 실시간 데이터와 다른가?
    /// - 체결통보는 **내 주문**에 대한 통보이다. (시세 데이터가 아님)
    /// - 암호화되어 수신된다. (AES-256-CBC 복호화 필요)
    /// - TR_ID가 실전/모의 환경에 따라 다르다. (H0STCNI0 / H0STCNI9)
    /// - tr_key가 종목코드가 아니라 HTS ID이다.
    /// - CNTG_YN 값으로 주문접수 통보(1)와 체결 통보(2)를 구분한다.
    /// </summary>
    public class RealtimeCcnlNotifyData
    {
        // ===== 고객 / 계좌 =====

        /// <summary>[0] 고객 ID</summary>
        public string CustId { get; set; } = string.Empty;

        /// <summary>[1] 계좌번호</summary>
        public string AcntNo { get; set; } = string.Empty;

        // ===== 주문 기본 =====

        /// <summary>[2] 주문번호</summary>
        public string OderNo { get; set; } = string.Empty;

        /// <summary>[3] 원주문번호 (정정/취소 시 원래 주문번호)</summary>
        public string OoderNo { get; set; } = string.Empty;

        /// <summary>[4] 매도매수구분 (01:매도 02:매수)</summary>
        public string SelnByovCls { get; set; } = string.Empty;

        /// <summary>[5] 정정구분 (0:정상 1:정정 2:취소)</summary>
        public string RctfCls { get; set; } = string.Empty;

        /// <summary>
        /// [6] 주문종류
        /// 00:지정가 01:시장가 02:조건부지정가 03:최유리지정가
        /// 04:최우선지정가 05:장전시간외 06:장후시간외 07:시간외단일가
        /// 11~16:IOC/FOK 21~24:중간가/스톱 등
        /// </summary>
        public string OderKind { get; set; } = string.Empty;

        /// <summary>[7] 주문조건 (0:없음 1:IOC 2:FOK)</summary>
        public string OderCond { get; set; } = string.Empty;

        // ===== 종목 / 체결 =====

        /// <summary>[8] 주식 단축 종목코드</summary>
        public string StckShrnIscd { get; set; } = string.Empty;

        /// <summary>[9] 체결 수량</summary>
        public string CntgQty { get; set; } = "0";

        /// <summary>[10] 체결단가</summary>
        public string CntgUnpr { get; set; } = "0";

        /// <summary>[11] 주식 체결 시간 (HHMMSS)</summary>
        public string StckCntgHour { get; set; } = string.Empty;

        // ===== 상태 구분 =====

        /// <summary>[12] 거부여부 (0:승인 1:거부)</summary>
        public string RfusYn { get; set; } = "0";

        /// <summary>
        /// [13] 체결여부 (1:주문/정정/취소/거부 접수 통보, 2:체결 통보)
        ///
        /// 왜 이 필드가 중요한가?
        /// - 체결통보 스트림에는 주문접수 통보와 체결 통보가 모두 섞여 온다.
        /// - 자동매매에서는 이 값을 기준으로 처리 로직을 분기해야 한다.
        /// </summary>
        public string CntgYn { get; set; } = string.Empty;

        /// <summary>[14] 접수여부 (1:주문접수 2:확인 3:취소(FOK/IOC))</summary>
        public string AcptYn { get; set; } = string.Empty;

        // ===== 지점 / 주문수량 =====

        /// <summary>[15] 지점번호</summary>
        public string BrncNo { get; set; } = string.Empty;

        /// <summary>[16] 주문수량</summary>
        public string OderQty { get; set; } = "0";

        /// <summary>[17] 계좌명</summary>
        public string AcntName { get; set; } = string.Empty;

        // ===== 호가조건 / 거래소 =====

        /// <summary>[18] 호가조건가격 (스톱지정가 시 표시)</summary>
        public string OrdCondPrc { get; set; } = "0";

        /// <summary>[19] 주문거래소 구분 (1:KRX 2:NXT 3:SOR-KRX 4:SOR-NXT)</summary>
        public string OrdExgGb { get; set; } = string.Empty;

        // ===== 기타 =====

        /// <summary>[20] 실시간체결창 표시여부 (Y/N)</summary>
        public string PopupYn { get; set; } = string.Empty;

        /// <summary>[21] 필러 (사용 안 함)</summary>
        public string Filler { get; set; } = string.Empty;

        /// <summary>[22] 신용구분</summary>
        public string CrdtCls { get; set; } = string.Empty;

        /// <summary>[23] 신용대출일자</summary>
        public string CrdtLoanDate { get; set; } = string.Empty;

        /// <summary>[24] 체결종목명</summary>
        public string CntgIsnm40 { get; set; } = string.Empty;

        /// <summary>[25] 주문가격</summary>
        public string OderPrc { get; set; } = "0";
    }
}
