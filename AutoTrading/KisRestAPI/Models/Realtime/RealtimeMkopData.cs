namespace KisRestAPI.Models.Realtime
{
    /// <summary>
    /// 국내주식 장운영정보 (KRX) 데이터 [실시간-049]
    ///
    /// TR_ID: H0STMKO0 (실전 전용, 모의투자 미지원)
    /// ^로 구분된 11개 필드를 순서대로 파싱하여 담는다.
    ///
    /// 왜 이 데이터가 중요한가?
    /// - 장운영정보는 장 상태 변화(장개시·장마감·VI발동/해제·서킷브레이크 등)를
    ///   실시간으로 알려주는 이벤트성 데이터이다.
    /// - 자동매매 프로그램에서는 장 상태에 따라 주문 전략을 변경하거나
    ///   매매를 중단/재개하는 등의 핵심 로직에 활용한다.
    /// - 실전 전용 API이므로 모의투자 환경에서는 구독할 수 없다.
    ///
    /// 왜 종목 단위 구독인가?
    /// - 장운영정보는 tr_key로 종목코드를 지정하여 구독한다.
    /// - 해당 종목의 VI 발동/해제 시, 그리고 장 전체 상태 변화 시 수신된다.
    /// </summary>
    public class RealtimeMkopData
    {
        // ===== 종목 코드 =====

        /// <summary>[0] 유가증권 단축 종목코드</summary>
        public string StockCode { get; set; } = string.Empty;

        // ===== 거래정지 =====

        /// <summary>[1] 거래정지 여부 (Y/N)</summary>
        public string TrhtYn { get; set; } = "N";

        /// <summary>[2] 거래정지 사유 내용</summary>
        public string TrSuspReasCntt { get; set; } = string.Empty;

        // ===== 장운영 구분 =====

        /// <summary>
        /// [3] 장운영 구분 코드 (3자리)
        ///
        /// 주요 코드:
        /// 110=장전 동시호가 개시, 112=장개시, 121=장후 동시호가 개시, 129=장마감
        /// 130=장개시전시간외개시, 139=장개시전시간외종료
        /// 140=시간외 종가 매매 개시, 149=시간외 종가 매매 종료
        /// 150=시간외 단일가 매매 개시, 159=시간외 단일가 매매 종료
        /// 164=시장임시정지, 174=서킷브레이크 발동, 175=서킷브레이크 해제
        /// 387=사이드카 매도발동, 388=사이드카 매도발동해제
        /// 397=사이드카 매수발동, 398=사이드카 매수발동해제
        /// F01=장개시 10초전, F06=장개시 1분전 등
        /// </summary>
        public string MkopClsCode { get; set; } = string.Empty;

        /// <summary>
        /// [4] 예상 장운영 구분 코드 (3자리)
        ///
        /// 주요 코드:
        /// 112=장전예상종료, 121=장후예상시작, 129=장후예상종료, 311=장전예상시작
        /// </summary>
        public string AntcMkopClsCode { get; set; } = string.Empty;

        // ===== 임의연장 / 배분 =====

        /// <summary>
        /// [5] 임의연장 구분 코드 (1자리)
        ///
        /// 1=시초동시 임의종료 지정, 2=시초동시 임의종료 해제
        /// 3=마감동시 임의종료 지정, 4=마감동시 임의종료 해제
        /// 5=시간외단일가 임의종료 지정, 6=시간외단일가 임의종료 해제
        /// </summary>
        public string MrktTrtmClsCode { get; set; } = string.Empty;

        /// <summary>
        /// [6] 동시호가 배분처리 구분 코드 (2자리)
        ///
        /// 첫째 자리: 1=배분개시, 2=배분해제
        /// 둘째 자리: 1=매수상한, 2=매수하한, 3=매도상한, 4=매도하한
        /// </summary>
        public string DiviAppClsCode { get; set; } = string.Empty;

        // ===== 종목 상태 =====

        /// <summary>
        /// [7] 종목 상태 구분 코드 (2자리)
        ///
        /// 51=관리종목, 52=투자위험, 53=투자경고, 54=투자주의
        /// 55=당사 신용가능, 57=증거금률100, 58=거래정지, 59=단기과열, 00=그 외
        /// </summary>
        public string IscdStatClsCode { get; set; } = string.Empty;

        // ===== VI 적용 =====

        /// <summary>
        /// [8] VI 적용 구분 코드 (Y/N)
        ///
        /// Y=VI 적용된 종목, N=VI 적용되지 않은 종목
        /// </summary>
        public string ViClsCode { get; set; } = "N";

        /// <summary>
        /// [9] 시간외 단일가 VI 적용 구분 코드 (Y/N)
        ///
        /// Y=시간외단일가 VI 적용, N=적용되지 않음
        /// </summary>
        public string OvtmViClsCode { get; set; } = "N";

        // ===== 거래소 =====

        /// <summary>[10] 거래소 구분 코드</summary>
        public string ExchClsCode { get; set; } = string.Empty;
    }
}
