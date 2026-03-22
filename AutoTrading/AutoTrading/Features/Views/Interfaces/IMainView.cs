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
        void UpdateConnectionStatus(bool isConnected);

        // ===== 거래 모드 표시 (모의투자/실전투자) =====
        void UpdateTradingModeDisplay(string modeText);

        // ===== 상태바 메시지 표시 =====
        void UpdateStatusBarMessage(string message);

        // ===== 사용자에게 오류 메시지 표시 =====
        void ShowErrorMessage(string message, string title);

        // ===== 사용자에게 알림 메시지 표시 =====
        void ShowInfoMessage(string message, string title);
    }
}
