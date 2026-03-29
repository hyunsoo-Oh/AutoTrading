using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoTrading.Features.Models.Api.Accounts
{
    /// <summary>
    /// output1 배열의 각 종목 잔고 항목 DTO
    /// </summary>
    public class InquireBalanceItem
    {
        /// <summary>
        /// 종목코드
        /// </summary>
        [JsonPropertyName("pdno")]
        public string ProductCode { get; set; } = string.Empty;

        /// <summary>
        /// 종목명
        /// </summary>
        [JsonPropertyName("prdt_name")]
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 보유수량
        /// </summary>
        [JsonPropertyName("hldg_qty")]
        public string HoldingQuantity { get; set; } = "0";

        /// <summary>
        /// 주문가능수량
        /// </summary>
        [JsonPropertyName("ord_psbl_qty")]
        public string OrderableQuantity { get; set; } = "0";

        /// <summary>
        /// 매입평균가격
        /// </summary>
        [JsonPropertyName("pchs_avg_pric")]
        public string PurchaseAveragePrice { get; set; } = "0";

        /// <summary>
        /// 매입금액
        /// </summary>
        [JsonPropertyName("pchs_amt")]
        public string PurchaseAmount { get; set; } = "0";

        /// <summary>
        /// 현재가
        /// </summary>
        [JsonPropertyName("prpr")]
        public string CurrentPrice { get; set; } = "0";

        /// <summary>
        /// 평가금액
        /// </summary>
        [JsonPropertyName("evlu_amt")]
        public string EvaluationAmount { get; set; } = "0";

        /// <summary>
        /// 평가손익금액
        /// </summary>
        [JsonPropertyName("evlu_pfls_amt")]
        public string EvaluationProfitLossAmount { get; set; } = "0";

        /// <summary>
        /// 평가손익률
        /// </summary>
        [JsonPropertyName("evlu_pfls_rt")]
        public string EvaluationProfitLossRate { get; set; } = "0";
    }
}
