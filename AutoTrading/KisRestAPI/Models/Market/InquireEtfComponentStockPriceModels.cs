using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Market
{
    // =====================================================================
    // ===== ETF 구성종목시세 요청 DTO =====
    // [국내주식-073] GET /uapi/etfetn/v1/quotations/inquire-component-stock-price
    //
    // [유의사항]
    // - 실전 계좌 전용 API ? 모의투자 미지원
    // =====================================================================

    public class InquireEtfComponentStockPriceRequest
    {
        /// <summary>조건 시장 분류 코드 (J 고정)</summary>
        public string FID_COND_MRKT_DIV_CODE { get; set; } = "J";

        /// <summary>입력 종목코드 (ETF 종목코드)</summary>
        public string FID_INPUT_ISCD { get; set; } = string.Empty;

        /// <summary>조건 화면 분류 코드 (11216 고정)</summary>
        public string FID_COND_SCR_DIV_CODE { get; set; } = "11216";
    }

    // =====================================================================
    // ===== ETF 구성종목시세 응답 DTO =====
    // =====================================================================

    public class InquireEtfComponentStockPriceResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>ETF 요약 정보 (단일 객체)</summary>
        [JsonPropertyName("output1")]
        public InquireEtfComponentStockPriceOutput1? Output1 { get; set; }

        /// <summary>ETF 구성 종목별 시세 배열</summary>
        [JsonPropertyName("output2")]
        public List<InquireEtfComponentStockPriceItem> Output2 { get; set; } = new();
    }

    // =====================================================================
    // ===== output1 ? ETF 요약 정보 DTO =====
    // =====================================================================

    public class InquireEtfComponentStockPriceOutput1
    {
        /// <summary>주식 현재가 (ETF 현재가)</summary>
        [JsonPropertyName("stck_prpr")]
        public string StckPrpr { get; set; } = "0";

        /// <summary>전일 대비</summary>
        [JsonPropertyName("prdy_vrss")]
        public string PrdyVrss { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>전일 대비율 (%)</summary>
        [JsonPropertyName("prdy_ctrt")]
        public string PrdyCtrt { get; set; } = "0";

        /// <summary>ETF 구성종목 시가총액</summary>
        [JsonPropertyName("etf_cnfg_issu_avls")]
        public string EtfCnfgIssuAvls { get; set; } = "0";

        // ===== NAV =====

        /// <summary>NAV</summary>
        [JsonPropertyName("nav")]
        public string Nav { get; set; } = "0";

        /// <summary>NAV 전일 대비 부호</summary>
        [JsonPropertyName("nav_prdy_vrss_sign")]
        public string NavPrdyVrssSign { get; set; } = string.Empty;

        /// <summary>NAV 전일 대비</summary>
        [JsonPropertyName("nav_prdy_vrss")]
        public string NavPrdyVrss { get; set; } = "0";

        /// <summary>NAV 전일 대비율 (%)</summary>
        [JsonPropertyName("nav_prdy_ctrt")]
        public string NavPrdyCtrt { get; set; } = "0";

        /// <summary>NAV 전일 종가</summary>
        [JsonPropertyName("prdy_clpr_nav")]
        public string PrdyClprNav { get; set; } = "0";

        /// <summary>NAV 시가</summary>
        [JsonPropertyName("oprc_nav")]
        public string OprcNav { get; set; } = "0";

        /// <summary>NAV 고가</summary>
        [JsonPropertyName("hprc_nav")]
        public string HprcNav { get; set; } = "0";

        /// <summary>NAV 저가</summary>
        [JsonPropertyName("lprc_nav")]
        public string LprcNav { get; set; } = "0";

        // ===== ETF 구성 정보 =====

        /// <summary>ETF 순자산 총액</summary>
        [JsonPropertyName("etf_ntas_ttam")]
        public string EtfNtasTtam { get; set; } = "0";

        /// <summary>ETF CU 단위 증권 수</summary>
        [JsonPropertyName("etf_cu_unit_scrt_cnt")]
        public string EtfCuUnitScrtCnt { get; set; } = "0";

        /// <summary>ETF 구성 종목 수</summary>
        [JsonPropertyName("etf_cnfg_issu_cnt")]
        public string EtfCnfgIssuCnt { get; set; } = "0";
    }

    // =====================================================================
    // ===== output2 ? ETF 구성 종목별 시세 항목 DTO =====
    // =====================================================================

    public class InquireEtfComponentStockPriceItem
    {
        /// <summary>주식 단축 종목코드</summary>
        [JsonPropertyName("stck_shrn_iscd")]
        public string StckShrnIscd { get; set; } = string.Empty;

        /// <summary>HTS 한글 종목명</summary>
        [JsonPropertyName("hts_kor_isnm")]
        public string HtsKorIsnm { get; set; } = string.Empty;

        /// <summary>주식 현재가</summary>
        [JsonPropertyName("stck_prpr")]
        public string StckPrpr { get; set; } = "0";

        /// <summary>전일 대비</summary>
        [JsonPropertyName("prdy_vrss")]
        public string PrdyVrss { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>전일 대비율 (%)</summary>
        [JsonPropertyName("prdy_ctrt")]
        public string PrdyCtrt { get; set; } = "0";

        /// <summary>누적 거래량</summary>
        [JsonPropertyName("acml_vol")]
        public string AcmlVol { get; set; } = "0";

        /// <summary>누적 거래 대금</summary>
        [JsonPropertyName("acml_tr_pbmn")]
        public string AcmlTrPbmn { get; set; } = "0";

        /// <summary>당일 등락 비율 (%)</summary>
        [JsonPropertyName("tday_rsfl_rate")]
        public string TdayRsflRate { get; set; } = "0";

        /// <summary>전일 대비 거래량</summary>
        [JsonPropertyName("prdy_vrss_vol")]
        public string PrdyVrssVol { get; set; } = "0";

        /// <summary>거래대금 회전율</summary>
        [JsonPropertyName("tr_pbmn_tnrt")]
        public string TrPbmnTnrt { get; set; } = "0";

        /// <summary>HTS 시가총액</summary>
        [JsonPropertyName("hts_avls")]
        public string HtsAvls { get; set; } = "0";

        /// <summary>ETF 구성종목 시가총액</summary>
        [JsonPropertyName("etf_cnfg_issu_avls")]
        public string EtfCnfgIssuAvls { get; set; } = "0";

        /// <summary>ETF 구성종목 비중 (%)</summary>
        [JsonPropertyName("etf_cnfg_issu_rlim")]
        public string EtfCnfgIssuRlim { get; set; } = "0";

        /// <summary>ETF 구성종목 내 평가금액</summary>
        [JsonPropertyName("etf_vltn_amt")]
        public string EtfVltnAmt { get; set; } = "0";
    }
}
