using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Market
{
    // =====================================================================
    // ===== 주식현재가 시세 요청 DTO =====
    // [v1_국내주식-008] GET /uapi/domestic-stock/v1/quotations/inquire-price
    // =====================================================================

    public class InquirePriceRequest
    {
        /// <summary>조건 시장 분류 코드 (J:KRX, NX:NXT, UN:통합)</summary>
        public string FID_COND_MRKT_DIV_CODE { get; set; } = "J";

        /// <summary>입력 종목코드 (ex 005930 삼성전자)</summary>
        public string FID_INPUT_ISCD { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 주식현재가 시세 응답 DTO =====
    // =====================================================================

    public class InquirePriceResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        [JsonPropertyName("output")]
        public InquirePriceOutput? Output { get; set; }
    }

    // =====================================================================
    // ===== 주식현재가 시세 output 항목 DTO =====
    // 응답 필드가 80개 이상으로 매우 많다.
    // 자주 사용하는 핵심 필드 위주로 정의하고,
    // 나머지는 필요 시 추가할 수 있도록 구조를 열어둔다.
    // =====================================================================

    public class InquirePriceOutput
    {
        // ===== 종목 기본 정보 =====

        /// <summary>종목 상태 구분 코드 (51:관리종목 52:투자위험 등)</summary>
        [JsonPropertyName("iscd_stat_cls_code")]
        public string IscdStatClsCode { get; set; } = string.Empty;

        /// <summary>증거금 비율</summary>
        [JsonPropertyName("marg_rate")]
        public string MargRate { get; set; } = string.Empty;

        /// <summary>대표 시장 한글 명</summary>
        [JsonPropertyName("rprs_mrkt_kor_name")]
        public string RprsMrktKorName { get; set; } = string.Empty;

        /// <summary>업종 한글 종목명</summary>
        [JsonPropertyName("bstp_kor_isnm")]
        public string BstpKorIsnm { get; set; } = string.Empty;

        // ===== 현재가 / 전일 대비 =====

        /// <summary>주식 현재가</summary>
        [JsonPropertyName("stck_prpr")]
        public string StckPrpr { get; set; } = "0";

        /// <summary>전일 대비</summary>
        [JsonPropertyName("prdy_vrss")]
        public string PrdyVrss { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>전일 대비율</summary>
        [JsonPropertyName("prdy_ctrt")]
        public string PrdyCtrt { get; set; } = "0";

        // ===== 거래량 / 거래대금 =====

        /// <summary>누적 거래 대금</summary>
        [JsonPropertyName("acml_tr_pbmn")]
        public string AcmlTrPbmn { get; set; } = "0";

        /// <summary>누적 거래량</summary>
        [JsonPropertyName("acml_vol")]
        public string AcmlVol { get; set; } = "0";

        /// <summary>전일 대비 거래량 비율</summary>
        [JsonPropertyName("prdy_vrss_vol_rate")]
        public string PrdyVrssVolRate { get; set; } = "0";

        // ===== 시가 / 고가 / 저가 / 상한 / 하한 / 기준 =====

        /// <summary>주식 시가</summary>
        [JsonPropertyName("stck_oprc")]
        public string StckOprc { get; set; } = "0";

        /// <summary>주식 최고가</summary>
        [JsonPropertyName("stck_hgpr")]
        public string StckHgpr { get; set; } = "0";

        /// <summary>주식 최저가</summary>
        [JsonPropertyName("stck_lwpr")]
        public string StckLwpr { get; set; } = "0";

        /// <summary>주식 상한가</summary>
        [JsonPropertyName("stck_mxpr")]
        public string StckMxpr { get; set; } = "0";

        /// <summary>주식 하한가</summary>
        [JsonPropertyName("stck_llam")]
        public string StckLlam { get; set; } = "0";

        /// <summary>주식 기준가</summary>
        [JsonPropertyName("stck_sdpr")]
        public string StckSdpr { get; set; } = "0";

        // ===== 외국인 / 프로그램 =====

        /// <summary>HTS 외국인 소진율</summary>
        [JsonPropertyName("hts_frgn_ehrt")]
        public string HtsFrgnEhrt { get; set; } = "0";

        /// <summary>외국인 순매수 수량</summary>
        [JsonPropertyName("frgn_ntby_qty")]
        public string FrgnNtbyQty { get; set; } = "0";

        /// <summary>프로그램매매 순매수 수량</summary>
        [JsonPropertyName("pgtr_ntby_qty")]
        public string PgtrNtbyQty { get; set; } = "0";

        // ===== 피벗 / 디저항 / 디지지 =====

        /// <summary>피벗 2차 디저항 가격</summary>
        [JsonPropertyName("pvt_scnd_dmrs_prc")]
        public string PvtScndDmrsPrc { get; set; } = "0";

        /// <summary>피벗 1차 디저항 가격</summary>
        [JsonPropertyName("pvt_frst_dmrs_prc")]
        public string PvtFrstDmrsPrc { get; set; } = "0";

        /// <summary>피벗 포인트 값</summary>
        [JsonPropertyName("pvt_pont_val")]
        public string PvtPontVal { get; set; } = "0";

        /// <summary>피벗 1차 디지지 가격</summary>
        [JsonPropertyName("pvt_frst_dmsp_prc")]
        public string PvtFrstDmspPrc { get; set; } = "0";

        /// <summary>피벗 2차 디지지 가격</summary>
        [JsonPropertyName("pvt_scnd_dmsp_prc")]
        public string PvtScndDmspPrc { get; set; } = "0";

        /// <summary>디저항 값</summary>
        [JsonPropertyName("dmrs_val")]
        public string DmrsVal { get; set; } = "0";

        /// <summary>디지지 값</summary>
        [JsonPropertyName("dmsp_val")]
        public string DmspVal { get; set; } = "0";

        // ===== 기업 재무 정보 =====

        /// <summary>자본금</summary>
        [JsonPropertyName("cpfn")]
        public string Cpfn { get; set; } = "0";

        /// <summary>주식 액면가</summary>
        [JsonPropertyName("stck_fcam")]
        public string StckFcam { get; set; } = "0";

        /// <summary>주식 대용가</summary>
        [JsonPropertyName("stck_sspr")]
        public string StckSspr { get; set; } = "0";

        /// <summary>호가단위</summary>
        [JsonPropertyName("aspr_unit")]
        public string AsprUnit { get; set; } = "0";

        /// <summary>상장 주수</summary>
        [JsonPropertyName("lstn_stcn")]
        public string LstnStcn { get; set; } = "0";

        /// <summary>HTS 시가총액</summary>
        [JsonPropertyName("hts_avls")]
        public string HtsAvls { get; set; } = "0";

        /// <summary>PER</summary>
        [JsonPropertyName("per")]
        public string Per { get; set; } = "0";

        /// <summary>PBR</summary>
        [JsonPropertyName("pbr")]
        public string Pbr { get; set; } = "0";

        /// <summary>결산 월</summary>
        [JsonPropertyName("stac_month")]
        public string StacMonth { get; set; } = string.Empty;

        /// <summary>거래량 회전율</summary>
        [JsonPropertyName("vol_tnrt")]
        public string VolTnrt { get; set; } = "0";

        /// <summary>EPS</summary>
        [JsonPropertyName("eps")]
        public string Eps { get; set; } = "0";

        /// <summary>BPS</summary>
        [JsonPropertyName("bps")]
        public string Bps { get; set; } = "0";

        // ===== 250일 고저 =====

        /// <summary>250일 최고가</summary>
        [JsonPropertyName("d250_hgpr")]
        public string D250Hgpr { get; set; } = "0";

        /// <summary>250일 최고가 일자</summary>
        [JsonPropertyName("d250_hgpr_date")]
        public string D250HgprDate { get; set; } = string.Empty;

        /// <summary>250일 최고가 대비 현재가 비율</summary>
        [JsonPropertyName("d250_hgpr_vrss_prpr_rate")]
        public string D250HgprVrssPrprRate { get; set; } = "0";

        /// <summary>250일 최저가</summary>
        [JsonPropertyName("d250_lwpr")]
        public string D250Lwpr { get; set; } = "0";

        /// <summary>250일 최저가 일자</summary>
        [JsonPropertyName("d250_lwpr_date")]
        public string D250LwprDate { get; set; } = string.Empty;

        /// <summary>250일 최저가 대비 현재가 비율</summary>
        [JsonPropertyName("d250_lwpr_vrss_prpr_rate")]
        public string D250LwprVrssPrprRate { get; set; } = "0";

        // ===== 연중 고저 =====

        /// <summary>주식 연중 최고가</summary>
        [JsonPropertyName("stck_dryy_hgpr")]
        public string StckDryyHgpr { get; set; } = "0";

        /// <summary>연중 최고가 대비 현재가 비율</summary>
        [JsonPropertyName("dryy_hgpr_vrss_prpr_rate")]
        public string DryyHgprVrssPrprRate { get; set; } = "0";

        /// <summary>연중 최고가 일자</summary>
        [JsonPropertyName("dryy_hgpr_date")]
        public string DryyHgprDate { get; set; } = string.Empty;

        /// <summary>주식 연중 최저가</summary>
        [JsonPropertyName("stck_dryy_lwpr")]
        public string StckDryyLwpr { get; set; } = "0";

        /// <summary>연중 최저가 대비 현재가 비율</summary>
        [JsonPropertyName("dryy_lwpr_vrss_prpr_rate")]
        public string DryyLwprVrssPrprRate { get; set; } = "0";

        /// <summary>연중 최저가 일자</summary>
        [JsonPropertyName("dryy_lwpr_date")]
        public string DryyLwprDate { get; set; } = string.Empty;

        // ===== 52주 고저 =====

        /// <summary>52주일 최고가</summary>
        [JsonPropertyName("w52_hgpr")]
        public string W52Hgpr { get; set; } = "0";

        /// <summary>52주일 최고가 대비 현재가 대비</summary>
        [JsonPropertyName("w52_hgpr_vrss_prpr_ctrt")]
        public string W52HgprVrssPrprCtrt { get; set; } = "0";

        /// <summary>52주일 최고가 일자</summary>
        [JsonPropertyName("w52_hgpr_date")]
        public string W52HgprDate { get; set; } = string.Empty;

        /// <summary>52주일 최저가</summary>
        [JsonPropertyName("w52_lwpr")]
        public string W52Lwpr { get; set; } = "0";

        /// <summary>52주일 최저가 대비 현재가 대비</summary>
        [JsonPropertyName("w52_lwpr_vrss_prpr_ctrt")]
        public string W52LwprVrssPrprCtrt { get; set; } = "0";

        /// <summary>52주일 최저가 일자</summary>
        [JsonPropertyName("w52_lwpr_date")]
        public string W52LwprDate { get; set; } = string.Empty;

        // ===== 기타 =====

        /// <summary>전체 융자 잔고 비율</summary>
        [JsonPropertyName("whol_loan_rmnd_rate")]
        public string WholLoanRmndRate { get; set; } = "0";

        /// <summary>공매도가능여부</summary>
        [JsonPropertyName("ssts_yn")]
        public string SstsYn { get; set; } = string.Empty;

        /// <summary>주식 단축 종목코드</summary>
        [JsonPropertyName("stck_shrn_iscd")]
        public string StckShrnIscd { get; set; } = string.Empty;

        /// <summary>액면가 통화명</summary>
        [JsonPropertyName("fcam_cnnm")]
        public string FcamCnnm { get; set; } = string.Empty;

        /// <summary>자본금 통화명</summary>
        [JsonPropertyName("cpfn_cnnm")]
        public string CpfnCnnm { get; set; } = string.Empty;

        /// <summary>외국인 보유 수량</summary>
        [JsonPropertyName("frgn_hldn_qty")]
        public string FrgnHldnQty { get; set; } = "0";

        /// <summary>VI적용구분코드</summary>
        [JsonPropertyName("vi_cls_code")]
        public string ViClsCode { get; set; } = string.Empty;

        /// <summary>시간외단일가VI적용구분코드</summary>
        [JsonPropertyName("ovtm_vi_cls_code")]
        public string OvtmViClsCode { get; set; } = string.Empty;

        /// <summary>최종 공매도 체결 수량</summary>
        [JsonPropertyName("last_ssts_cntg_qty")]
        public string LastSstsCntgQty { get; set; } = "0";

        /// <summary>투자유의여부</summary>
        [JsonPropertyName("invt_caful_yn")]
        public string InvtCafulYn { get; set; } = string.Empty;

        /// <summary>시장경고코드</summary>
        [JsonPropertyName("mrkt_warn_cls_code")]
        public string MrktWarnClsCode { get; set; } = string.Empty;

        /// <summary>단기과열여부</summary>
        [JsonPropertyName("short_over_yn")]
        public string ShortOverYn { get; set; } = string.Empty;

        /// <summary>정리매매여부</summary>
        [JsonPropertyName("sltr_yn")]
        public string SltrYn { get; set; } = string.Empty;

        // ===== 추가 필드 (신 고가 저가 / 임시 정지 등) =====

        /// <summary>신 고가 저가 구분 코드</summary>
        [JsonPropertyName("new_hgpr_lwpr_cls_code")]
        public string NewHgprLwprClsCode { get; set; } = string.Empty;

        /// <summary>임시 정지 여부</summary>
        [JsonPropertyName("temp_stop_yn")]
        public string TempStopYn { get; set; } = string.Empty;

        /// <summary>시가 범위 연장 여부</summary>
        [JsonPropertyName("oprc_rang_cont_yn")]
        public string OprcRangContYn { get; set; } = string.Empty;

        /// <summary>종가 범위 연장 여부</summary>
        [JsonPropertyName("clpr_rang_cont_yn")]
        public string ClprRangContYn { get; set; } = string.Empty;

        /// <summary>신용 가능 여부</summary>
        [JsonPropertyName("crdt_able_yn")]
        public string CrdtAbleYn { get; set; } = string.Empty;

        /// <summary>보증금 비율 구분 코드</summary>
        [JsonPropertyName("grmn_rate_cls_code")]
        public string GrmnRateClsCode { get; set; } = string.Empty;

        /// <summary>ELW 발행 여부</summary>
        [JsonPropertyName("elw_pblc_yn")]
        public string ElwPblcYn { get; set; } = string.Empty;

        /// <summary>가중 평균 주식 가격</summary>
        [JsonPropertyName("wghn_avrg_stck_prc")]
        public string WghnAvrgStckPrc { get; set; } = "0";

        /// <summary>HTS 매매 수량 단위 값</summary>
        [JsonPropertyName("hts_deal_qty_unit_val")]
        public string HtsDealQtyUnitVal { get; set; } = "0";

        /// <summary>제한 폭 가격</summary>
        [JsonPropertyName("rstc_wdth_prc")]
        public string RstcWdthPrc { get; set; } = "0";

        /// <summary>접근도</summary>
        [JsonPropertyName("apprch_rate")]
        public string ApprchRate { get; set; } = "0";

        /// <summary>관리종목여부</summary>
        [JsonPropertyName("mang_issu_cls_code")]
        public string MangIssuClsCode { get; set; } = string.Empty;
    }
}
