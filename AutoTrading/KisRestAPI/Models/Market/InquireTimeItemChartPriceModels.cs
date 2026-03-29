using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Market
{
    // =====================================================================
    // ===== 주식당일분봉조회 요청 DTO =====
    // [v1_국내주식-022] GET /uapi/domestic-stock/v1/quotations/inquire-time-itemchartprice
    // 당일 분봉 데이터를 한 번의 호출에 최대 30건 조회한다.
    //
    // [유의사항]
    // - 당일 분봉 데이터만 제공 (전일자 분봉 미제공)
    // - FID_INPUT_HOUR_1에 미래 시각 입력 시 현재가로 조회됨
    // =====================================================================

    public class InquireTimeItemChartPriceRequest
    {
        /// <summary>조건 시장 분류 코드 (J:KRX, NX:NXT, UN:통합)</summary>
        public string FID_COND_MRKT_DIV_CODE { get; set; } = "J";

        /// <summary>입력 종목코드 (ex 005930 삼성전자)</summary>
        public string FID_INPUT_ISCD { get; set; } = string.Empty;

        /// <summary>
        /// 입력 시간 (HHmmss)
        /// 해당 시각 이전 최대 30건의 분봉을 반환한다.
        /// ex) "153000" → 15:30 이전 분봉 30건
        /// </summary>
        public string FID_INPUT_HOUR_1 { get; set; } = string.Empty;

        /// <summary>과거 데이터 포함 여부 (Y/N)</summary>
        public string FID_PW_DATA_INCU_YN { get; set; } = "Y";

        /// <summary>기타 구분 코드 (기본값 빈 문자열)</summary>
        public string FID_ETC_CLS_CODE { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 주식당일분봉조회 응답 DTO =====
    // =====================================================================

    public class InquireTimeItemChartPriceResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>종목 현황 요약 (단일 객체)</summary>
        [JsonPropertyName("output1")]
        public InquireTimeItemChartPriceOutput1? Output1 { get; set; }

        /// <summary>
        /// 분봉 배열 (최대 30건, 최신순)
        /// ※ 첫 번째 배열의 체결량(CntgVol)은 해당 분봉의 첫 체결 전까지
        ///    이전 분봉의 체결량이 표시된다.
        /// </summary>
        [JsonPropertyName("output2")]
        public List<InquireTimeItemChartPriceItem> Output2 { get; set; } = new();
    }

    // =====================================================================
    // ===== output1 ? 종목 현황 요약 DTO =====
    // =====================================================================

    public class InquireTimeItemChartPriceOutput1
    {
        /// <summary>전일 대비</summary>
        [JsonPropertyName("prdy_vrss")]
        public string PrdyVrss { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>전일 대비율 (소수점 두 자리)</summary>
        [JsonPropertyName("prdy_ctrt")]
        public string PrdyCtrt { get; set; } = "0";

        /// <summary>전일 종가</summary>
        [JsonPropertyName("stck_prdy_clpr")]
        public string StckPrdyClpr { get; set; } = "0";

        /// <summary>누적 거래량</summary>
        [JsonPropertyName("acml_vol")]
        public string AcmlVol { get; set; } = "0";

        /// <summary>누적 거래대금</summary>
        [JsonPropertyName("acml_tr_pbmn")]
        public string AcmlTrPbmn { get; set; } = "0";

        /// <summary>HTS 한글 종목명</summary>
        [JsonPropertyName("hts_kor_isnm")]
        public string HtsKorIsnm { get; set; } = string.Empty;

        /// <summary>주식 현재가</summary>
        [JsonPropertyName("stck_prpr")]
        public string StckPrpr { get; set; } = "0";
    }

    // =====================================================================
    // ===== output2 ? 분봉 캔들 항목 DTO =====
    // =====================================================================

    public class InquireTimeItemChartPriceItem
    {
        /// <summary>주식 영업 일자 (YYYYMMDD)</summary>
        [JsonPropertyName("stck_bsop_date")]
        public string StckBsopDate { get; set; } = string.Empty;

        /// <summary>주식 체결 시간 (HHmmss)</summary>
        [JsonPropertyName("stck_cntg_hour")]
        public string StckCntgHour { get; set; } = string.Empty;

        /// <summary>주식 현재가 (해당 분봉의 종가)</summary>
        [JsonPropertyName("stck_prpr")]
        public string StckPrpr { get; set; } = "0";

        /// <summary>주식 시가</summary>
        [JsonPropertyName("stck_oprc")]
        public string StckOprc { get; set; } = "0";

        /// <summary>주식 최고가</summary>
        [JsonPropertyName("stck_hgpr")]
        public string StckHgpr { get; set; } = "0";

        /// <summary>주식 최저가</summary>
        [JsonPropertyName("stck_lwpr")]
        public string StckLwpr { get; set; } = "0";

        /// <summary>
        /// 체결 거래량
        /// ※ 첫 번째 항목은 해당 분봉의 첫 체결 전까지 이전 분봉 체결량이 표시됨
        /// </summary>
        [JsonPropertyName("cntg_vol")]
        public string CntgVol { get; set; } = "0";

        /// <summary>누적 거래대금</summary>
        [JsonPropertyName("acml_tr_pbmn")]
        public string AcmlTrPbmn { get; set; } = "0";
    }
}
