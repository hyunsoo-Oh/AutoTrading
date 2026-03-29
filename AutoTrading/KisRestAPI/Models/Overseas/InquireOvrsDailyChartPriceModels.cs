using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Overseas
{
    // =====================================================================
    // ===== 해외주식 종목/지수/환율 기간별시세 요청 DTO =====
    // [v1_해외주식-012] GET /uapi/overseas-price/v1/quotations/inquire-daily-chartprice
    //
    // [유의사항]
    // - 미국주식 조회 시 다우30 / 나스닥100 / S&P500 종목만 조회 가능
    //   더 많은 미국주식은 '해외주식기간별시세' API 사용
    // - 해외지수 당일 시세는 지연 or 종가 시세로 제공
    // =====================================================================

    public class InquireOvrsDailyChartPriceRequest
    {
        /// <summary>
        /// FID 조건 시장 분류 코드
        /// N:해외지수  X:환율  I:국채  S:금선물
        /// </summary>
        public string FID_COND_MRKT_DIV_CODE { get; set; } = "N";

        /// <summary>
        /// FID 입력 종목코드
        /// 해외주식 마스터 코드 참조 (포럼 > FAQ > 종목정보 다운로드(해외) > 해외지수)
        /// </summary>
        public string FID_INPUT_ISCD { get; set; } = string.Empty;

        /// <summary>시작일자 (YYYYMMDD)</summary>
        public string FID_INPUT_DATE_1 { get; set; } = string.Empty;

        /// <summary>종료일자 (YYYYMMDD)</summary>
        public string FID_INPUT_DATE_2 { get; set; } = string.Empty;

        /// <summary>
        /// 기간 분류 코드
        /// D:일  W:주  M:월  Y:년
        /// </summary>
        public string FID_PERIOD_DIV_CODE { get; set; } = "D";
    }

    // =====================================================================
    // ===== 해외주식 종목/지수/환율 기간별시세 응답 DTO =====
    // =====================================================================

    public class InquireOvrsDailyChartPriceResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>기본 정보 (단일 객체)</summary>
        [JsonPropertyName("output1")]
        public InquireOvrsDailyChartPriceOutput1? Output1 { get; set; }

        /// <summary>일자별 캔들 배열</summary>
        [JsonPropertyName("output2")]
        public List<InquireOvrsDailyChartPriceItem> Output2 { get; set; } = new();
    }

    // =====================================================================
    // ===== output1 ? 기본 정보 DTO =====
    // 소수점 4자리까지 제공 (해외지수/환율 특성)
    // =====================================================================

    public class InquireOvrsDailyChartPriceOutput1
    {
        /// <summary>전일 대비 (소수점 4자리)</summary>
        [JsonPropertyName("ovrs_nmix_prdy_vrss")]
        public string OvrsNmixPrdyVrss { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>전일 대비율 (소수점 2자리, %)</summary>
        [JsonPropertyName("prdy_ctrt")]
        public string PrdyCtrt { get; set; } = "0";

        /// <summary>전일 종가 (소수점 4자리)</summary>
        [JsonPropertyName("ovrs_nmix_prdy_clpr")]
        public string OvrsNmixPrdyClpr { get; set; } = "0";

        /// <summary>누적 거래량</summary>
        [JsonPropertyName("acml_vol")]
        public string AcmlVol { get; set; } = "0";

        /// <summary>HTS 한글 종목명</summary>
        [JsonPropertyName("hts_kor_isnm")]
        public string HtsKorIsnm { get; set; } = string.Empty;

        /// <summary>현재가 (소수점 4자리)</summary>
        [JsonPropertyName("ovrs_nmix_prpr")]
        public string OvrsNmixPrpr { get; set; } = "0";

        /// <summary>단축 종목코드</summary>
        [JsonPropertyName("stck_shrn_iscd")]
        public string StckShrnIscd { get; set; } = string.Empty;

        /// <summary>전일 거래량</summary>
        [JsonPropertyName("prdy_vol")]
        public string PrdyVol { get; set; } = "0";

        /// <summary>시가 (소수점 4자리)</summary>
        [JsonPropertyName("ovrs_prod_oprc")]
        public string OvrsProdOprc { get; set; } = "0";

        /// <summary>최고가 (소수점 4자리)</summary>
        [JsonPropertyName("ovrs_prod_hgpr")]
        public string OvrsProdHgpr { get; set; } = "0";

        /// <summary>최저가 (소수점 4자리)</summary>
        [JsonPropertyName("ovrs_prod_lwpr")]
        public string OvrsProdLwpr { get; set; } = "0";
    }

    // =====================================================================
    // ===== output2 ? 일자별 캔들 항목 DTO =====
    // =====================================================================

    public class InquireOvrsDailyChartPriceItem
    {
        /// <summary>영업 일자 (YYYYMMDD)</summary>
        [JsonPropertyName("stck_bsop_date")]
        public string StckBsopDate { get; set; } = string.Empty;

        /// <summary>종가 / 현재가 (소수점 4자리)</summary>
        [JsonPropertyName("ovrs_nmix_prpr")]
        public string OvrsNmixPrpr { get; set; } = "0";

        /// <summary>시가 (소수점 4자리)</summary>
        [JsonPropertyName("ovrs_nmix_oprc")]
        public string OvrsNmixOprc { get; set; } = "0";

        /// <summary>최고가 (소수점 4자리)</summary>
        [JsonPropertyName("ovrs_nmix_hgpr")]
        public string OvrsNmixHgpr { get; set; } = "0";

        /// <summary>최저가 (소수점 4자리)</summary>
        [JsonPropertyName("ovrs_nmix_lwpr")]
        public string OvrsNmixLwpr { get; set; } = "0";

        /// <summary>누적 거래량</summary>
        [JsonPropertyName("acml_vol")]
        public string AcmlVol { get; set; } = "0";

        /// <summary>변경 여부 ? 당일 체결 미발생으로 시가가 없는 경우 Y (차트 표시용)</summary>
        [JsonPropertyName("mod_yn")]
        public string ModYn { get; set; } = string.Empty;
    }
}
