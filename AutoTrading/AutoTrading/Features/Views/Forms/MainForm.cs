using AutoTrading.Controls;
using AutoTrading.Controls.Shell.SideBar;
using AutoTrading.Presentation.Models.Market;
using System.Runtime.InteropServices;

namespace AutoTrading
{
    public partial class MainForm : Form
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        private System.Windows.Forms.Timer _clockTimer;

        private List<StockBaseInfo> _stockInfoList = new List<StockBaseInfo>();

        bool _isLoggedIn = false;

        public MainForm()
        {
            InitializeComponent();

            AllocConsole();

            textBox_Search.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitClockTimer();
            InitNavigationBar();


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


                //if (result == 1)
                //{
                //    toolStripStatusLabel_ConnectState.Text = "● Connected";
                //    _isLoggedIn = true;
                //}
                //else
                //{
                //    toolStripStatusLabel_ConnectState.Text = "○ Disconnected";
                //    _isLoggedIn = false;
                //}
            };
            _clockTimer?.Start();
        }

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

        // 로그인 이벤트 핸들러
        private void AxkhOpenapi_OnEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode == 0)
            {
                _isLoggedIn = true;



                //toolStripStatusLabel_UserName.Text = $"{name} : {accountServer}";
            }
            else if (e.nErrCode == 100)
            {
                MessageBox.Show("사용자 정보교환 실패!");
            }
            else if (e.nErrCode == 101)
            {
                MessageBox.Show("서버 접속 실패!");
            }
            else if (e.nErrCode == 102)
            {
                MessageBox.Show($"로그인 실패! 에러 코드: {e.nErrCode}");
            }
            else
            {
                MessageBox.Show($"알 수 없는 에러 발생! 에러 코드: {e.nErrCode}");
            }
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
                    case "Login":
                        // 로그인 폼 열기

                        break;
                    case "GetStockInfo":

                        toolStripStatusLabel_GetStockInfo.Text = $"종목 정보 요청 완료: {_stockInfoList.Count}개";
                        break;
                    default:

                        break;
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button? button = sender as System.Windows.Forms.Button;

            if (button != null)
            {
                switch (button.Tag)
                {
                    case "Search":
                        string searchItemName = textBox_Search.Text.Trim();

                        if (string.IsNullOrEmpty(searchItemName))
                        {
                            MessageBox.Show("검색어를 입력하세요.");
                            return;
                        }
                        break;
                }
            }
        }
    }
}
