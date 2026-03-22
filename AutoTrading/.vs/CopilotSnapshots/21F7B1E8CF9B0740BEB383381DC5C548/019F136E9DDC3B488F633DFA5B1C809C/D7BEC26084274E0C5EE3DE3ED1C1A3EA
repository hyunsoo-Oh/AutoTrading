using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoTrading.Features.Models.Api.Auth
{
    public class TokenResponse
    {
        // ===== API 응답 JSON 필드명과 C# 프로퍼티명을 명시적으로 매핑 =====
        // System.Text.Json은 PropertyNameCaseInsensitive=true 설정만으로는
        // snake_case(access_token) <-> PascalCase(AccessToken) 변환을 하지 않는다.
        // [JsonPropertyName]으로 실제 JSON 키를 직접 지정해야 올바르게 역직렬화된다.
        // ===== =====

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = string.Empty;

        [JsonPropertyName("expires_in")]
        public double ExpiresIn { get; set; }

        // ===== API 문서 기준 실제 필드명은 "access_token_token_expired" =====
        // "AccessTokenExpired"로는 절대 매핑되지 않으므로 반드시 명시 필요
        // ===== =====
        [JsonPropertyName("access_token_token_expired")]
        public string AccessTokenExpired { get; set; } = string.Empty;
    }
}
