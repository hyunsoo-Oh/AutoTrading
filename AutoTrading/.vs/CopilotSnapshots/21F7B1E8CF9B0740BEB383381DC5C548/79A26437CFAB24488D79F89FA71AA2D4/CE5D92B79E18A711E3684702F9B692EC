using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoTrading.Features.Models.Api.Auth
{
    public class TokenRequest
    {
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; } = "client_credentials";
        [JsonPropertyName("appkey")]
        public string AppKey { get; set; } = string.Empty;
        [JsonPropertyName("appsecret")]
        public string AppSecret { get; set; } = string.Empty;
    }

}
