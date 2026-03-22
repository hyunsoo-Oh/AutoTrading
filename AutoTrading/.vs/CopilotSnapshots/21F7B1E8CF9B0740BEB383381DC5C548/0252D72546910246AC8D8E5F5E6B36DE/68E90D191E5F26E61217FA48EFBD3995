using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoTrading.Features.Models.Api.Auth
{
    /// <summary>
    /// 한국투자증권 API 에러 응답 모델
    /// 
    /// 토큰 발급이든 HashKey 발급이든 실패 시 서버가
    /// error_code / error_description 형태를 줄 수 있으므로 공통으로 사용한다.
    /// </summary>
    public class KiaErrorResponse
    {
        [JsonPropertyName("error_code")]
        public string ErrorCode { get; set; } = string.Empty;

        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; set; } = string.Empty;

        // 일부 API는 msg1, msg_cd 같은 필드를 주기도 하므로 확장 용도로 추가
        [JsonPropertyName("msg1")]
        public string Message1 { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MessageCode { get; set; } = string.Empty;
    }
}
