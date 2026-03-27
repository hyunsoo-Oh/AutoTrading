using System.Text.Json.Serialization;

namespace AutoTrading.Features.Models.Api.Accounts
{
    /// <summary>
    /// 주식잔고조회_실현손익 output2 배열의 계좌 요약 DTO
    ///
    /// 이 API의 핵심 필드:
    /// - RlztPfls(실현손익): 매도를 통해 실제로 확정된 손익금액
    /// - RlztErngRt(실현수익율): 실현손익의 수익률
    /// - RealEvluPfls(실평가손익): 실평가손익 (실현+미실현 통합)
    /// </summary>
    public sealed class InquireBalanceRlzPlSummary
    {
        /// <summary>예수금 총금액</summary>
        [JsonPropertyName("dnca_tot_amt")]
        public string DepositTotalAmount { get; set; } = string.Empty;

        /// <summary>익일 정산금액</summary>
        [JsonPropertyName("nxdy_excc_amt")]
        public string NxdyExccAmt { get; set; } = string.Empty;

        /// <summary>CMA 평가금액</summary>
        [JsonPropertyName("cma_evlu_amt")]
        public string CmaEvluAmt { get; set; } = string.Empty;

        /// <summary>전일 매수금액</summary>
        [JsonPropertyName("bfdy_buy_amt")]
        public string BfdyBuyAmt { get; set; } = string.Empty;

        /// <summary>금일 매수금액</summary>
        [JsonPropertyName("thdt_buy_amt")]
        public string ThdtBuyAmt { get; set; } = string.Empty;

        /// <summary>전일 매도금액</summary>
        [JsonPropertyName("bfdy_sll_amt")]
        public string BfdySllAmt { get; set; } = string.Empty;

        /// <summary>금일 매도금액</summary>
        [JsonPropertyName("thdt_sll_amt")]
        public string ThdtSllAmt { get; set; } = string.Empty;

        /// <summary>총 대출금액</summary>
        [JsonPropertyName("tot_loan_amt")]
        public string TotLoanAmt { get; set; } = string.Empty;

        /// <summary>유가 평가금액</summary>
        [JsonPropertyName("scts_evlu_amt")]
        public string SctsEvluAmt { get; set; } = string.Empty;

        /// <summary>총 평가금액</summary>
        [JsonPropertyName("tot_evlu_amt")]
        public string TotalEvaluationAmount { get; set; } = string.Empty;

        /// <summary>순자산금액</summary>
        [JsonPropertyName("nass_amt")]
        public string NetAssetAmount { get; set; } = string.Empty;

        /// <summary>매입금액 합계</summary>
        [JsonPropertyName("pchs_amt_smtl_amt")]
        public string PurchaseAmountTotal { get; set; } = string.Empty;

        /// <summary>평가금액 합계</summary>
        [JsonPropertyName("evlu_amt_smtl_amt")]
        public string EvaluationAmountTotal { get; set; } = string.Empty;

        /// <summary>평가손익 합계</summary>
        [JsonPropertyName("evlu_pfls_smtl_amt")]
        public string EvaluationProfitLossTotal { get; set; } = string.Empty;

        /// <summary>전일 총자산 평가금액</summary>
        [JsonPropertyName("bfdy_tot_asst_evlu_amt")]
        public string BfdyTotAsstEvluAmt { get; set; } = string.Empty;

        /// <summary>자산증감액</summary>
        [JsonPropertyName("asst_icdc_amt")]
        public string AsstIcdcAmt { get; set; } = string.Empty;

        /// <summary>자산증감수익율</summary>
        [JsonPropertyName("asst_icdc_erng_rt")]
        public string AsstIcdcErngRt { get; set; } = string.Empty;

        // ===== 이 API의 핵심 필드 =====
        // 일반 잔고조회(InquireBalanceSummary)에는 없는 실현손익 전용 필드들이다.

        /// <summary>
        /// 실현손익 — 매도를 통해 실제로 확정된 손익 금액
        /// 양수 = 이익, 음수 = 손실
        /// </summary>
        [JsonPropertyName("rlzt_pfls")]
        public string RlztPfls { get; set; } = string.Empty;

        /// <summary>실현수익율</summary>
        [JsonPropertyName("rlzt_erng_rt")]
        public string RlztErngRt { get; set; } = string.Empty;

        /// <summary>
        /// 실평가손익 — 실현손익 + 미실현 평가손익의 합산
        /// </summary>
        [JsonPropertyName("real_evlu_pfls")]
        public string RealEvluPfls { get; set; } = string.Empty;

        /// <summary>실평가손익수익율</summary>
        [JsonPropertyName("real_evlu_pfls_erng_rt")]
        public string RealEvluPflsErngRt { get; set; } = string.Empty;
    }
}
