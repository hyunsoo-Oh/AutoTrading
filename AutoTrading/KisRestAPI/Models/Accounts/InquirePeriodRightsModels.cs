using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Accounts
{
    // =====================================================================
    // ===== 기간별당사권리현황조회 요청 DTO =====
    // 실투자 전용 API — 모의투자 미지원
    // =====================================================================

    public sealed class InquirePeriodRightsRequest
    {
        /// <summary>조회구분 — "03" 입력</summary>
        public string INQR_DVSN { get; set; } = "03";

        /// <summary>고객권리확인번호25 — 공란 입력</summary>
        public string CUST_RNCNO25 { get; set; } = string.Empty;

        /// <summary>홈페이지ID — 공란 입력</summary>
        public string HMID { get; set; } = string.Empty;

        /// <summary>종합계좌번호 — 8자리</summary>
        public string CANO { get; set; } = string.Empty;

        /// <summary>계좌상품코드 — 2자리 (기본 "01")</summary>
        public string ACNT_PRDT_CD { get; set; } = "01";

        /// <summary>조회시작일자 (YYYYMMDD)</summary>
        public string INQR_STRT_DT { get; set; } = string.Empty;

        /// <summary>조회종료일자 (YYYYMMDD)</summary>
        public string INQR_END_DT { get; set; } = string.Empty;

        /// <summary>권리종류코드 — 공란 입력 시 전체</summary>
        public string RGHT_TYPE_CD { get; set; } = string.Empty;

        /// <summary>상품번호 (종목코드) — 공란 입력 시 전체</summary>
        public string PDNO { get; set; } = string.Empty;

        /// <summary>상품유형코드 — 공란 입력</summary>
        public string PRDT_TYPE_CD { get; set; } = string.Empty;

        /// <summary>연속조회키100</summary>
        public string CTX_AREA_NK100 { get; set; } = string.Empty;

        /// <summary>연속조회검색조건100</summary>
        public string CTX_AREA_FK100 { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 기간별당사권리현황조회 응답 DTO =====
    // =====================================================================

    public sealed class InquirePeriodRightsResponse
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
        public List<InquirePeriodRightsItem> Output1 { get; set; } = new();
    }

    // =====================================================================
    // ===== 기간별당사권리현황조회 output1 항목 DTO =====
    // =====================================================================

    public sealed class InquirePeriodRightsItem
    {
        [JsonPropertyName("acno10")]
        public string Acno10 { get; set; } = string.Empty;

        [JsonPropertyName("rght_type_cd")]
        public string RghtTypeCd { get; set; } = string.Empty;

        [JsonPropertyName("bass_dt")]
        public string BassDt { get; set; } = string.Empty;

        [JsonPropertyName("rght_cblc_type_cd")]
        public string RghtCblcTypeCd { get; set; } = string.Empty;

        [JsonPropertyName("rptt_pdno")]
        public string RpttPdno { get; set; } = string.Empty;

        [JsonPropertyName("pdno")]
        public string Pdno { get; set; } = string.Empty;

        [JsonPropertyName("prdt_type_cd")]
        public string PrdtTypeCd { get; set; } = string.Empty;

        [JsonPropertyName("shtn_pdno")]
        public string ShtnPdno { get; set; } = string.Empty;

        [JsonPropertyName("prdt_name")]
        public string PrdtName { get; set; } = string.Empty;

        [JsonPropertyName("cblc_qty")]
        public string CblcQty { get; set; } = string.Empty;

        [JsonPropertyName("last_alct_qty")]
        public string LastAlctQty { get; set; } = string.Empty;

        [JsonPropertyName("excs_alct_qty")]
        public string ExcsAlctQty { get; set; } = string.Empty;

        [JsonPropertyName("tot_alct_qty")]
        public string TotAlctQty { get; set; } = string.Empty;

        [JsonPropertyName("last_ftsk_qty")]
        public string LastFtskQty { get; set; } = string.Empty;

        [JsonPropertyName("last_alct_amt")]
        public string LastAlctAmt { get; set; } = string.Empty;

        [JsonPropertyName("last_ftsk_chgs")]
        public string LastFtskChgs { get; set; } = string.Empty;

        [JsonPropertyName("rdpt_prca")]
        public string RdptPrca { get; set; } = string.Empty;

        [JsonPropertyName("dlay_int_amt")]
        public string DlayIntAmt { get; set; } = string.Empty;

        [JsonPropertyName("lstg_dt")]
        public string LstgDt { get; set; } = string.Empty;

        [JsonPropertyName("sbsc_end_dt")]
        public string SbscEndDt { get; set; } = string.Empty;

        [JsonPropertyName("cash_dfrm_dt")]
        public string CashDfrmDt { get; set; } = string.Empty;

        [JsonPropertyName("rqst_qty")]
        public string RqstQty { get; set; } = string.Empty;

        [JsonPropertyName("rqst_amt")]
        public string RqstAmt { get; set; } = string.Empty;

        [JsonPropertyName("rqst_dt")]
        public string RqstDt { get; set; } = string.Empty;

        [JsonPropertyName("rfnd_dt")]
        public string RfndDt { get; set; } = string.Empty;

        [JsonPropertyName("rfnd_amt")]
        public string RfndAmt { get; set; } = string.Empty;

        [JsonPropertyName("lstg_stqt")]
        public string LstgStqt { get; set; } = string.Empty;

        [JsonPropertyName("tax_amt")]
        public string TaxAmt { get; set; } = string.Empty;

        [JsonPropertyName("sbsc_unpr")]
        public string SbscUnpr { get; set; } = string.Empty;
    }
}
