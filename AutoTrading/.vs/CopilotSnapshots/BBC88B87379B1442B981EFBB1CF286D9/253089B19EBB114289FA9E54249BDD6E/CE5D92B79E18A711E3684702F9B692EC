using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoTrading.Features.Models.Api.Auth
{
    /// <summary>
    /// 토큰 발급 요청 DTO
    /// </summary>
    public class TokenRequest
    {
        /// <summary>HTTP 요청 본문의 Content-Type 헤더 값</summary>
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; } = "client_credentials";

        /// <summary>한국투자증권 앱 키</summary>
        [JsonPropertyName("appkey")]
        public string AppKey { get; set; } = string.Empty;

        /// <summary>한국투자증권 앱 시크릿</summary>
        [JsonPropertyName("appsecret")]
        public string AppSecret { get; set; } = string.Empty;
    }

}
