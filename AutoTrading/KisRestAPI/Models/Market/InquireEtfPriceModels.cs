using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Market
{
    // =====================================================================
    // ===== ETF/ETN 현재가 요청 DTO =====
    // [v1_국내주식-068] GET /uapi/etfetn/v1/quotations/inquire-price
    //
    // [유의사항]
    // - 실전 계좌 전용 API ? 모의투자 미지원
    // =====================================================================

    public class InquireEtfPriceRequest
    {
        /// <summary>FID 입력 종목코드</summary>
        public string fid_input_iscd { get; set; } = string.Empty;

        /// <summary>FID 조건 시장 분류 코드 (J 고정)</summary>
        public string fid_cond_mrkt_div_code { get; set; } = "J";
    }

    // =====================================================================
    // ===== ETF/ETN 현재가 응답 DTO =====
    // =====================================================================

    public class InquireEtfPriceResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>ETF/ETN 현재가 상세 (단일 객체)</summary>
        [JsonPropertyName("output")]
        public InquireEtfPriceOutput? Output { get; set; }
    }

    // =====================================================================
    // ===== ETF/ETN 현재가 output DTO =====
    // =====================================================================

    public class InquireEtfPriceOutput
    {
        // ===== 현재가 / 전일 대비 =====

        /// <summary>주식 현재가</summary>
        [JsonPropertyName("stck_prpr")]
        public string StckPrpr { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>전일 대비</summary>
        [JsonPropertyName("prdy_vrss")]
        public string PrdyVrss { get; set; } = "0";

        /// <summary>전일 대비율 (%)</summary>
        [JsonPropertyName("prdy_ctrt")]
        public string PrdyCtrt { get; set; } = "0";

        // ===== 거래량 =====

        /// <summary>누적 거래량</summary>
        [JsonPropertyName("acml_vol")]
        public string AcmlVol { get; set; } = "0";

        /// <summary>전일 거래량</summary>
        [JsonPropertyName("prdy_vol")]
        public string PrdyVol { get; set; } = "0";

        // ===== 가격 정보 =====

        /// <summary>주식 상한가</summary>
        [JsonPropertyName("stck_mxpr")]
        public string StckMxpr { get; set; } = "0";

        /// <summary>주식 하한가</summary>
        [JsonPropertyName("stck_llam")]
        public string StckLlam { get; set; } = "0";

        /// <summary>주식 전일 종가</summary>
        [JsonPropertyName("stck_prdy_clpr")]
        public string StckPrdyClpr { get; set; } = "0";

        /// <summary>주식 시가</summary>
        [JsonPropertyName("stck_oprc")]
        public string StckOprc { get; set; } = "0";

        /// <summary>전일 종가 대비 시가 비율</summary>
        [JsonPropertyName("prdy_clpr_vrss_oprc_rate")]
        public string PrdyClprVrssOprcRate { get; set; } = "0";

        /// <summary>주식 최고가</summary>
        [JsonPropertyName("stck_hgpr")]
        public string StckHgpr { get; set; } = "0";

        /// <summary>전일 종가 대비 최고가 비율</summary>
        [JsonPropertyName("prdy_clpr_vrss_hgpr_rate")]
        public string PrdyClprVrssHgprRate { get; set; } = "0";

        /// <summary>주식 최저가</summary>
        [JsonPropertyName("stck_lwpr")]
        public string StckLwpr { get; set; } = "0";

        /// <summary>전일 종가 대비 최저가 비율</summary>
        [JsonPropertyName("prdy_clpr_vrss_lwpr_rate")]
        public string PrdyClprVrssLwprRate { get; set; } = "0";

        /// <summary>주식 기준가</summary>
        [JsonPropertyName("stck_sdpr")]
        public string StckSdpr { get; set; } = "0";

        /// <summary>주식 대용가</summary>
        [JsonPropertyName("stck_sspr")]
        public string StckSspr { get; set; } = "0";

        // ===== NAV =====

        /// <summary>전일 최종 NAV</summary>
        [JsonPropertyName("prdy_last_nav")]
        public string PrdyLastNav { get; set; } = "0";

        /// <summary>NAV</summary>
        [JsonPropertyName("nav")]
        public string Nav { get; set; } = "0";

        /// <summary>NAV 전일 대비</summary>
        [JsonPropertyName("nav_prdy_vrss")]
        public string NavPrdyVrss { get; set; } = "0";

        /// <summary>NAV 전일 대비 부호</summary>
        [JsonPropertyName("nav_prdy_vrss_sign")]
        public string NavPrdyVrssSign { get; set; } = string.Empty;

        /// <summary>NAV 전일 대비율 (%)</summary>
        [JsonPropertyName("nav_prdy_ctrt")]
        public string NavPrdyCtrt { get; set; } = "0";

        // ===== 추적 오차 / 괴리율 =====

        /// <summary>추적 오차율 (%)</summary>
        [JsonPropertyName("trc_errt")]
        public string TrcErrt { get; set; } = "0";

        /// <summary>괴리율 (%)</summary>
        [JsonPropertyName("dprt")]
        public string Dprt { get; set; } = "0";

        /// <summary>지수 대비율</summary>
        [JsonPropertyName("nmix_ctrt")]
        public string NmixCtrt { get; set; } = "0";

        /// <summary>ETF 추적 수익률 배수</summary>
        [JsonPropertyName("etf_trc_ert_mltp")]
        public string EtfTrcErtMltp { get; set; } = "0";

        // ===== ETF 순자산 =====

        /// <summary>ETF 유통 주수</summary>
        [JsonPropertyName("etf_crcl_stcn")]
        public string EtfCrclStcn { get; set; } = "0";

        /// <summary>ETF 순자산 총액</summary>
        [JsonPropertyName("etf_ntas_ttam")]
        public string EtfNtasTtam { get; set; } = "0";

        /// <summary>ETF 외화 순자산 총액</summary>
        [JsonPropertyName("etf_frcr_ntas_ttam")]
        public string EtfFrcrNtasTtam { get; set; } = "0";

        /// <summary>ETF 유통 순자산 총액</summary>
        [JsonPropertyName("etf_crcl_ntas_ttam")]
        public string EtfCrclNtasTtam { get; set; } = "0";

        /// <summary>ETF 외화 유통 순자산 총액</summary>
        [JsonPropertyName("etf_frcr_crcl_ntas_ttam")]
        public string EtfFrcrCrclNtasTtam { get; set; } = "0";

        /// <summary>ETF 외화 최종 순자산 가치 값</summary>
        [JsonPropertyName("etf_frcr_last_ntas_wrth_val")]
        public string EtfFrcrLastNtasWrthVal { get; set; } = "0";

        // ===== 외국인 =====

        /// <summary>외국인 한도 비율 (%)</summary>
        [JsonPropertyName("frgn_limt_rate")]
        public string FrgnLimtRate { get; set; } = "0";

        /// <summary>외국인 주문 가능 수량</summary>
        [JsonPropertyName("frgn_oder_able_qty")]
        public string FrgnOderAbleQty { get; set; } = "0";

        /// <summary>외국인 보유 수량</summary>
        [JsonPropertyName("frgn_hldn_qty")]
        public string FrgnHldnQty { get; set; } = "0";

        /// <summary>외국인 보유 수량 비율 (%)</summary>
        [JsonPropertyName("frgn_hldn_qty_rate")]
        public string FrgnHldnQtyRate { get; set; } = "0";

        // ===== ETF 구성 정보 =====

        /// <summary>ETF CU 단위 증권 수</summary>
        [JsonPropertyName("etf_cu_unit_scrt_cnt")]
        public string EtfCuUnitScrtCnt { get; set; } = "0";

        /// <summary>ETF 구성 종목 수</summary>
        [JsonPropertyName("etf_cnfg_issu_cnt")]
        public string EtfCnfgIssuCnt { get; set; } = "0";

        /// <summary>ETF 배당 주기</summary>
        [JsonPropertyName("etf_dvdn_cycl")]
        public string EtfDvdnCycl { get; set; } = string.Empty;

        /// <summary>통화 코드</summary>
        [JsonPropertyName("crcd")]
        public string Crcd { get; set; } = string.Empty;

        /// <summary>LP 주문 가능 구분 코드</summary>
        [JsonPropertyName("lp_oder_able_cls_code")]
        public string LpOderAbleClsCode { get; set; } = string.Empty;

        /// <summary>LP 보유 비율 (%)</summary>
        [JsonPropertyName("lp_hldn_rate")]
        public string LpHldnRate { get; set; } = "0";

        /// <summary>ETN LP 보유량</summary>
        [JsonPropertyName("lp_hldn_vol")]
        public string LpHldnVol { get; set; } = "0";

        // ===== 연중 고저 =====

        /// <summary>주식 연중 최고가</summary>
        [JsonPropertyName("stck_dryy_hgpr")]
        public string StckDryyHgpr { get; set; } = "0";

        /// <summary>연중 최고가 대비 현재가 비율 (%)</summary>
        [JsonPropertyName("dryy_hgpr_vrss_prpr_rate")]
        public string DryyHgprVrssPrprRate { get; set; } = "0";

        /// <summary>연중 최고가 일자 (YYYYMMDD)</summary>
        [JsonPropertyName("dryy_hgpr_date")]
        public string DryyHgprDate { get; set; } = string.Empty;

        /// <summary>주식 연중 최저가</summary>
        [JsonPropertyName("stck_dryy_lwpr")]
        public string StckDryyLwpr { get; set; } = "0";

        /// <summary>연중 최저가 대비 현재가 비율 (%)</summary>
        [JsonPropertyName("dryy_lwpr_vrss_prpr_rate")]
        public string DryyLwprVrssPrprRate { get; set; } = "0";

        /// <summary>연중 최저가 일자 (YYYYMMDD)</summary>
        [JsonPropertyName("dryy_lwpr_date")]
        public string DryyLwprDate { get; set; } = string.Empty;

        // ===== 분류 / 종목 정보 =====

        /// <summary>업종 한글 종목명 (거래소 정보 기준, 미제공 종목 있음)</summary>
        [JsonPropertyName("bstp_kor_isnm")]
        public string BstpKorIsnm { get; set; } = string.Empty;

        /// <summary>VI 적용 구분 코드</summary>
        [JsonPropertyName("vi_cls_code")]
        public string ViClsCode { get; set; } = string.Empty;

        /// <summary>상장 주수</summary>
        [JsonPropertyName("lstn_stcn")]
        public string LstnStcn { get; set; } = "0";

        /// <summary>ETF 대상지수 업종코드</summary>
        [JsonPropertyName("etf_trgt_nmix_bstp_code")]
        public string EtfTrgtNmixBstpCode { get; set; } = string.Empty;

        /// <summary>ETF 분류 명</summary>
        [JsonPropertyName("etf_div_name")]
        public string EtfDivName { get; set; } = string.Empty;

        /// <summary>ETF 대표 업종 한글 종목명</summary>
        [JsonPropertyName("etf_rprs_bstp_kor_isnm")]
        public string EtfRprsBstpKorIsnm { get; set; } = string.Empty;

        /// <summary>회원사 명</summary>
        [JsonPropertyName("mbcr_name")]
        public string MbcrName { get; set; } = string.Empty;

        /// <summary>주식 상장 일자 (YYYYMMDD)</summary>
        [JsonPropertyName("stck_lstn_date")]
        public string StckLstnDate { get; set; } = string.Empty;

        /// <summary>만기 일자 (YYYYMMDD)</summary>
        [JsonPropertyName("mtrt_date")]
        public string MtrtDate { get; set; } = string.Empty;

        /// <summary>분배금 형태 코드</summary>
        [JsonPropertyName("shrg_type_code")]
        public string ShrgTypeCode { get; set; } = string.Empty;
    }
}
