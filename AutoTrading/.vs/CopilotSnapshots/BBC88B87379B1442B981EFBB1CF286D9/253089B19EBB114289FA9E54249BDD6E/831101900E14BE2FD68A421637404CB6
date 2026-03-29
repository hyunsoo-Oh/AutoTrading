using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoTrading.Features.Models.Api.Accounts
{
    /// <summary>
    /// output2 배열의 계좌 요약 DTO
    /// </summary>
    public class InquireBalanceSummary
    {
        /// <summary>
        /// 예수금 총금액
        /// </summary>
        [JsonPropertyName("dnca_tot_amt")]
        public string DepositTotalAmount { get; set; } = "0";

        /// <summary>
        /// 총평가금액
        /// </summary>
        [JsonPropertyName("tot_evlu_amt")]
        public string TotalEvaluationAmount { get; set; } = "0";

        /// <summary>
        /// 순자산금액
        /// </summary>
        [JsonPropertyName("nass_amt")]
        public string NetAssetAmount { get; set; } = "0";

        /// <summary>
        /// 매입금액합계
        /// </summary>
        [JsonPropertyName("pchs_amt_smtl_amt")]
        public string PurchaseAmountTotal { get; set; } = "0";

        /// <summary>
        /// 평가금액합계
        /// </summary>
        [JsonPropertyName("evlu_amt_smtl_amt")]
        public string EvaluationAmountTotal { get; set; } = "0";

        /// <summary>
        /// 평가손익합계
        /// </summary>
        [JsonPropertyName("evlu_pfls_smtl_amt")]
        public string EvaluationProfitLossTotal { get; set; } = "0";
    }
}
