using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Accounts
{
    // =====================================================================
    // ===== 기간별손익일별합산조회 요청 DTO =====
    // 실투자 전용 API — 모의투자 미지원
    // =====================================================================

    public sealed class InquirePeriodProfitLossRequest
    {
        /// <summary>종합계좌번호 — 8자리</summary>
        public string CANO { get; set; } = string.Empty;

        /// <summary>계좌상품코드 — 2자리 (기본 "01")</summary>
        public string ACNT_PRDT_CD { get; set; } = "01";

        /// <summary>조회시작일자 (YYYYMMDD)</summary>
        public string INQR_STRT_DT { get; set; } = string.Empty;

        /// <summary>조회종료일자 (YYYYMMDD)</summary>
        public string INQR_END_DT { get; set; } = string.Empty;

        /// <summary>상품번호 (종목코드) — 공란 입력 시 전체</summary>
        public string PDNO { get; set; } = string.Empty;

        /// <summary>조회구분 — "00" 입력</summary>
        public string INQR_DVSN { get; set; } = "00";

        /// <summary>정렬구분 — "00": 최근 순</summary>
        public string SORT_DVSN { get; set; } = "00";

        /// <summary>잔고구분 — "00": 전체</summary>
        public string CBLC_DVSN { get; set; } = "00";

        /// <summary>연속조회검색조건100</summary>
        public string CTX_AREA_FK100 { get; set; } = string.Empty;

        /// <summary>연속조회키100</summary>
        public string CTX_AREA_NK100 { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 기간별손익일별합산조회 응답 DTO =====
    // =====================================================================

    public sealed class InquirePeriodProfitLossResponse
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
        public List<InquirePeriodProfitLossItem> Output1 { get; set; } = new();

        [JsonPropertyName("output2")]
        public InquirePeriodProfitLossSummary? Output2 { get; set; }
    }

    // =====================================================================
    // ===== 기간별손익일별합산조회 output1 항목 DTO =====
    // =====================================================================

    public sealed class InquirePeriodProfitLossItem
    {
        [JsonPropertyName("trad_dt")]
        public string TradDt { get; set; } = string.Empty;

        [JsonPropertyName("buy_amt")]
        public string BuyAmt { get; set; } = string.Empty;

        [JsonPropertyName("sll_amt")]
        public string SllAmt { get; set; } = string.Empty;

        [JsonPropertyName("rlzt_pfls")]
        public string RlztPfls { get; set; } = string.Empty;

        [JsonPropertyName("fee")]
        public string Fee { get; set; } = string.Empty;

        [JsonPropertyName("loan_int")]
        public string LoanInt { get; set; } = string.Empty;

        [JsonPropertyName("tl_tax")]
        public string TlTax { get; set; } = string.Empty;

        [JsonPropertyName("pfls_rt")]
        public string PflsRt { get; set; } = string.Empty;

        [JsonPropertyName("sll_qty1")]
        public string SllQty1 { get; set; } = string.Empty;

        [JsonPropertyName("buy_qty1")]
        public string BuyQty1 { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 기간별손익일별합산조회 output2 요약 DTO =====
    // =====================================================================

    public sealed class InquirePeriodProfitLossSummary
    {
        [JsonPropertyName("sll_qty_smtl")]
        public string SllQtySmtl { get; set; } = string.Empty;

        [JsonPropertyName("sll_tr_amt_smtl")]
        public string SllTrAmtSmtl { get; set; } = string.Empty;

        [JsonPropertyName("sll_fee_smtl")]
        public string SllFeeSmtl { get; set; } = string.Empty;

        [JsonPropertyName("sll_tltx_smtl")]
        public string SllTltxSmtl { get; set; } = string.Empty;

        [JsonPropertyName("sll_excc_amt_smtl")]
        public string SllExccAmtSmtl { get; set; } = string.Empty;

        [JsonPropertyName("buy_qty_smtl")]
        public string BuyQtySmtl { get; set; } = string.Empty;

        [JsonPropertyName("buy_tr_amt_smtl")]
        public string BuyTrAmtSmtl { get; set; } = string.Empty;

        [JsonPropertyName("buy_fee_smtl")]
        public string BuyFeeSmtl { get; set; } = string.Empty;

        [JsonPropertyName("buy_tax_smtl")]
        public string BuyTaxSmtl { get; set; } = string.Empty;

        [JsonPropertyName("buy_excc_amt_smtl")]
        public string BuyExccAmtSmtl { get; set; } = string.Empty;

        [JsonPropertyName("tot_qty")]
        public string TotQty { get; set; } = string.Empty;

        [JsonPropertyName("tot_tr_amt")]
        public string TotTrAmt { get; set; } = string.Empty;

        [JsonPropertyName("tot_fee")]
        public string TotFee { get; set; } = string.Empty;

        [JsonPropertyName("tot_tltx")]
        public string TotTltx { get; set; } = string.Empty;

        [JsonPropertyName("tot_excc_amt")]
        public string TotExccAmt { get; set; } = string.Empty;

        [JsonPropertyName("tot_rlzt_pfls")]
        public string TotRlztPfls { get; set; } = string.Empty;

        [JsonPropertyName("loan_int")]
        public string LoanInt { get; set; } = string.Empty;
    }
}
