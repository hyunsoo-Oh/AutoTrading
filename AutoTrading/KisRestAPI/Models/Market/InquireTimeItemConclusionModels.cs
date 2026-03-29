using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Market
{
    // =====================================================================
    // ===== 주식현재가 당일시간대별체결 요청 DTO =====
    // [v1_국내주식-023] GET /uapi/domestic-stock/v1/quotations/inquire-time-itemconclusion
    // 특정 시각 이전의 당일 시간대별 체결 내역을 조회한다.
    // =====================================================================

    public class InquireTimeItemConclusionRequest
    {
        /// <summary>조건 시장 분류 코드 (J:KRX, NX:NXT, UN:통합)</summary>
        public string FID_COND_MRKT_DIV_CODE { get; set; } = "J";

        /// <summary>입력 종목코드 (ex 005930 삼성전자)</summary>
        public string FID_INPUT_ISCD { get; set; } = string.Empty;

        /// <summary>입력 시간 (HHmmss) ? 해당 시각 이전의 체결 내역을 반환한다.</summary>
        public string FID_INPUT_HOUR_1 { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 주식현재가 당일시간대별체결 응답 DTO =====
    // =====================================================================

    public class InquireTimeItemConclusionResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>종목 현황 요약 (단일 객체)</summary>
        [JsonPropertyName("output1")]
        public InquireTimeItemConclusionOutput1? Output1 { get; set; }

        /// <summary>시간대별 체결 목록</summary>
        [JsonPropertyName("output2")]
        public List<InquireTimeItemConclusionItem> Output2 { get; set; } = new();
    }

    // =====================================================================
    // ===== output1 ? 종목 현황 요약 DTO =====
    // =====================================================================

    public class InquireTimeItemConclusionOutput1
    {
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

        /// <summary>전일 거래량</summary>
        [JsonPropertyName("prdy_vol")]
        public string PrdyVol { get; set; } = "0";

        /// <summary>대표 시장 한글 명</summary>
        [JsonPropertyName("rprs_mrkt_kor_name")]
        public string RprsMrktKorName { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== output2 ? 시간대별 체결 항목 DTO =====
    // =====================================================================

    public class InquireTimeItemConclusionItem
    {
        /// <summary>주식 체결 시간 (HHmmss)</summary>
        [JsonPropertyName("stck_cntg_hour")]
        public string StckCntgHour { get; set; } = string.Empty;

        /// <summary>주식 현재가</summary>
        [JsonPropertyName("stck_pbpr")]
        public string StckPbpr { get; set; } = "0";

        /// <summary>전일 대비</summary>
        [JsonPropertyName("prdy_vrss")]
        public string PrdyVrss { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>전일 대비율 (%)</summary>
        [JsonPropertyName("prdy_ctrt")]
        public string PrdyCtrt { get; set; } = "0";

        /// <summary>매도호가</summary>
        [JsonPropertyName("askp")]
        public string Askp { get; set; } = "0";

        /// <summary>매수호가</summary>
        [JsonPropertyName("bidp")]
        public string Bidp { get; set; } = "0";

        /// <summary>당일 체결강도</summary>
        [JsonPropertyName("tday_rltv")]
        public string TdayRltv { get; set; } = "0";

        /// <summary>누적 거래량</summary>
        [JsonPropertyName("acml_vol")]
        public string AcmlVol { get; set; } = "0";

        /// <summary>체결량</summary>
        [JsonPropertyName("cnqn")]
        public string Cnqn { get; set; } = "0";
    }
}
