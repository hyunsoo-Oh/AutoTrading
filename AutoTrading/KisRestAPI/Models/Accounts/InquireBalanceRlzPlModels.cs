using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Accounts
{
    // =====================================================================
    // ===== 주식잔고조회_실현손익 요청 DTO =====
    // 실투자 전용 API — 모의투자 미지원
    // =====================================================================

    public sealed class InquireBalanceRlzPlRequest
    {
        public string CANO { get; set; } = string.Empty;
        public string ACNT_PRDT_CD { get; set; } = "01";
        public string AFHR_FLPR_YN { get; set; } = "N";
        public string OFL_YN { get; set; } = string.Empty;
        public string INQR_DVSN { get; set; } = "00";
        public string UNPR_DVSN { get; set; } = "01";
        public string FUND_STTL_ICLD_YN { get; set; } = "N";
        public string FNCG_AMT_AUTO_RDPT_YN { get; set; } = "N";
        public string PRCS_DVSN { get; set; } = "00";
        public string COST_ICLD_YN { get; set; } = "N";
        public string CTX_AREA_FK100 { get; set; } = string.Empty;
        public string CTX_AREA_NK100 { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 주식잔고조회_실현손익 응답 DTO =====
    // =====================================================================

    public sealed class InquireBalanceRlzPlResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        [JsonPropertyName("ctx_area_fk100")]
        public string CtxAreaFk100 { get; set; } = string.Empty;

        [JsonPropertyName("ctx_area_nk100")]
        public string CtxAreaNk100 { get; set; } = string.Empty;

        [JsonPropertyName("output1")]
        public List<InquireBalanceRlzPlItem> Output1 { get; set; } = new();

        [JsonPropertyName("output2")]
        public List<InquireBalanceRlzPlSummary> Output2 { get; set; } = new();
    }

    // =====================================================================
    // ===== 주식잔고조회_실현손익 output1 항목 DTO =====
    // =====================================================================

    public sealed class InquireBalanceRlzPlItem
    {
        [JsonPropertyName("pdno")]
        public string ProductCode { get; set; } = string.Empty;

        [JsonPropertyName("prdt_name")]
        public string ProductName { get; set; } = string.Empty;

        [JsonPropertyName("trad_dvsn_name")]
        public string TradDvsnName { get; set; } = string.Empty;

        [JsonPropertyName("bfdy_buy_qty")]
        public string BfdyBuyQty { get; set; } = string.Empty;

        [JsonPropertyName("bfdy_sll_qty")]
        public string BfdySllQty { get; set; } = string.Empty;

        [JsonPropertyName("thdt_buyqty")]
        public string ThdtBuyQty { get; set; } = string.Empty;

        [JsonPropertyName("thdt_sll_qty")]
        public string ThdtSllQty { get; set; } = string.Empty;

        [JsonPropertyName("hldg_qty")]
        public string HoldingQuantity { get; set; } = string.Empty;

        [JsonPropertyName("ord_psbl_qty")]
        public string OrderableQuantity { get; set; } = string.Empty;

        [JsonPropertyName("pchs_avg_pric")]
        public string PurchaseAveragePrice { get; set; } = string.Empty;

        [JsonPropertyName("pchs_amt")]
        public string PurchaseAmount { get; set; } = string.Empty;

        [JsonPropertyName("prpr")]
        public string CurrentPrice { get; set; } = string.Empty;

        [JsonPropertyName("evlu_amt")]
        public string EvaluationAmount { get; set; } = string.Empty;

        [JsonPropertyName("evlu_pfls_amt")]
        public string EvaluationProfitLossAmount { get; set; } = string.Empty;

        [JsonPropertyName("evlu_pfls_rt")]
        public string EvaluationProfitLossRate { get; set; } = string.Empty;

        [JsonPropertyName("evlu_erng_rt")]
        public string EvaluationEarningRate { get; set; } = string.Empty;

        [JsonPropertyName("bfdy_cprs_icdc")]
        public string BfdyCprsIcdc { get; set; } = string.Empty;

        [JsonPropertyName("fltt_rt")]
        public string FlttRt { get; set; } = string.Empty;

        [JsonPropertyName("loan_dt")]
        public string LoanDt { get; set; } = string.Empty;

        [JsonPropertyName("loan_amt")]
        public string LoanAmt { get; set; } = string.Empty;

        [JsonPropertyName("expd_dt")]
        public string ExpdDt { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 주식잔고조회_실현손익 output2 요약 DTO =====
    // =====================================================================

    public sealed class InquireBalanceRlzPlSummary
    {
        [JsonPropertyName("dnca_tot_amt")]
        public string DepositTotalAmount { get; set; } = string.Empty;

        [JsonPropertyName("nxdy_excc_amt")]
        public string NxdyExccAmt { get; set; } = string.Empty;

        [JsonPropertyName("cma_evlu_amt")]
        public string CmaEvluAmt { get; set; } = string.Empty;

        [JsonPropertyName("bfdy_buy_amt")]
        public string BfdyBuyAmt { get; set; } = string.Empty;

        [JsonPropertyName("thdt_buy_amt")]
        public string ThdtBuyAmt { get; set; } = string.Empty;

        [JsonPropertyName("bfdy_sll_amt")]
        public string BfdySllAmt { get; set; } = string.Empty;

        [JsonPropertyName("thdt_sll_amt")]
        public string ThdtSllAmt { get; set; } = string.Empty;

        [JsonPropertyName("tot_loan_amt")]
        public string TotLoanAmt { get; set; } = string.Empty;

        [JsonPropertyName("scts_evlu_amt")]
        public string SctsEvluAmt { get; set; } = string.Empty;

        [JsonPropertyName("tot_evlu_amt")]
        public string TotalEvaluationAmount { get; set; } = string.Empty;

        [JsonPropertyName("nass_amt")]
        public string NetAssetAmount { get; set; } = string.Empty;

        [JsonPropertyName("pchs_amt_smtl_amt")]
        public string PurchaseAmountTotal { get; set; } = string.Empty;

        [JsonPropertyName("evlu_amt_smtl_amt")]
        public string EvaluationAmountTotal { get; set; } = string.Empty;

        [JsonPropertyName("evlu_pfls_smtl_amt")]
        public string EvaluationProfitLossTotal { get; set; } = string.Empty;

        [JsonPropertyName("bfdy_tot_asst_evlu_amt")]
        public string BfdyTotAsstEvluAmt { get; set; } = string.Empty;

        [JsonPropertyName("asst_icdc_amt")]
        public string AsstIcdcAmt { get; set; } = string.Empty;

        [JsonPropertyName("asst_icdc_erng_rt")]
        public string AsstIcdcErngRt { get; set; } = string.Empty;

        [JsonPropertyName("rlzt_pfls")]
        public string RlztPfls { get; set; } = string.Empty;

        [JsonPropertyName("rlzt_erng_rt")]
        public string RlztErngRt { get; set; } = string.Empty;

        [JsonPropertyName("real_evlu_pfls")]
        public string RealEvluPfls { get; set; } = string.Empty;

        [JsonPropertyName("real_evlu_pfls_erng_rt")]
        public string RealEvluPflsErngRt { get; set; } = string.Empty;
    }
}
