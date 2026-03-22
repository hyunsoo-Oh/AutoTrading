using AutoTrading.Configuration;
using AutoTrading.Services.KoreaInvest.Auth;
using AutoTrading.Services.KoreaInvest.Common;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace AutoTrading
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // 1) appsettings.json 파일 읽기 준비
            // SetBasePath(AppContext.BaseDirectory):
            // - 실행 파일이 있는 폴더를 기준으로 설정 파일을 찾는다.
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // 2) "ApiSettings" 섹션을 ApiSettings 객체로 변환
            var apiSettings = config.GetSection("ApiSettings").Get<ApiSettings>();

            // 3) 설정 로드 실패 방어
            // 설정이 비어 있거나 파일이 잘못되면 여기서 막는다.
            if (apiSettings == null)
            {
                MessageBox.Show("ApiSettings 설정 로드 실패", "오류");
                return;
            }

            // 4) TradingMode 문자열을 enum으로 변환
            var tradingMode = apiSettings.TradingMode.Equals("Live", StringComparison.OrdinalIgnoreCase)
                ? KiaTradingMode.Live
                : KiaTradingMode.Mock;

            // 5) 거래 환경 서비스 생성
            var kiaTradingService = new KiaTradingService(apiSettings);
            kiaTradingService.SetEnvironment(tradingMode);

            // 6) HttpClient 생성
            // 프로그램 전체에서 재사용하는 방향이 좋다.
            var httpClient = new HttpClient();

            // 7) 인증 서비스 생성
            var authService = new KiaAuthService(httpClient, apiSettings, kiaTradingService);

            // 8) MainForm에 서비스 주입 후 실행
            // MainForm 생성자 안에서 MainPresenter가 생성된다.
            Application.Run(new MainForm(authService, apiSettings.TradingMode, apiSettings, kiaTradingService));
        }
    }
}