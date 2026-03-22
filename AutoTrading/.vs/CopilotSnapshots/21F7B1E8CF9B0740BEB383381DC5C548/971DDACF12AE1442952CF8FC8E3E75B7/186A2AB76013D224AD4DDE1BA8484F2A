using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Configuration
{
    /// <summary>
    /// API 접속정보를 담는 클래스 (모의투자/실전투자)
    /// </summary>
    public class ApiEndpointSettings
    {
        public string BaseUrl { get; set; } = string.Empty; // API 서버 주소
        public string AppKey { get; set; } = string.Empty;  // 한국투자증권에섭 API 앱 키
        public string AppSecret { get; set; } = string.Empty; // 한국투자증권에섭 API 앱 시크릿

        // ===== 계좌번호: 앞 8자리 =====
        // Mock/Live 환경마다 계좌번호가 다르므로 각 환경 설정에 포함한다.
        // 하드코딩 대신 여기서 읽으면 환경 전환만으로 계좌번호도 자동으로 바뀐다.
        // ===== =====
        public string AccountNumber { get; set; } = string.Empty;
    }
}
