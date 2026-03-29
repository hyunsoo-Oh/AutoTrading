using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoTrading.Features.Models.Api.Orders
{
    /// <summary>
    /// 주문 요청 DTO
    /// </summary>
    public sealed class OrderCashRequest
    {
        /// <summary>종합계좌번호 (8자리)</summary>
        [JsonPropertyName("CANO")]
        public string Cano { get; set; } = string.Empty;

        /// <summary>계좌상품코드 (보통 01)</summary>
        [JsonPropertyName("ACNT_PRDT_CD")]
        public string AcntPrdtCd { get; set; } = "01";

        /// <summary>상품번호 (종목코드 6자리, ETN 7자리)</summary>
        [JsonPropertyName("PDNO")]
        public string PdNo { get; set; } = string.Empty;

        /// <summary>매도유형 (01: 일반매도, 02: 임의매매, 05: 대차매도)</summary>
        [JsonPropertyName("SLL_TYPE")]
        public string SllType { get; set; } = "01";

        /// <summary>
        /// 주문구분 (00: 지정가, 01: 시장가 등)
        /// </summary>
        [JsonPropertyName("ORD_DVSN")]
        public string OrdDvsn { get; set; } = "00";

        /// <summary>주문수량</summary>
        [JsonPropertyName("ORD_QTY")]
        public string OrdQty { get; set; } = string.Empty;

        /// <summary>주문단가 (시장가 주문 시 "0")</summary>
        [JsonPropertyName("ORD_UNPR")]
        public string OrdUnpr { get; set; } = string.Empty;

        /// <summary>조건가격 (스탑지정가 주문 시에만 사용)</summary>
        [JsonPropertyName("CNDT_PRIC")]
        public string CndtPric { get; set; } = "0";

        /// <summary>거래소ID구분코드 (KRX, NXT, SOR)</summary>
        [JsonPropertyName("EXCG_ID_DVSN_CD")]
        public string ExcgIdDvsnCd { get; set; } = "KRX";
    }
}
