using KisRestAPI.Models.Realtime;

namespace KisRestAPI.Realtime
{
    /// <summary>
    /// 한국투자증권 WebSocket 실시간 데이터 서비스 인터페이스
    ///
    /// 현재 구현:
    /// - 국내주식 실시간체결가   [실시간-003] (H0STCNT0)
    /// - 국내주식 실시간호가     [실시간-004] (H0STASP0)
    /// - 국내주식 실시간체결통보 [실시간-005] (H0STCNI0/H0STCNI9)
    /// - 국내주식 실시간예상체결 [실시간-041] (H0STANC0, 실전 전용)
    /// - 국내주식 실시간회원사   [실시간-047] (H0STMBC0, 실전 전용)
    /// - 국내주식 장운영정보     [실시간-049] (H0STMKO0, 실전 전용)
    /// - 국내지수 실시간체결       [실시간-026] (H0UPCNT0, 실전 전용)
    /// - 국내지수 실시간예상체결   [실시간-027] (H0UPANC0, 실전 전용)
    /// - 국내지수 실시간프로그램매매 [실시간-028] (H0UPPGM0, 실전 전용)
    /// </summary>
    public interface IRealtimeService : IDisposable
    {
        // ===== 연결 관리 =====

        /// <summary>WebSocket 서버에 연결한다 (Approval Key 발급 포함)</summary>
        Task ConnectAsync(CancellationToken cancellationToken = default);

        /// <summary>WebSocket 연결을 해제한다</summary>
        Task DisconnectAsync();

        /// <summary>현재 연결 상태</summary>
        bool IsConnected { get; }

        // ===== 국내주식 실시간체결가 [실시간-003] =====

        /// <summary>실시간체결가 구독 등록 (TR_ID: H0STCNT0)</summary>
        Task SubscribeCcnlAsync(string stockCode, CancellationToken cancellationToken = default);

        /// <summary>실시간체결가 구독 해제</summary>
        Task UnsubscribeCcnlAsync(string stockCode, CancellationToken cancellationToken = default);

        /// <summary>실시간체결가 데이터 수신 이벤트</summary>
        event EventHandler<RealtimeCcnlData> CcnlReceived;

        // ===== 국내주식 실시간호가 [실시간-004] =====

        /// <summary>실시간호가 구독 등록 (TR_ID: H0STASP0)</summary>
        Task SubscribeAspAsync(string stockCode, CancellationToken cancellationToken = default);

        /// <summary>실시간호가 구독 해제</summary>
        Task UnsubscribeAspAsync(string stockCode, CancellationToken cancellationToken = default);

        /// <summary>실시간호가 데이터 수신 이벤트</summary>
        event EventHandler<RealtimeAspData> AspReceived;

        // ===== 국내주식 실시간체결통보 [실시간-005] =====

        /// <summary>
        /// 실시간체결통보 구독 등록
        /// TR_ID: H0STCNI0 (실전) / H0STCNI9 (모의) — 환경에 따라 자동 선택
        ///
        /// 왜 htsId를 받는가?
        /// - 체결통보는 종목이 아니라 HTS ID 단위로 구독한다.
        /// - 내 주문에 대한 모든 통보(주문접수/체결/거부)가 이 채널로 온다.
        /// </summary>
        Task SubscribeCcnlNotifyAsync(string htsId, CancellationToken cancellationToken = default);

        /// <summary>실시간체결통보 구독 해제</summary>
        Task UnsubscribeCcnlNotifyAsync(string htsId, CancellationToken cancellationToken = default);

        /// <summary>실시간체결통보 데이터 수신 이벤트 (암호화 데이터 복호화 후 발생)</summary>
        event EventHandler<RealtimeCcnlNotifyData> CcnlNotifyReceived;

        // ===== 국내주식 실시간예상체결 [실시간-041] =====

        /// <summary>
        /// 실시간예상체결 구독 등록 (TR_ID: H0STANC0)
        ///
        /// ※ 실전 전용 — 모의투자 환경에서 호출하면 예외가 발생한다.
        /// 예상체결은 동시호가 등 특수 시간대의 예상 체결 데이터를 제공한다.
        /// </summary>
        Task SubscribeAntcAsync(string stockCode, CancellationToken cancellationToken = default);

        /// <summary>실시간예상체결 구독 해제</summary>
        Task UnsubscribeAntcAsync(string stockCode, CancellationToken cancellationToken = default);

        /// <summary>실시간예상체결 데이터 수신 이벤트</summary>
        event EventHandler<RealtimeAntcData> AntcReceived;

        // ===== 국내주식 실시간회원사 [실시간-047] =====

        /// <summary>
        /// 실시간회원사 구독 등록 (TR_ID: H0STMBC0)
        ///
        /// ※ 실전 전용 — 모의투자 환경에서 호출하면 예외가 발생한다.
        /// 거래원(회원사) 매도/매수 상위 5개 정보를 실시간으로 제공한다.
        /// </summary>
        Task SubscribeMbcrAsync(string stockCode, CancellationToken cancellationToken = default);

        /// <summary>실시간회원사 구독 해제</summary>
        Task UnsubscribeMbcrAsync(string stockCode, CancellationToken cancellationToken = default);

        /// <summary>실시간회원사 데이터 수신 이벤트</summary>
        event EventHandler<RealtimeMbcrData> MbcrReceived;

        // ===== 국내주식 장운영정보 [실시간-049] =====

        /// <summary>
        /// 장운영정보 구독 등록 (TR_ID: H0STMKO0)
        ///
        /// ※ 실전 전용 — 모의투자 환경에서 호출하면 예외가 발생한다.
        /// 장 상태 변화(장개시·장마감·VI발동/해제·서킷브레이크 등)를 실시간으로 수신한다.
        /// 종목코드를 지정하면 해당 종목의 VI 발동/해제 시에도 데이터가 수신된다.
        /// </summary>
        Task SubscribeMkopAsync(string stockCode, CancellationToken cancellationToken = default);

        /// <summary>장운영정보 구독 해제</summary>
        Task UnsubscribeMkopAsync(string stockCode, CancellationToken cancellationToken = default);

        /// <summary>장운영정보 데이터 수신 이벤트</summary>
        event EventHandler<RealtimeMkopData> MkopReceived;

        // ===== 국내지수 실시간체결 [실시간-026] =====

        /// <summary>
        /// 국내지수 실시간체결 구독 등록 (TR_ID: H0UPCNT0)
        ///
        /// ※ 실전 전용 — 모의투자 환경에서 호출하면 예외가 발생한다.
        /// KOSPI, KOSDAQ 등 업종지수의 실시간 체결 데이터를 수신한다.
        /// tr_key는 업종구분코드(예: "0001"=코스피, "1001"=코스닥)를 사용한다.
        /// </summary>
        Task SubscribeIndexCcnlAsync(string sectorCode, CancellationToken cancellationToken = default);

        /// <summary>국내지수 실시간체결 구독 해제</summary>
        Task UnsubscribeIndexCcnlAsync(string sectorCode, CancellationToken cancellationToken = default);

        /// <summary>국내지수 실시간체결 데이터 수신 이벤트</summary>
        event EventHandler<RealtimeIndexCcnlData> IndexCcnlReceived;

        // ===== 국내지수 실시간예상체결 [실시간-027] =====

        /// <summary>
        /// 국내지수 실시간예상체결 구독 등록 (TR_ID: H0UPANC0)
        ///
        /// ※ 실전 전용 — 모의투자 환경에서 호출하면 예외가 발생한다.
        /// 동시호가 등 특수 시간대에 KOSPI, KOSDAQ 등 업종지수의 예상 체결 데이터를 수신한다.
        /// tr_key는 업종구분코드(예: "0001"=코스피, "1001"=코스닥)를 사용한다.
        /// </summary>
        Task SubscribeIndexAntcAsync(string sectorCode, CancellationToken cancellationToken = default);

        /// <summary>국내지수 실시간예상체결 구독 해제</summary>
        Task UnsubscribeIndexAntcAsync(string sectorCode, CancellationToken cancellationToken = default);

        /// <summary>국내지수 실시간예상체결 데이터 수신 이벤트</summary>
        event EventHandler<RealtimeIndexAntcData> IndexAntcReceived;

        // ===== 국내지수 실시간프로그램매매 [실시간-028] =====

        /// <summary>
        /// 국내지수 실시간프로그램매매 구독 등록 (TR_ID: H0UPPGM0)
        ///
        /// ※ 실전 전용 — 모의투자 환경에서 호출하면 예외가 발생한다.
        /// 차익/비차익 프로그램매매의 매도·매수 현황을 실시간으로 수신한다.
        /// tr_key는 업종구분코드(예: "0001"=코스피, "1001"=코스닥)를 사용한다.
        /// </summary>
        Task SubscribeIndexPgmAsync(string sectorCode, CancellationToken cancellationToken = default);

        /// <summary>국내지수 실시간프로그램매매 구독 해제</summary>
        Task UnsubscribeIndexPgmAsync(string sectorCode, CancellationToken cancellationToken = default);

        /// <summary>국내지수 실시간프로그램매매 데이터 수신 이벤트</summary>
        event EventHandler<RealtimeIndexPgmData> IndexPgmReceived;

        // ===== 연결 상태 이벤트 =====

        /// <summary>연결 상태가 변경되었을 때 발생 (true=연결, false=끊김)</summary>
        event EventHandler<bool> ConnectionStateChanged;
    }
}
