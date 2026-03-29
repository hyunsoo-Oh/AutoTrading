using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoTrading.Features.Models.Api.Orders
{
    /// <summary>
    /// 주문 응답 DTO
    /// </summary>
    public sealed class OrderCashResponse
    {
        /// <summary>성공 실패 여부 (0: 성공, 0 이외: 실패)</summary>
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        /// <summary>응답코드</summary>
        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        /// <summary>응답메세지</summary>
        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>응답상세 (주문 결과)</summary>
        [JsonPropertyName("output")]
        public OrderOutput? Output { get; set; }

        public class OrderOutput
        {
            /// <summary>한국거래소 전송 주문 조직번호 (계좌관리점코드)</summary>
            [JsonPropertyName("KRX_FWDG_ORD_ORGNO")]
            public string KrxFwdgOrdOrgno { get; set; } = string.Empty;

            /// <summary>주문번호 (주문취소/정정 시 필요)</summary>
            [JsonPropertyName("ODNO")]
            public string Odno { get; set; } = string.Empty;

            /// <summary>주문시간 (HHMMSS 형식)</summary>
            [JsonPropertyName("ORD_TMD")]
            public string OrdTmd { get; set; } = string.Empty;
        }
    }
}
