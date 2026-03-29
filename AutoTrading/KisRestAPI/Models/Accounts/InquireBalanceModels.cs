using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Accounts
{
    // =====================================================================
    // ===== 주식잔고조회 요청 DTO =====
    // =====================================================================

    public class InquireBalanceRequest
    {
        public string CANO { get; set; } = string.Empty;
        public string ACNT_PRDT_CD { get; set; } = string.Empty;
        public string AFHR_FLPR_YN { get; set; } = "N";
        public string OFL_YN { get; set; } = string.Empty;
        public string INQR_DVSN { get; set; } = "01";
        public string UNPR_DVSN { get; set; } = "01";
        public string FUND_STTL_ICLD_YN { get; set; } = "N";
        public string FNCG_AMT_AUTO_RDPT_YN { get; set; } = "N";
        public string PRCS_DVSN { get; set; } = "00";
        public string CTX_AREA_FK100 { get; set; } = string.Empty;
        public string CTX_AREA_NK100 { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 주식잔고조회 응답 DTO =====
    // =====================================================================

    public class InquireBalanceResponse
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
        public List<InquireBalanceItem> Output1 { get; set; } = new();

        [JsonPropertyName("output2")]
        public List<InquireBalanceSummary> Output2 { get; set; } = new();
    }

    // =====================================================================
    // ===== 주식잔고조회 output1 항목 DTO =====
    // =====================================================================

    public class InquireBalanceItem
    {
        [JsonPropertyName("pdno")]
        public string ProductCode { get; set; } = string.Empty;

        [JsonPropertyName("prdt_name")]
        public string ProductName { get; set; } = string.Empty;

        [JsonPropertyName("hldg_qty")]
        public string HoldingQuantity { get; set; } = "0";

        [JsonPropertyName("ord_psbl_qty")]
        public string OrderableQuantity { get; set; } = "0";

        [JsonPropertyName("pchs_avg_pric")]
        public string PurchaseAveragePrice { get; set; } = "0";

        [JsonPropertyName("pchs_amt")]
        public string PurchaseAmount { get; set; } = "0";

        [JsonPropertyName("prpr")]
        public string CurrentPrice { get; set; } = "0";

        [JsonPropertyName("evlu_amt")]
        public string EvaluationAmount { get; set; } = "0";

        [JsonPropertyName("evlu_pfls_amt")]
        public string EvaluationProfitLossAmount { get; set; } = "0";

        [JsonPropertyName("evlu_pfls_rt")]
        public string EvaluationProfitLossRate { get; set; } = "0";
    }

    // =====================================================================
    // ===== 주식잔고조회 output2 요약 DTO =====
    // =====================================================================

    public class InquireBalanceSummary
    {
        [JsonPropertyName("dnca_tot_amt")]
        public string DepositTotalAmount { get; set; } = "0";

        [JsonPropertyName("tot_evlu_amt")]
        public string TotalEvaluationAmount { get; set; } = "0";

        [JsonPropertyName("nass_amt")]
        public string NetAssetAmount { get; set; } = "0";

        [JsonPropertyName("pchs_amt_smtl_amt")]
        public string PurchaseAmountTotal { get; set; } = "0";

        [JsonPropertyName("evlu_amt_smtl_amt")]
        public string EvaluationAmountTotal { get; set; } = "0";

        [JsonPropertyName("evlu_pfls_smtl_amt")]
        public string EvaluationProfitLossTotal { get; set; } = "0";
    }
}
