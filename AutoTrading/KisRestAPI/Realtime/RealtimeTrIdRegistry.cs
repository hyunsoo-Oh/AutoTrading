namespace KisRestAPI.Realtime
{
    /// <summary>
    /// 실시간 WebSocket TR_ID 상수 레지스트리
    ///
    /// 확인된 TR_ID만 등록한다.
    /// 새로운 실시간 구독 유형을 추가할 때 이 클래스에 상수를 추가하면 된다.
    /// </summary>
    internal static class RealtimeTrIdRegistry
    {
        // =====================================================================
        // ===== 확인된 TR_ID =====
        // =====================================================================

        /// <summary>국내주식 실시간체결가 [실시간-003] — 실전/모의 동일</summary>
        public const string DomesticCcnl = "H0STCNT0";

        /// <summary>국내주식 실시간호가 [실시간-004] — 실전/모의 동일</summary>
        public const string DomesticAsp = "H0STASP0";

        /// <summary>국내주식 실시간체결통보 [실시간-005] — 실전 전용</summary>
        public const string CcnlNotifyLive = "H0STCNI0";

        /// <summary>국내주식 실시간체결통보 [실시간-005] — 모의투자 전용</summary>
        public const string CcnlNotifyMock = "H0STCNI9";

        /// <summary>
        /// 국내주식 실시간예상체결 [실시간-041] — 실전 전용 (모의투자 미지원)
        ///
        /// 왜 실전 전용인가?
        /// - 예상체결은 동시호가 등 특수 시간대의 예상 체결 데이터를 제공한다.
        /// - 한국투자증권에서 모의투자 환경에서는 이 데이터를 지원하지 않는다.
        /// </summary>
        public const string DomesticAntc = "H0STANC0";

        /// <summary>
        /// 국내주식 실시간회원사 (KRX) [실시간-047] — 실전 전용 (모의투자 미지원)
        ///
        /// 왜 실전 전용인가?
        /// - 회원사(거래원) 정보는 KRX에서 제공하는 실전 전용 데이터이다.
        /// - 한국투자증권에서 모의투자 환경에서는 이 데이터를 지원하지 않는다.
        /// </summary>
        public const string DomesticMbcr = "H0STMBC0";

        /// <summary>
        /// 국내주식 장운영정보 (KRX) [실시간-049] — 실전 전용 (모의투자 미지원)
        ///
        /// 왜 실전 전용인가?
        /// - 장운영정보(VI 발동/해제, 장개시/마감 등)는 KRX 실시간 데이터이다.
        /// - 한국투자증권에서 모의투자 환경에서는 이 데이터를 지원하지 않는다.
        /// </summary>
        public const string DomesticMkop = "H0STMKO0";

        /// <summary>
        /// 국내지수 실시간체결 [실시간-026] — 실전 전용 (모의투자 미지원)
        ///
        /// 왜 실전 전용인가?
        /// - 국내지수 실시간체결은 KRX 실시간 데이터이다.
        /// - 한국투자증권에서 모의투자 환경에서는 이 데이터를 지원하지 않는다.
        ///
        /// 왜 기존 체결가(DomesticCcnl)와 별도인가?
        /// - DomesticCcnl(H0STCNT0)은 개별 종목(6자리 종목코드) 체결이고,
        ///   DomesticIndexCcnl(H0UPCNT0)은 업종지수(업종구분코드) 체결이다.
        /// - tr_key 형식과 응답 필드 구조가 완전히 다르다.
        /// </summary>
        public const string DomesticIndexCcnl = "H0UPCNT0";

        /// <summary>
        /// 국내지수 실시간예상체결 [실시간-027] — 실전 전용 (모의투자 미지원)
        ///
        /// 왜 실전 전용인가?
        /// - 국내지수 실시간예상체결은 KRX 실시간 데이터이다.
        /// - 한국투자증권에서 모의투자 환경에서는 이 데이터를 지원하지 않는다.
        ///
        /// 왜 DomesticIndexCcnl과 별도인가?
        /// - DomesticIndexCcnl(H0UPCNT0)은 장중 실시간 체결이고,
        ///   DomesticIndexAntc(H0UPANC0)은 동시호가 등 예상 체결이다.
        /// - 수신 시점과 데이터의 의미가 다르다.
        /// </summary>
        public const string DomesticIndexAntc = "H0UPANC0";

        /// <summary>
        /// 국내지수 실시간프로그램매매 TR_ID (실전 전용)
        ///
        /// 왜 실전 전용인가?
        /// - 국내지수 실시간프로그램매매는 KRX 실시간 데이터이다.
        /// - 한국투자증권에서 모의투자 환경에서는 이 데이터를 지원하지 않는다.
        ///
        /// 왜 별도 상수인가?
        /// - 프로그램매매(H0UPPGM0)는 차익/비차익 매도·매수 현황(88개 필드)을 제공하며,
        ///   지수체결(H0UPCNT0)이나 지수예상체결(H0UPANC0)과 데이터 구조가 완전히 다르다.
        /// </summary>
        public const string DomesticIndexPgm = "H0UPPGM0";

        /// <summary>
        /// 해당 TR_ID가 암호화 대상인지 판단한다.
        ///
        /// 왜 체결통보만 암호화인가?
        /// - 체결통보에는 고객ID, 계좌번호, 주문번호 등 민감 정보가 포함된다.
        /// - 한국투자증권은 이 데이터를 AES-256-CBC로 암호화하여 전송한다.
        /// - 구독 응답에서 받은 IV/Key로 복호화해야 한다.
        /// </summary>
        public static bool IsEncrypted(string trId)
        {
            return trId == CcnlNotifyLive || trId == CcnlNotifyMock;
        }
    }
}
