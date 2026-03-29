using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Market
{
    // =====================================================================
    // ===== 주식현재가 체결 요청 DTO =====
    // [v1_국내주식-009] GET /uapi/domestic-stock/v1/quotations/inquire-ccnl
    // =====================================================================

    public class InquireCcnlRequest
    {
        /// <summary>조건 시장 분류 코드 (J:KRX, NX:NXT, UN:통합)</summary>
        public string FID_COND_MRKT_DIV_CODE { get; set; } = "J";

        /// <summary>입력 종목코드 (ex 005930 삼성전자)</summary>
        public string FID_INPUT_ISCD { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 주식현재가 체결 응답 DTO =====
    // output은 Object Array — 체결 내역 리스트
    // =====================================================================

    public class InquireCcnlResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>체결 내역 리스트 (시세 API와 달리 배열로 반환된다)</summary>
        [JsonPropertyName("output")]
        public List<InquireCcnlOutput> Output { get; set; } = new();
    }

    // =====================================================================
    // ===== 주식현재가 체결 output 항목 DTO =====
    // 각 항목은 하나의 체결 레코드를 나타낸다.
    // =====================================================================

    public class InquireCcnlOutput
    {
        /// <summary>주식 체결 시간 (HHMMSS)</summary>
        [JsonPropertyName("stck_cntg_hour")]
        public string StckCntgHour { get; set; } = string.Empty;

        /// <summary>주식 현재가</summary>
        [JsonPropertyName("stck_prpr")]
        public string StckPrpr { get; set; } = "0";

        /// <summary>전일 대비</summary>
        [JsonPropertyName("prdy_vrss")]
        public string PrdyVrss { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>체결 거래량</summary>
        [JsonPropertyName("cntg_vol")]
        public string CntgVol { get; set; } = "0";

        /// <summary>당일 체결강도</summary>
        [JsonPropertyName("tday_rltv")]
        public string TdayRltv { get; set; } = "0";

        /// <summary>전일 대비율</summary>
        [JsonPropertyName("prdy_ctrt")]
        public string PrdyCtrt { get; set; } = "0";
    }
}
