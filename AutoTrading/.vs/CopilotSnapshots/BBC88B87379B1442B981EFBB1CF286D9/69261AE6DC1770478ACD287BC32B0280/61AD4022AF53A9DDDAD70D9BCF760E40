using AutoTrading.Configuration;
using AutoTrading.Features.Presenters.Dashboard;
using AutoTrading.Features.Views.Interfaces;
using AutoTrading.Services.KoreaInvest.Auth;
using AutoTrading.Services.KoreaInvest.Common.Http;

namespace AutoTrading.Features.Views.Contents
{
    /// <summary>
    /// 대시보드 페이지 — 전체 현황 요약 화면
    /// </summary>
    public partial class Dashboard : UserControl, IDashboardView
    {
        private DashboardPresenter? _presenter;
        private System.Windows.Forms.Timer? _refreshTimer;

        /// <summary>카드 갱신 주기 (1분)</summary>
        private const int RefreshIntervalMs = 60 * 1000;

        public Dashboard()
        {
            InitializeComponent();
        }

        public Dashboard(
            IAuthService authService,
            ApiSettings apiSettings,
            IKisTradingService kisTradingService) : this()
        {
            _presenter = new DashboardPresenter(this, authService, apiSettings, kisTradingService);
            Load += Dashboard_Load;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // 최초 1회 즉시 조회
            _ = RefreshAsync();

            // 1분 주기 타이머 — components에 등록해 Dispose 시 자동 정리
            components ??= new System.ComponentModel.Container();
            _refreshTimer = new System.Windows.Forms.Timer(components)
            {
                Interval = RefreshIntervalMs
            };
            _refreshTimer.Tick += async (s, ev) => await RefreshAsync();
            _refreshTimer.Start();
        }

        private async Task RefreshAsync()
        {
            if (_presenter == null) return;
            await _presenter.RefreshBalanceAsync();
        }

        // ========================================================
        // ===== IDashboardView 구현 =====
        // Presenter가 호출하는 UI 갱신 메서드들이다.
        // ========================================================

        public void UpdateBalanceSummary(
            decimal totalEvaluation,
            decimal deposits,
            decimal profitLoss,
            decimal purchaseAmount)
        {
            if (InvokeRequired)
            {
                Invoke(() => UpdateBalanceSummary(totalEvaluation, deposits, profitLoss, purchaseAmount));
                return;
            }

            // 총 자산 가치: 총평가금액 표시, 등락은 평가손익으로 자동 계산
            valueTrackerCard_TotalAsset.TotalValue  = totalEvaluation;
            valueTrackerCard_TotalAsset.ChangeAmount = profitLoss;

            // 예수금: 금액만 표시
            valueTrackerCard_Deposits.TotalValue = deposits;

            // 수익률: 평가손익 금액 표시 + 수익률(%) 직접 계산
            valueTrackerCard_Rate.TotalValue  = profitLoss;
            valueTrackerCard_Rate.ChangeAmount = profitLoss;
            if (purchaseAmount != 0m)
                valueTrackerCard_Rate.ChangeRate =
                    (float)(Math.Abs(profitLoss) / Math.Abs(purchaseAmount) * 100m);
        }
    }
}
