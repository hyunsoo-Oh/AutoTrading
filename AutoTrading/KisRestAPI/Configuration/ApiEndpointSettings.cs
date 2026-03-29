namespace KisRestAPI.Configuration
{
    /// <summary>
    /// API 접속정보를 담는 클래스 (모의투자/실전투자)
    /// </summary>
    public class ApiEndpointSettings
    {
        /// <summary>API 서버 주소</summary>
        public string BaseUrl { get; set; } = string.Empty;

        /// <summary>한국투자증권 API 앱 키</summary>
        public string AppKey { get; set; } = string.Empty;

        /// <summary>한국투자증권 API 앱 시크릿</summary>
        public string AppSecret { get; set; } = string.Empty;

        /// <summary>계좌번호 앞 8자리 (환경별로 다름)</summary>
        public string AccountNumber { get; set; } = string.Empty;

        /// <summary>WebSocket 실시간 서버 URL (환경별로 다름)</summary>
        public string WebSocketUrl { get; set; } = string.Empty;
    }
}
