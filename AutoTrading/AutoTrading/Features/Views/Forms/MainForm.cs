using AutoTrading.Configuration;
using AutoTrading.Controls;
using AutoTrading.Controls.Shell.SideBar;
using AutoTrading.Features.Models.Api.Accounts;
using AutoTrading.Features.Models.Api.Auth;
using AutoTrading.Presentation.Models.Market;
using AutoTrading.Services.KoreaInvest.Accounts;
using AutoTrading.Services.KoreaInvest.Auth;
using AutoTrading.Services.KoreaInvest.Common;
using System.Runtime.InteropServices;

namespace AutoTrading
{
    public partial class MainForm : Form
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        private System.Windows.Forms.Timer _clockTimer;

        // ===== 토큰 자동 갱신 타이머 =====
        // 일정 주기마다 토큰 유효성을 확인하고 만료 임박 시 재발급한다.
        // KiaAuthService.IsTokenValid() 내부에서 만료 1분 전을 기준으로 판단하므로
        // 여기서는 단순히 주기적으로 GetAccessTokenAsync를 호출하기만 하면 된다.
        // ===== =====
        private System.Windows.Forms.Timer _tokenRefreshTimer;

        /// <summary>
        /// 현재 실행 모드 표시용 값
        /// "Mock" 또는 "Live"
        /// </summary>
        private readonly string _tradingMode;

        private readonly ApiSettings _apiSettings;
        private readonly IKiaTradingService _kiaTradingService;
        private InquireBalanceHeaderBuilder? _headerBuilder;
        private IAccountService? _accountService;

        private HttpClient? _httpClient;

        // ===== 토큰 갱신 주기(ms) =====
        // 한국투자증권 API 토큰 유효기간은 24시간, 6시간 이내 재발급 시 기존 토큰 반환
        // 1분(100,000ms)마다 확인하면 만료 1분 전 조건과 맞물려 적시에 재발급된다.
        // ===== =====
        private const int TokenRefreshIntervalMs = 1 * 60 * 1000;

        /// <summary>
        /// 인증 서비스
        /// Form이 직접 REST 통신 세부 구현을 알 필요 없도록 서비스로 분리했다.
        /// </summary>
        private readonly IAuthService _authService;

        private List<StockBaseInfo> _stockInfoList = new List<StockBaseInfo>();

        bool _isLoggedIn = false;

        public MainForm()
        {
            InitializeComponent();

            // 콘솔창 열기 (디버깅용)
            AllocConsole();
        }

        public MainForm(IAuthService authService, string tradingMode, ApiSettings apiSettings, IKiaTradingService kiaTradingService) : this()
        {
            _authService = authService;
            _tradingMode = tradingMode == "Mock" ? "모의투자" : "실전투자";
            _apiSettings = apiSettings;
            _kiaTradingService = kiaTradingService;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 현재 실행 환경을 화면에 표시
            twoLineTopBar.InvestmentMode = $"{_tradingMode}";

            InitClockTimer();
            InitNavigationBar();
            RebuildKiaServices();

            // ===== 앱 시작 시 즉시 토큰 발급 시도 =====
            // Load 이벤트에서 직접 await 할 수 없으므로 별도 async 메서드로 위임한다.
            // ===== =====
            _ = InitTokenAsync();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

        }

        private void InitClockTimer()
        {
            _clockTimer = new System.Windows.Forms.Timer();
            _clockTimer.Interval = 1000;
            _clockTimer.Tick += (s, e) =>
            {
                toolStripStatusLabel_Clock.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  ");

            };
            _clockTimer?.Start();
        }

        // 앱 시작 시 최초 토큰 발급 및 자동 갱신 타이머 초기화
        private async Task InitTokenAsync()
        {
            // ===== 앱 시작 시 Mock/Live 토큰을 모두 미리 발급 =====
            // 환경 전환 시 즉시 재사용할 수 있도록 두 환경 토큰을 모두 준비한다.
            // 각 토큰은 KiaAuthService 내부에서 환경별로 독립 캐싱된다.
            // ===== =====
            KiaTradingMode original = _kiaTradingService.CurrentEnvironment;

            // Mock 토큰 발급
            _kiaTradingService.SetEnvironment(KiaTradingMode.Mock);
            await GetTokenAsync();

            // Live 토큰 발급
            _kiaTradingService.SetEnvironment(KiaTradingMode.Live);
            await GetTokenAsync();

            // 시작 환경으로 복원
            _kiaTradingService.SetEnvironment(original);
            await GetTokenAsync();

            // 이후 주기적으로 갱신 확인 (만료 1분 전에만 재발급)
            _tokenRefreshTimer = new System.Windows.Forms.Timer();
            _tokenRefreshTimer.Interval = TokenRefreshIntervalMs;
            _tokenRefreshTimer.Tick += async (s, e) =>
            {
                // ===== 타이머마다 두 환경 모두 갱신 여부를 확인한다 =====
                // IsTokenValid()가 만료 1분 전일 때만 서버 호출하므로
                // 유효한 토큰은 그냥 통과되어 불필요한 발급 요청이 없다.
                // ===== =====
                KiaTradingMode current = _kiaTradingService.CurrentEnvironment;

                _kiaTradingService.SetEnvironment(KiaTradingMode.Mock);
                await GetTokenAsync();

                _kiaTradingService.SetEnvironment(KiaTradingMode.Live);
                await GetTokenAsync();

                _kiaTradingService.SetEnvironment(current);
                await GetTokenAsync();
            };
            _tokenRefreshTimer.Start();
        }

        // SideNavigationBar에 메뉴 아이템을 설정하고 이벤트 핸들러를 연결
        private void InitNavigationBar()
        {
            sideNavigationBar.SetItems(
            [
                new NavigationItemDefinition("Portfolio", "Portfolio"),
                new NavigationItemDefinition("Orders", "Orders"),
                new NavigationItemDefinition("Dashboard", "Dashboard"),
            ]);

            Console.WriteLine("[NAV] items initialized: Dashboard, Orders, Portfolio (+ fixed bottom Settings)");

            sideNavigationBar.SelectionChanged -= SideNavigationBar_SelectionChanged;
            sideNavigationBar.SelectionChanged += SideNavigationBar_SelectionChanged;

            sideNavigationBar.SettingsInvoked += (_, _) =>
            {
                //new SettingsForm().Show();
            };
        }

        private void SideNavigationBar_SelectionChanged(object? sender, NavigationSelectionChangedEventArgs e)
        {
            Console.WriteLine($"[NAV] {e.PreviousKey} -> {e.CurrentKey}");
        }

        private void Request_ItemCodeList(string index)
        {

        }

        /// MenuStrip의 메뉴 클릭 이벤트 핸들러
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem? item = sender as ToolStripMenuItem;

            if (item != null)
            {
                switch (item.Tag)
                {
                    case "Mock":
                        SwitchTradingEnvironment(KiaTradingMode.Mock);
                        break;

                    case "Live":
                        SwitchTradingEnvironment(KiaTradingMode.Live);
                        break;
                    case "GetStockInfo":
                        toolStripStatusLabel_GetStockInfo.Text = $"종목 정보 요청 완료: {_stockInfoList.Count}개";
                        break;
                    default:
                        break;
                }
            }
        }

        // ===== 토큰 발급/갱신 공통 메서드 =====
        // - 앱 시작 자동 발급, 주기 타이머, 수동 Login 메뉴 모두 이 메서드를 사용
        // - 성공 시 TopBar 연결 상태를 갱신하고, 실패 시 연결 해제 상태로 표시
        // ===== =====
        private async Task GetTokenAsync()
        {
            try
            {
                TokenResponse? token = await _authService.GetAccessTokenAsync();

                bool connected = token != null && !string.IsNullOrWhiteSpace(token.AccessToken);
                twoLineTopBar.IsServerConnected = connected;

                Console.WriteLine(connected
                    ? $"[TOKEN] 발급/갱신 성공 | 만료: {token!.AccessTokenExpired}"
                    : "[TOKEN] 발급 실패: 토큰이 비어 있음");
            }
            catch (Exception ex)
            {
                twoLineTopBar.IsServerConnected = false;
                Console.WriteLine($"[TOKEN] 발급 실패: {ex.Message}");
                MessageBox.Show($"토큰 요청 실패: {ex.Message}", "인증 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // ===== =====

        private void button_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button? button = sender as System.Windows.Forms.Button;

            if (button != null)
            {
                switch (button.Tag)
                {
                    case "Search":

                        break;
                }
            }
        }

        private async void button_Test_Click(object sender, EventArgs e)
        {
            //// ===== 테스트용: 토큰 강제 무효화 후 재발급 시뮬레이션 =====
            //// InvalidateToken()으로 캐시를 비우면 GetTokenAsync()가
            //// 서버에서 새 토큰을 발급받는 경로를 탄다.
            //// ===== =====
            //Console.WriteLine("[TEST] 토큰 강제 무효화 → 재발급 시도");
            //twoLineTopBar.IsServerConnected = false;

            //_authService.InvalidateToken();

            try
            {
                if (_accountService == null)
                {
                    MessageBox.Show("계좌 서비스가 초기화되지 않았습니다.");
                    return;
                }

                KiaTradingMode mode = _kiaTradingService.CurrentEnvironment;
                ApiEndpointSettings currentSettings = _kiaTradingService.GetCurrentSettings();

                var request = new InquireBalanceRequest
                {
                    // ===== 계좌번호를 appsettings.json에서 읽는다 =====
                    // 환경 전환(Mock/Live) 시 자동으로 해당 환경의 계좌번호가 사용된다.
                    // ===== =====
                    CANO = currentSettings.AccountNumber,
                    ACNT_PRDT_CD = InquireBalanceAccountProductCodeProvider.Get(mode),
                    AFHR_FLPR_YN = "N",
                    OFL_YN = "",
                    INQR_DVSN = "01",
                    UNPR_DVSN = "01",
                    FUND_STTL_ICLD_YN = "N",
                    FNCG_AMT_AUTO_RDPT_YN = "N",
                    PRCS_DVSN = "00",
                    CTX_AREA_FK100 = "",
                    CTX_AREA_NK100 = ""
                };

                InquireBalanceResponse? response =
                    await _accountService.InquireBalanceAsync(request);

                if (response == null)
                {
                    MessageBox.Show("응답이 null 입니다.");
                    return;
                }

                Console.WriteLine($"RtCd: {response.RtCd}");
                Console.WriteLine($"MsgCd: {response.MsgCd}");
                Console.WriteLine($"Msg1: {response.Msg1}");
                Console.WriteLine($"보유 종목 수: {response.Output1?.Count ?? 0}");

                // output2는 보통 1건처럼 쓰는 경우가 많습니다.
                if (response.Output2 != null && response.Output2.Count > 0)
                {
                    var summary = response.Output2[0];

                    Console.WriteLine($"예수금: {summary.DepositTotalAmount}");
                    Console.WriteLine($"총평가금액: {summary.TotalEvaluationAmount}");
                    Console.WriteLine($"순자산금액: {summary.NetAssetAmount}");

                    int totalValue;

                    if (int.TryParse(summary.TotalEvaluationAmount, out totalValue))
                    {
                        //valueTrackerCard1.TotalValue = totalValue;
                    }
                }

                // 간단히 DataGridView 바인딩 테스트
                if (response.Output1 != null)
                {

                }

                MessageBox.Show("주식잔고조회 테스트 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류: {ex.Message}");
                MessageBox.Show(ex.ToString(), "주식잔고조회 테스트 실패");
            }
        }

        private void RebuildKiaServices()
        {
            _httpClient?.Dispose();
            _httpClient = new HttpClient();

            // _headerBuilder를 먼저 생성한 뒤 _accountService에 주입해야 한다.
            // _headerBuilder가 null인 상태로 KiaAccountService에 넘기면 런타임 오류가 발생한다.
            _headerBuilder = new InquireBalanceHeaderBuilder(_authService, _apiSettings, _kiaTradingService);
            _accountService = new KiaAccountService(_httpClient, _kiaTradingService, _headerBuilder);
        }

        // ===== 거래 환경(Mock/Live) 전환 =====
        // 환경이 바뀌면 이전 환경의 토큰은 무효이므로 반드시 캐시를 비운 뒤 재발급해야 한다.
        // ===== =====
        private void SwitchTradingEnvironment(KiaTradingMode mode)
        {
            if (_kiaTradingService.CurrentEnvironment == mode)
            {
                Console.WriteLine($"[ENV] 이미 {mode} 환경입니다.");
                return;
            }

            // 1) 환경 전환
            _kiaTradingService.SetEnvironment(mode);

            // 2) TopBar 표시 갱신
            twoLineTopBar.InvestmentMode = mode == KiaTradingMode.Live ? "실전투자" : "모의투자";

            // ===== 환경 전환 시 InvalidateToken()을 호출하지 않는다 =====
            // 토큰은 환경별로 독립 캐싱되므로 이미 발급된 토큰은 그대로 재사용한다.
            // GetTokenAsync()가 해당 환경의 유효 토큰을 찾으면 서버 호출 없이 반환된다.
            // ===== =====
            _ = GetTokenAsync();

            Console.WriteLine($"[ENV] 환경 전환: {mode}");
        }
        // ===== =====
    }
}
