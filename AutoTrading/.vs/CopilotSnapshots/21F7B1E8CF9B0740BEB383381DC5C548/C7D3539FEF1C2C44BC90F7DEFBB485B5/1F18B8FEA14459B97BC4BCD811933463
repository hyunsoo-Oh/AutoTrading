using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoTrading.Features.Models.Api.Auth
{
    /// <summary>
    /// HashKey 발급 성공 응답 모델
    /// 
    /// API 문서상 응답 키 이름이 대문자 HASH 이므로
    /// 반드시 [JsonPropertyName("HASH")]로 명시해야 한다.
    /// </summary>
    public class HashKeyResponse
    {
        [JsonPropertyName("HASH")]
        public string Hash { get; set; } = string.Empty;
    }
}
