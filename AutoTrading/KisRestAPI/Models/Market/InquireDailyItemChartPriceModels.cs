using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Market
{
    // =====================================================================
    // ===== 국내주식기간별시세 요청 DTO =====
    // [v1_국내주식-016] GET /uapi/domestic-stock/v1/quotations/inquire-daily-itemchartprice
    // 일/주/월/년봉 캔들 데이터를 최대 100건 조회한다.
    // =====================================================================

    public class InquireDailyItemChartPriceRequest
    {
        /// <summary>조건 시장 분류 코드 (J:KRX, NX:NXT, UN:통합)</summary>
        public string FID_COND_MRKT_DIV_CODE { get; set; } = "J";

        /// <summary>입력 종목코드 (ex 005930 삼성전자)</summary>
        public string FID_INPUT_ISCD { get; set; } = string.Empty;

        /// <summary>조회 시작일자 (YYYYMMDD)</summary>
        public string FID_INPUT_DATE_1 { get; set; } = string.Empty;

        /// <summary>조회 종료일자 (YYYYMMDD) ? 한 번의 호출에 최대 100건</summary>
        public string FID_INPUT_DATE_2 { get; set; } = string.Empty;

        /// <summary>
        /// 기간 분류 코드
        /// D:일봉  W:주봉  M:월봉  Y:년봉
        /// </summary>
        public string FID_PERIOD_DIV_CODE { get; set; } = "D";

        /// <summary>
        /// 수정주가 원주가 가격 여부
        /// 0 : 수정주가  1 : 원주가
        /// </summary>
        public string FID_ORG_ADJ_PRC { get; set; } = "0";
    }

    // =====================================================================
    // ===== 국내주식기간별시세 응답 DTO =====
    // =====================================================================

    public class InquireDailyItemChartPriceResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>종목 현황 요약 (단일 객체)</summary>
        [JsonPropertyName("output1")]
        public InquireDailyItemChartPriceOutput1? Output1 { get; set; }

        /// <summary>기간별 캔들 배열 (최대 100건)</summary>
        [JsonPropertyName("output2")]
        public List<InquireDailyItemChartPriceItem> Output2 { get; set; } = new();
    }

    // =====================================================================
    // ===== output1 ? 종목 현황 요약 DTO =====
    // =====================================================================

    public class InquireDailyItemChartPriceOutput1
    {
        /// <summary>전일 대비</summary>
        [JsonPropertyName("prdy_vrss")]
        public string PrdyVrss { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>전일 대비율 (%)</summary>
        [JsonPropertyName("prdy_ctrt")]
        public string PrdyCtrt { get; set; } = "0";

        /// <summary>주식 전일 종가</summary>
        [JsonPropertyName("stck_prdy_clpr")]
        public string StckPrdyClpr { get; set; } = "0";

        /// <summary>누적 거래량</summary>
        [JsonPropertyName("acml_vol")]
        public string AcmlVol { get; set; } = "0";

        /// <summary>누적 거래 대금</summary>
        [JsonPropertyName("acml_tr_pbmn")]
        public string AcmlTrPbmn { get; set; } = "0";

        /// <summary>HTS 한글 종목명</summary>
        [JsonPropertyName("hts_kor_isnm")]
        public string HtsKorIsnm { get; set; } = string.Empty;

        /// <summary>주식 현재가</summary>
        [JsonPropertyName("stck_prpr")]
        public string StckPrpr { get; set; } = "0";

        /// <summary>주식 단축 종목코드</summary>
        [JsonPropertyName("stck_shrn_iscd")]
        public string StckShrnIscd { get; set; } = string.Empty;

        /// <summary>전일 거래량</summary>
        [JsonPropertyName("prdy_vol")]
        public string PrdyVol { get; set; } = "0";

        /// <summary>주식 상한가</summary>
        [JsonPropertyName("stck_mxpr")]
        public string StckMxpr { get; set; } = "0";

        /// <summary>주식 하한가</summary>
        [JsonPropertyName("stck_llam")]
        public string StckLlam { get; set; } = "0";

        /// <summary>주식 시가</summary>
        [JsonPropertyName("stck_oprc")]
        public string StckOprc { get; set; } = "0";

        /// <summary>주식 최고가</summary>
        [JsonPropertyName("stck_hgpr")]
        public string StckHgpr { get; set; } = "0";

        /// <summary>주식 최저가</summary>
        [JsonPropertyName("stck_lwpr")]
        public string StckLwpr { get; set; } = "0";

        /// <summary>주식 전일 시가</summary>
        [JsonPropertyName("stck_prdy_oprc")]
        public string StckPrdyOprc { get; set; } = "0";

        /// <summary>주식 전일 최고가</summary>
        [JsonPropertyName("stck_prdy_hgpr")]
        public string StckPrdyHgpr { get; set; } = "0";

        /// <summary>주식 전일 최저가</summary>
        [JsonPropertyName("stck_prdy_lwpr")]
        public string StckPrdyLwpr { get; set; } = "0";

        /// <summary>매도호가</summary>
        [JsonPropertyName("askp")]
        public string Askp { get; set; } = "0";

        /// <summary>매수호가</summary>
        [JsonPropertyName("bidp")]
        public string Bidp { get; set; } = "0";

        /// <summary>전일 대비 거래량</summary>
        [JsonPropertyName("prdy_vrss_vol")]
        public string PrdyVrssVol { get; set; } = "0";

        /// <summary>거래량 회전율</summary>
        [JsonPropertyName("vol_tnrt")]
        public string VolTnrt { get; set; } = "0";

        /// <summary>주식 액면가</summary>
        [JsonPropertyName("stck_fcam")]
        public string StckFcam { get; set; } = "0";

        /// <summary>상장 주수</summary>
        [JsonPropertyName("lstn_stcn")]
        public string LstnStcn { get; set; } = "0";

        /// <summary>자본금</summary>
        [JsonPropertyName("cpfn")]
        public string Cpfn { get; set; } = "0";

        /// <summary>HTS 시가총액</summary>
        [JsonPropertyName("hts_avls")]
        public string HtsAvls { get; set; } = "0";

        /// <summary>PER</summary>
        [JsonPropertyName("per")]
        public string Per { get; set; } = "0";

        /// <summary>EPS</summary>
        [JsonPropertyName("eps")]
        public string Eps { get; set; } = "0";

        /// <summary>PBR</summary>
        [JsonPropertyName("pbr")]
        public string Pbr { get; set; } = "0";

        /// <summary>전체 융자 잔고 비율</summary>
        [JsonPropertyName("itewhol_loan_rmnd_ratem")]
        public string ItewholLoanRmndRatem { get; set; } = "0";
    }

    // =====================================================================
    // ===== output2 ? 기간별 캔들 항목 DTO =====
    // =====================================================================

    public class InquireDailyItemChartPriceItem
    {
        /// <summary>주식 영업 일자 (YYYYMMDD)</summary>
        [JsonPropertyName("stck_bsop_date")]
        public string StckBsopDate { get; set; } = string.Empty;

        /// <summary>주식 종가</summary>
        [JsonPropertyName("stck_clpr")]
        public string StckClpr { get; set; } = "0";

        /// <summary>주식 시가</summary>
        [JsonPropertyName("stck_oprc")]
        public string StckOprc { get; set; } = "0";

        /// <summary>주식 최고가</summary>
        [JsonPropertyName("stck_hgpr")]
        public string StckHgpr { get; set; } = "0";

        /// <summary>주식 최저가</summary>
        [JsonPropertyName("stck_lwpr")]
        public string StckLwpr { get; set; } = "0";

        /// <summary>누적 거래량</summary>
        [JsonPropertyName("acml_vol")]
        public string AcmlVol { get; set; } = "0";

        /// <summary>누적 거래 대금</summary>
        [JsonPropertyName("acml_tr_pbmn")]
        public string AcmlTrPbmn { get; set; } = "0";

        /// <summary>
        /// 락 구분 코드
        /// 01:권리락 02:배당락 03:분배락 04:권배락
        /// 05:중간(분기)배당락 06:권리중간배당락 07:권리분기배당락
        /// </summary>
        [JsonPropertyName("flng_cls_code")]
        public string FlngClsCode { get; set; } = string.Empty;

        /// <summary>분할 비율 (기준가/전일 종가)</summary>
        [JsonPropertyName("prtt_rate")]
        public string PrttRate { get; set; } = "0";

        /// <summary>변경 여부 ? 당일 체결 미발생으로 시가가 없는 경우 Y (차트 표시용)</summary>
        [JsonPropertyName("mod_yn")]
        public string ModYn { get; set; } = string.Empty;

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>전일 대비</summary>
        [JsonPropertyName("prdy_vrss")]
        public string PrdyVrss { get; set; } = "0";

        /// <summary>
        /// 재평가사유코드
        /// 00:해당없음 01:회사분할 02:자본감소 03:장기간정지 04:초과분배
        /// 05:대규모배당 06:회사분할합병 07:ETN증권병합/분할 08:신종증권기세조정 99:기타
        /// </summary>
        [JsonPropertyName("revl_issu_reas")]
        public string RevlIssuReas { get; set; } = "00";
    }
}
