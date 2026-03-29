using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Market
{
    // =====================================================================
    // ===== 주식현재가 일자별 요청 DTO =====
    // [v1_국내주식-010] GET /uapi/domestic-stock/v1/quotations/inquire-daily-price
    // 일/주/월별 주가를 최근 30일(주/월)로 조회한다.
    // =====================================================================

    public class InquireDailyPriceRequest
    {
        /// <summary>조건 시장 분류 코드 (J:KRX, NX:NXT, UN:통합)</summary>
        public string FID_COND_MRKT_DIV_CODE { get; set; } = "J";

        /// <summary>입력 종목코드 (ex 005930 삼성전자)</summary>
        public string FID_INPUT_ISCD { get; set; } = string.Empty;

        /// <summary>
        /// 기간 분류 코드
        /// D : (일) 최근 30거래일
        /// W : (주) 최근 30주
        /// M : (월) 최근 30개월
        /// </summary>
        public string FID_PERIOD_DIV_CODE { get; set; } = "D";

        /// <summary>
        /// 수정주가 원주가 가격
        /// 0 : 수정주가 미반영
        /// 1 : 수정주가 반영 (액면분할/액면병합 등 권리 발생 시 과거 시세를 현재 주가에 맞게 보정)
        /// </summary>
        public string FID_ORG_ADJ_PRC { get; set; } = "1";
    }

    // =====================================================================
    // ===== 주식현재가 일자별 응답 DTO =====
    // =====================================================================

    public class InquireDailyPriceResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>일자별 주가 배열 (최근 30일/주/월)</summary>
        [JsonPropertyName("output")]
        public List<InquireDailyPriceItem> Output { get; set; } = new();
    }

    // =====================================================================
    // ===== 주식현재가 일자별 output 항목 DTO =====
    // =====================================================================

    public class InquireDailyPriceItem
    {
        /// <summary>주식 영업 일자 (YYYYMMDD)</summary>
        [JsonPropertyName("stck_bsop_date")]
        public string StckBsopDate { get; set; } = string.Empty;

        /// <summary>주식 시가</summary>
        [JsonPropertyName("stck_oprc")]
        public string StckOprc { get; set; } = "0";

        /// <summary>주식 최고가</summary>
        [JsonPropertyName("stck_hgpr")]
        public string StckHgpr { get; set; } = "0";

        /// <summary>주식 최저가</summary>
        [JsonPropertyName("stck_lwpr")]
        public string StckLwpr { get; set; } = "0";

        /// <summary>주식 종가</summary>
        [JsonPropertyName("stck_clpr")]
        public string StckClpr { get; set; } = "0";

        /// <summary>누적 거래량</summary>
        [JsonPropertyName("acml_vol")]
        public string AcmlVol { get; set; } = "0";

        /// <summary>전일 대비 거래량 비율</summary>
        [JsonPropertyName("prdy_vrss_vol_rate")]
        public string PrdyVrssVolRate { get; set; } = "0";

        /// <summary>전일 대비</summary>
        [JsonPropertyName("prdy_vrss")]
        public string PrdyVrss { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>전일 대비율 (%)</summary>
        [JsonPropertyName("prdy_ctrt")]
        public string PrdyCtrt { get; set; } = "0";

        /// <summary>HTS 외국인 소진율 (%)</summary>
        [JsonPropertyName("hts_frgn_ehrt")]
        public string HtsFrgnEhrt { get; set; } = "0";

        /// <summary>외국인 순매수 수량</summary>
        [JsonPropertyName("frgn_ntby_qty")]
        public string FrgnNtbyQty { get; set; } = "0";

        /// <summary>
        /// 락 구분 코드
        /// 01:권리락 02:배당락 03:분배락 04:권배락
        /// 05:중간(분기)배당락 06:권리중간배당락 07:권리분기배당락
        /// </summary>
        [JsonPropertyName("flng_cls_code")]
        public string FlngClsCode { get; set; } = string.Empty;

        /// <summary>누적 분할 비율</summary>
        [JsonPropertyName("acml_prtt_rate")]
        public string AcmlPrttRate { get; set; } = "0";
    }
}
