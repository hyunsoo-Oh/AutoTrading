using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Accounts
{
    // =====================================================================
    // ===== 투자계좌자산현황조회 요청 DTO =====
    // 실투자 전용 API — 모의투자 미지원
    // =====================================================================

    public sealed class InquireAccountBalanceRequest
    {
        public string CANO { get; set; } = string.Empty;
        public string ACNT_PRDT_CD { get; set; } = "01";
        public string INQR_DVSN_1 { get; set; } = string.Empty;
        public string BSPR_BF_DT_APLY_YN { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 투자계좌자산현황조회 응답 DTO =====
    // =====================================================================

    public sealed class InquireAccountBalanceResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        [JsonPropertyName("output1")]
        public List<InquireAccountBalanceItem> Output1 { get; set; } = new();

        [JsonPropertyName("output2")]
        public InquireAccountBalanceSummary? Output2 { get; set; }
    }

    // =====================================================================
    // ===== 투자계좌자산현황조회 output1 항목 DTO =====
    // =====================================================================

    public sealed class InquireAccountBalanceItem
    {
        [JsonPropertyName("pchs_amt")]
        public string PurchaseAmount { get; set; } = string.Empty;

        [JsonPropertyName("evlu_amt")]
        public string EvaluationAmount { get; set; } = string.Empty;

        [JsonPropertyName("evlu_pfls_amt")]
        public string EvaluationProfitLossAmount { get; set; } = string.Empty;

        [JsonPropertyName("crdt_lnd_amt")]
        public string CreditLoanAmount { get; set; } = string.Empty;

        [JsonPropertyName("real_nass_amt")]
        public string RealNetAssetAmount { get; set; } = string.Empty;

        [JsonPropertyName("whol_weit_rt")]
        public string WholeWeightRate { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 투자계좌자산현황조회 output2 요약 DTO =====
    // =====================================================================

    public sealed class InquireAccountBalanceSummary
    {
        [JsonPropertyName("pchs_amt_smtl")]
        public string PurchaseAmountTotal { get; set; } = string.Empty;

        [JsonPropertyName("nass_tot_amt")]
        public string NassTotAmt { get; set; } = string.Empty;

        [JsonPropertyName("loan_amt_smtl")]
        public string LoanAmountTotal { get; set; } = string.Empty;

        [JsonPropertyName("evlu_pfls_amt_smtl")]
        public string EvaluationProfitLossTotal { get; set; } = string.Empty;

        [JsonPropertyName("evlu_amt_smtl")]
        public string EvaluationAmountTotal { get; set; } = string.Empty;

        [JsonPropertyName("tot_asst_amt")]
        public string TotAsstAmt { get; set; } = string.Empty;

        [JsonPropertyName("cma_auto_loan_amt")]
        public string CmaAutoLoanAmt { get; set; } = string.Empty;

        [JsonPropertyName("tot_mgln_amt")]
        public string TotMglnAmt { get; set; } = string.Empty;

        [JsonPropertyName("crdt_fncg_amt")]
        public string CrdtFncgAmt { get; set; } = string.Empty;

        [JsonPropertyName("frcr_evlu_tota")]
        public string FrcrEvluTota { get; set; } = string.Empty;

        [JsonPropertyName("tot_dncl_amt")]
        public string TotDnclAmt { get; set; } = string.Empty;

        [JsonPropertyName("cma_evlu_amt")]
        public string CmaEvluAmt { get; set; } = string.Empty;

        [JsonPropertyName("dncl_amt")]
        public string DnclAmt { get; set; } = string.Empty;

        [JsonPropertyName("tot_sbst_amt")]
        public string TotSbstAmt { get; set; } = string.Empty;

        [JsonPropertyName("thdt_rcvb_amt")]
        public string ThdtRcvbAmt { get; set; } = string.Empty;

        [JsonPropertyName("ovrs_stck_evlu_amt1")]
        public string OvrsStckEvluAmt1 { get; set; } = string.Empty;

        [JsonPropertyName("ovrs_bond_evlu_amt")]
        public string OvrsBondEvluAmt { get; set; } = string.Empty;

        [JsonPropertyName("sbsc_dncl_amt")]
        public string SbscDnclAmt { get; set; } = string.Empty;
    }
}
