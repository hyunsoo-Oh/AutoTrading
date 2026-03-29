using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Accounts
{
    // =====================================================================
    // ===== 주식통합증거금 현황 요청 DTO =====
    // 실투자 전용 API — 모의투자 미지원 / 단건조회 불가 API
    // =====================================================================

    public sealed class InquireIntgrMarginRequest
    {
        /// <summary>종합계좌번호 — 8자리</summary>
        public string CANO { get; set; } = string.Empty;

        /// <summary>계좌상품코드 — 2자리 (기본 "01")</summary>
        public string ACNT_PRDT_CD { get; set; } = "01";

        /// <summary>CMA평가금액포함여부 — "N" 입력</summary>
        public string CMA_EVLU_AMT_ICLD_YN { get; set; } = "N";

        /// <summary>원화외화구분코드 — "01": 원화기준, "02": 외화기준</summary>
        public string WCRC_FRCR_DVSN_CD { get; set; } = "02";

        /// <summary>선물환약정외화구분코드 — "01": 원화기준, "02": 외화기준</summary>
        public string FWEX_CTRT_FRCR_DVSN_CD { get; set; } = "02";
    }

    // =====================================================================
    // ===== 주식통합증거금 현황 응답 DTO =====
    // output1/output2가 아닌 단일 "output" Object를 반환
    // =====================================================================

    public sealed class InquireIntgrMarginResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        [JsonPropertyName("output")]
        public InquireIntgrMarginOutput? Output { get; set; }
    }

    // =====================================================================
    // ===== 주식통합증거금 현황 output 단일 Object DTO =====
    // =====================================================================

    public sealed class InquireIntgrMarginOutput
    {
        // ===== 국내 증거금 기본 정보 =====

        [JsonPropertyName("acmga_rt")]
        public string AcmgaRt { get; set; } = string.Empty;

        [JsonPropertyName("acmga_pct100_aptm_rson")]
        public string AcmgaPct100AptmRson { get; set; } = string.Empty;

        // ===== 국내 주식 — 대상금액 =====

        [JsonPropertyName("stck_cash_objt_amt")]
        public string StckCashObjtAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_sbst_objt_amt")]
        public string StckSbstObjtAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_evlu_objt_amt")]
        public string StckEvluObjtAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_ruse_psbl_objt_amt")]
        public string StckRusePsblObjtAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_fund_rpch_chgs_objt_amt")]
        public string StckFundRpchChgsObjtAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_fncg_rdpt_objt_atm")]
        public string StckFncgRdptObjtAtm { get; set; } = string.Empty;

        [JsonPropertyName("bond_ruse_psbl_objt_amt")]
        public string BondRusePsblObjtAmt { get; set; } = string.Empty;

        // ===== 국내 주식 — 사용금액 =====

        [JsonPropertyName("stck_cash_use_amt")]
        public string StckCashUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_sbst_use_amt")]
        public string StckSbstUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_evlu_use_amt")]
        public string StckEvluUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_ruse_psbl_amt_use_amt")]
        public string StckRusePsblAmtUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_fund_rpch_chgs_use_amt")]
        public string StckFundRpchChgsUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_fncg_rdpt_amt_use_amt")]
        public string StckFncgRdptAmtUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("bond_ruse_psbl_amt_use_amt")]
        public string BondRusePsblAmtUseAmt { get; set; } = string.Empty;

        // ===== 국내 주식 — 주문가능금액 =====

        [JsonPropertyName("stck_cash_ord_psbl_amt")]
        public string StckCashOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_sbst_ord_psbl_amt")]
        public string StckSbstOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_evlu_ord_psbl_amt")]
        public string StckEvluOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_ruse_psbl_ord_psbl_amt")]
        public string StckRusePsblOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_fund_rpch_ord_psbl_amt")]
        public string StckFundRpchOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("bond_ruse_psbl_ord_psbl_amt")]
        public string BondRusePsblOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("rcvb_amt")]
        public string RcvbAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_loan_grta_ruse_psbl_amt")]
        public string StckLoanGrtaRusePsblAmt { get; set; } = string.Empty;

        // ===== 국내 주식 — 증거금율별 최대 주문가능금액 =====

        [JsonPropertyName("stck_cash20_max_ord_psbl_amt")]
        public string StckCash20MaxOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_cash30_max_ord_psbl_amt")]
        public string StckCash30MaxOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_cash40_max_ord_psbl_amt")]
        public string StckCash40MaxOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_cash50_max_ord_psbl_amt")]
        public string StckCash50MaxOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_cash60_max_ord_psbl_amt")]
        public string StckCash60MaxOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_cash100_max_ord_psbl_amt")]
        public string StckCash100MaxOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_rsip100_max_ord_psbl_amt")]
        public string StckRsip100MaxOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("bond_max_ord_psbl_amt")]
        public string BondMaxOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_fncg45_max_ord_psbl_amt")]
        public string StckFncg45MaxOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_fncg50_max_ord_psbl_amt")]
        public string StckFncg50MaxOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_fncg60_max_ord_psbl_amt")]
        public string StckFncg60MaxOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_fncg70_max_ord_psbl_amt")]
        public string StckFncg70MaxOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_stln_max_ord_psbl_amt")]
        public string StckStlnMaxOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("lmt_amt")]
        public string LmtAmt { get; set; } = string.Empty;

        // ===== 해외 주식 — 통합증거금 기본 =====

        [JsonPropertyName("ovrs_stck_itgr_mgna_dvsn_name")]
        public string OvrsStckItgrMgnaDvsnName { get; set; } = string.Empty;

        // ===== 해외 주식 — 통화별 대상/사용/주문가능 금액 =====

        [JsonPropertyName("usd_objt_amt")]
        public string UsdObjtAmt { get; set; } = string.Empty;

        [JsonPropertyName("usd_use_amt")]
        public string UsdUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("usd_ord_psbl_amt")]
        public string UsdOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("hkd_objt_amt")]
        public string HkdObjtAmt { get; set; } = string.Empty;

        [JsonPropertyName("hkd_use_amt")]
        public string HkdUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("hkd_ord_psbl_amt")]
        public string HkdOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("jpy_objt_amt")]
        public string JpyObjtAmt { get; set; } = string.Empty;

        [JsonPropertyName("jpy_use_amt")]
        public string JpyUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("jpy_ord_psbl_amt")]
        public string JpyOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("cny_objt_amt")]
        public string CnyObjtAmt { get; set; } = string.Empty;

        [JsonPropertyName("cny_use_amt")]
        public string CnyUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("cny_ord_psbl_amt")]
        public string CnyOrdPsblAmt { get; set; } = string.Empty;

        // ===== 해외 주식 — 통화별 재사용 대상/사용/주문가능 금액 =====

        [JsonPropertyName("usd_ruse_objt_amt")]
        public string UsdRuseObjtAmt { get; set; } = string.Empty;

        [JsonPropertyName("usd_ruse_amt")]
        public string UsdRuseAmt { get; set; } = string.Empty;

        [JsonPropertyName("usd_ruse_ord_psbl_amt")]
        public string UsdRuseOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("hkd_ruse_objt_amt")]
        public string HkdRuseObjtAmt { get; set; } = string.Empty;

        [JsonPropertyName("hkd_ruse_amt")]
        public string HkdRuseAmt { get; set; } = string.Empty;

        [JsonPropertyName("hkd_ruse_ord_psbl_amt")]
        public string HkdRuseOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("jpy_ruse_objt_amt")]
        public string JpyRuseObjtAmt { get; set; } = string.Empty;

        [JsonPropertyName("jpy_ruse_amt")]
        public string JpyRuseAmt { get; set; } = string.Empty;

        [JsonPropertyName("jpy_ruse_ord_psbl_amt")]
        public string JpyRuseOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("cny_ruse_objt_amt")]
        public string CnyRuseObjtAmt { get; set; } = string.Empty;

        [JsonPropertyName("cny_ruse_amt")]
        public string CnyRuseAmt { get; set; } = string.Empty;

        [JsonPropertyName("cny_ruse_ord_psbl_amt")]
        public string CnyRuseOrdPsblAmt { get; set; } = string.Empty;

        // ===== 해외 주식 — 일반/통합 주문가능금액 =====

        [JsonPropertyName("usd_gnrl_ord_psbl_amt")]
        public string UsdGnrlOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("usd_itgr_ord_psbl_amt")]
        public string UsdItgrOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("hkd_gnrl_ord_psbl_amt")]
        public string HkdGnrlOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("hkd_itgr_ord_psbl_amt")]
        public string HkdItgrOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("jpy_gnrl_ord_psbl_amt")]
        public string JpyGnrlOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("jpy_itgr_ord_psbl_amt")]
        public string JpyItgrOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("cny_gnrl_ord_psbl_amt")]
        public string CnyGnrlOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("cny_itgr_ord_psbl_amt")]
        public string CnyItgrOrdPsblAmt { get; set; } = string.Empty;

        // ===== 국내 주식 — 통합 증거금율별 주문가능금액 =====

        [JsonPropertyName("stck_itgr_cash20_ord_psbl_amt")]
        public string StckItgrCash20OrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_itgr_cash30_ord_psbl_amt")]
        public string StckItgrCash30OrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_itgr_cash40_ord_psbl_amt")]
        public string StckItgrCash40OrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_itgr_cash50_ord_psbl_amt")]
        public string StckItgrCash50OrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_itgr_cash60_ord_psbl_amt")]
        public string StckItgrCash60OrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_itgr_cash100_ord_psbl_amt")]
        public string StckItgrCash100OrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_itgr_100_ord_psbl_amt")]
        public string StckItgr100OrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_itgr_fncg45_ord_psbl_amt")]
        public string StckItgrFncg45OrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_itgr_fncg50_ord_psbl_amt")]
        public string StckItgrFncg50OrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_itgr_fncg60_ord_psbl_amt")]
        public string StckItgrFncg60OrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_itgr_fncg70_ord_psbl_amt")]
        public string StckItgrFncg70OrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_itgr_stln_ord_psbl_amt")]
        public string StckItgrStlnOrdPsblAmt { get; set; } = string.Empty;

        [JsonPropertyName("bond_itgr_ord_psbl_amt")]
        public string BondItgrOrdPsblAmt { get; set; } = string.Empty;

        // ===== 국내 주식 — 해외사용금액 =====

        [JsonPropertyName("stck_cash_ovrs_use_amt")]
        public string StckCashOvrsUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_sbst_ovrs_use_amt")]
        public string StckSbstOvrsUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_evlu_ovrs_use_amt")]
        public string StckEvluOvrsUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_re_use_amt_ovrs_use_amt")]
        public string StckReUseAmtOvrsUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_fund_rpch_ovrs_use_amt")]
        public string StckFundRpchOvrsUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("stck_fncg_rdpt_ovrs_use_amt")]
        public string StckFncgRdptOvrsUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("bond_re_use_ovrs_use_amt")]
        public string BondReUseOvrsUseAmt { get; set; } = string.Empty;

        // ===== 해외 주식 — 타시장사용금액 =====

        [JsonPropertyName("usd_oth_mket_use_amt")]
        public string UsdOthMketUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("jpy_oth_mket_use_amt")]
        public string JpyOthMketUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("cny_oth_mket_use_amt")]
        public string CnyOthMketUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("hkd_oth_mket_use_amt")]
        public string HkdOthMketUseAmt { get; set; } = string.Empty;

        // ===== 해외 주식 — 재사용 타시장사용금액 =====

        [JsonPropertyName("usd_re_use_oth_mket_use_amt")]
        public string UsdReUseOthMketUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("jpy_re_use_oth_mket_use_amt")]
        public string JpyReUseOthMketUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("cny_re_use_oth_mket_use_amt")]
        public string CnyReUseOthMketUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("hkd_re_use_oth_mket_use_amt")]
        public string HkdReUseOthMketUseAmt { get; set; } = string.Empty;

        [JsonPropertyName("hgkg_cny_re_use_amt")]
        public string HgkgCnyReUseAmt { get; set; } = string.Empty;

        // ===== 환율 정보 =====

        [JsonPropertyName("usd_frst_bltn_exrt")]
        public string UsdFrstBltnExrt { get; set; } = string.Empty;

        [JsonPropertyName("hkd_frst_bltn_exrt")]
        public string HkdFrstBltnExrt { get; set; } = string.Empty;

        [JsonPropertyName("jpy_frst_bltn_exrt")]
        public string JpyFrstBltnExrt { get; set; } = string.Empty;

        [JsonPropertyName("cny_frst_bltn_exrt")]
        public string CnyFrstBltnExrt { get; set; } = string.Empty;
    }
}
