using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Market
{
    // =====================================================================
    // ===== 금리 종합(국내채권/금리) 요청 DTO =====
    // [국내주식-155] GET /uapi/domestic-stock/v1/quotations/comp-interest
    //
    // [유의사항]
    // - 실전 계좌 전용 API ? 모의투자 미지원
    // - 11:30 이후에 신규 데이터가 수신된다.
    // - FID_COND_MRKT_DIV_CODE : "I" 고정
    // - FID_COND_SCR_DIV_CODE  : "20702" 고정
    // - FID_DIV_CLS_CODE       : 1 (해외금리지표)
    // - FID_DIV_CLS_CODE1      : 공백 = 전체
    // =====================================================================

    public class CompInterestRequest
    {
        /// <summary>조건 시장 분류 코드 (I 고정)</summary>
        public string FID_COND_MRKT_DIV_CODE { get; set; } = "I";

        /// <summary>조건 화면 분류 코드 (20702 고정)</summary>
        public string FID_COND_SCR_DIV_CODE { get; set; } = "20702";

        /// <summary>분류 구분 코드 (1 : 해외금리지표)</summary>
        public string FID_DIV_CLS_CODE { get; set; } = "1";

        /// <summary>분류 구분 코드1 (공백 : 전체)</summary>
        public string FID_DIV_CLS_CODE1 { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 금리 종합 응답 DTO =====
    // =====================================================================

    public class CompInterestResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>해외 금리지표 배열</summary>
        [JsonPropertyName("output1")]
        public List<CompInterestOutput1Item> Output1 { get; set; } = new();

        /// <summary>국내 채권/금리 현재값 배열</summary>
        [JsonPropertyName("output2")]
        public List<CompInterestOutput2Item> Output2 { get; set; } = new();
    }

    // =====================================================================
    // ===== output1 ? 해외 금리지표 항목 DTO =====
    // =====================================================================

    public class CompInterestOutput1Item
    {
        /// <summary>자료 코드</summary>
        [JsonPropertyName("bcdt_code")]
        public string BcdtCode { get; set; } = string.Empty;

        /// <summary>HTS 한글 종목명</summary>
        [JsonPropertyName("hts_kor_isnm")]
        public string HtsKorIsnm { get; set; } = string.Empty;

        /// <summary>채권 금리 현재가 (%)</summary>
        [JsonPropertyName("bond_mnrt_prpr")]
        public string BondMnrtPrpr { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>채권 금리 전일 대비</summary>
        [JsonPropertyName("bond_mnrt_prdy_vrss")]
        public string BondMnrtPrdyVrss { get; set; } = "0";

        /// <summary>전일 대비율 (%)</summary>
        [JsonPropertyName("prdy_ctrt")]
        public string PrdyCtrt { get; set; } = "0";

        /// <summary>주식 영업 일자 (YYYYMMDD)</summary>
        [JsonPropertyName("stck_bsop_date")]
        public string StckBsopDate { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== output2 ? 국내 채권/금리 항목 DTO =====
    // =====================================================================

    public class CompInterestOutput2Item
    {
        /// <summary>자료 코드</summary>
        [JsonPropertyName("bcdt_code")]
        public string BcdtCode { get; set; } = string.Empty;

        /// <summary>HTS 한글 종목명</summary>
        [JsonPropertyName("hts_kor_isnm")]
        public string HtsKorIsnm { get; set; } = string.Empty;

        /// <summary>채권 금리 현재가 (%)</summary>
        [JsonPropertyName("bond_mnrt_prpr")]
        public string BondMnrtPrpr { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>채권 금리 전일 대비</summary>
        [JsonPropertyName("bond_mnrt_prdy_vrss")]
        public string BondMnrtPrdyVrss { get; set; } = "0";

        /// <summary>국내 채권/금리 전일 대비율 (%)</summary>
        [JsonPropertyName("bstp_nmix_prdy_ctrt")]
        public string BstpNmixPrdyCtrt { get; set; } = "0";

        /// <summary>주식 영업 일자 (YYYYMMDD)</summary>
        [JsonPropertyName("stck_bsop_date")]
        public string StckBsopDate { get; set; } = string.Empty;
    }
}

