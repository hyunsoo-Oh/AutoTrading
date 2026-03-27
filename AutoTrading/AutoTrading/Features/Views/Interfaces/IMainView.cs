namespace AutoTrading.Features.Views.Interfaces
{
    /// <summary>
    /// MainForm이 구현하는 View 인터페이스
    ///
    /// Presenter는 이 인터페이스를 통해서만 UI를 조작한다.
    /// MainForm 내부 컨트롤(TopBar, StatusBar 등)에 직접 접근하지 않는다.
    ///
    /// 수정 포인트:
    /// - UI에 새로운 상태 표시가 필요하면 여기에 메서드/프로퍼티를 추가한다.
    /// - Presenter가 호출할 수 있는 UI 동작만 노출한다.
    /// </summary>
    public interface IMainView
    {
        // ===== 서버 연결 상태 표시 =====
        void UpdateConnectionStatus(bool isConnected, string accountInfo);

        // ===== 거래 모드 표시 (모의투자/실전투자) =====
        void UpdateTradingModeDisplay(string modeText);

        // ===== 상태바 메시지 표시 =====
        void UpdateStatusBarMessage(string message);

        // ===== 사용자에게 오류 메시지 표시 =====
        void ShowErrorMessage(string message, string title);

        // ===== 사용자에게 알림 메시지 표시 =====
        void ShowInfoMessage(string message, string title);

        // ===== 주문 목록에서 항목 선택 다이얼로그 =====
        // Presenter가 조회한 주문 목록을 사용자에게 보여주고 선택받는다.
        // 반환값: 선택한 항목의 index (0-based), 취소 시 null
        int? ShowOrderSelectionDialog(IReadOnlyList<string> items, string title);

        // ===== 사용자에게 확인/취소 다이얼로그 표시 =====
        // Presenter가 흐름 분기 전 사용자 동의를 구할 때 사용한다.
        // 반환값: true(확인), false(취소)
        bool ShowConfirmMessage(string message, string title);
    }
}
