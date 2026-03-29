using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Market
{
    // =====================================================================
    // ===== 주식현재가 호가/예상체결 요청 DTO =====
    // [v1_국내주식-011] GET /uapi/domestic-stock/v1/quotations/inquire-asking-price-exp-ccn
    // 매수/매도 호가 10단계와 예상체결가를 조회한다.
    // =====================================================================

    public class InquireAskingPriceRequest
    {
        /// <summary>조건 시장 분류 코드 (J:KRX, NX:NXT, UN:통합)</summary>
        public string FID_COND_MRKT_DIV_CODE { get; set; } = "J";

        /// <summary>입력 종목코드 (ex 005930 삼성전자)</summary>
        public string FID_INPUT_ISCD { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 주식현재가 호가/예상체결 응답 DTO =====
    // =====================================================================

    public class InquireAskingPriceResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>호가 10단계 (매도/매수 가격·잔량·증감)</summary>
        [JsonPropertyName("output1")]
        public InquireAskingPriceOutput1? Output1 { get; set; }

        /// <summary>예상체결 정보</summary>
        [JsonPropertyName("output2")]
        public InquireAskingPriceOutput2? Output2 { get; set; }
    }

    // =====================================================================
    // ===== output1 ? 호가 10단계 DTO =====
    // 매도호가(askp) : 1 = 최우선 매도호가 (가장 낮은 매도가)
    // 매수호가(bidp) : 1 = 최우선 매수호가 (가장 높은 매수가)
    // =====================================================================

    public class InquireAskingPriceOutput1
    {
        /// <summary>호가 접수 시간 (HHmmss)</summary>
        [JsonPropertyName("aspr_acpt_hour")]
        public string AsprAcptHour { get; set; } = string.Empty;

        // ===== 매도호가 (1=최우선, 10=최후순위) =====
        [JsonPropertyName("askp1")]  public string AskP1  { get; set; } = "0";
        [JsonPropertyName("askp2")]  public string AskP2  { get; set; } = "0";
        [JsonPropertyName("askp3")]  public string AskP3  { get; set; } = "0";
        [JsonPropertyName("askp4")]  public string AskP4  { get; set; } = "0";
        [JsonPropertyName("askp5")]  public string AskP5  { get; set; } = "0";
        [JsonPropertyName("askp6")]  public string AskP6  { get; set; } = "0";
        [JsonPropertyName("askp7")]  public string AskP7  { get; set; } = "0";
        [JsonPropertyName("askp8")]  public string AskP8  { get; set; } = "0";
        [JsonPropertyName("askp9")]  public string AskP9  { get; set; } = "0";
        [JsonPropertyName("askp10")] public string AskP10 { get; set; } = "0";

        // ===== 매수호가 (1=최우선, 10=최후순위) =====
        [JsonPropertyName("bidp1")]  public string BidP1  { get; set; } = "0";
        [JsonPropertyName("bidp2")]  public string BidP2  { get; set; } = "0";
        [JsonPropertyName("bidp3")]  public string BidP3  { get; set; } = "0";
        [JsonPropertyName("bidp4")]  public string BidP4  { get; set; } = "0";
        [JsonPropertyName("bidp5")]  public string BidP5  { get; set; } = "0";
        [JsonPropertyName("bidp6")]  public string BidP6  { get; set; } = "0";
        [JsonPropertyName("bidp7")]  public string BidP7  { get; set; } = "0";
        [JsonPropertyName("bidp8")]  public string BidP8  { get; set; } = "0";
        [JsonPropertyName("bidp9")]  public string BidP9  { get; set; } = "0";
        [JsonPropertyName("bidp10")] public string BidP10 { get; set; } = "0";

        // ===== 매도호가 잔량 =====
        [JsonPropertyName("askp_rsqn1")]  public string AskPRsqn1  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn2")]  public string AskPRsqn2  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn3")]  public string AskPRsqn3  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn4")]  public string AskPRsqn4  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn5")]  public string AskPRsqn5  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn6")]  public string AskPRsqn6  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn7")]  public string AskPRsqn7  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn8")]  public string AskPRsqn8  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn9")]  public string AskPRsqn9  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn10")] public string AskPRsqn10 { get; set; } = "0";

        // ===== 매수호가 잔량 =====
        [JsonPropertyName("bidp_rsqn1")]  public string BidPRsqn1  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn2")]  public string BidPRsqn2  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn3")]  public string BidPRsqn3  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn4")]  public string BidPRsqn4  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn5")]  public string BidPRsqn5  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn6")]  public string BidPRsqn6  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn7")]  public string BidPRsqn7  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn8")]  public string BidPRsqn8  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn9")]  public string BidPRsqn9  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn10")] public string BidPRsqn10 { get; set; } = "0";

        // ===== 매도호가 잔량 증감 =====
        [JsonPropertyName("askp_rsqn_icdc1")]  public string AskPRsqnIcdc1  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn_icdc2")]  public string AskPRsqnIcdc2  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn_icdc3")]  public string AskPRsqnIcdc3  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn_icdc4")]  public string AskPRsqnIcdc4  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn_icdc5")]  public string AskPRsqnIcdc5  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn_icdc6")]  public string AskPRsqnIcdc6  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn_icdc7")]  public string AskPRsqnIcdc7  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn_icdc8")]  public string AskPRsqnIcdc8  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn_icdc9")]  public string AskPRsqnIcdc9  { get; set; } = "0";
        [JsonPropertyName("askp_rsqn_icdc10")] public string AskPRsqnIcdc10 { get; set; } = "0";

        // ===== 매수호가 잔량 증감 =====
        [JsonPropertyName("bidp_rsqn_icdc1")]  public string BidPRsqnIcdc1  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn_icdc2")]  public string BidPRsqnIcdc2  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn_icdc3")]  public string BidPRsqnIcdc3  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn_icdc4")]  public string BidPRsqnIcdc4  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn_icdc5")]  public string BidPRsqnIcdc5  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn_icdc6")]  public string BidPRsqnIcdc6  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn_icdc7")]  public string BidPRsqnIcdc7  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn_icdc8")]  public string BidPRsqnIcdc8  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn_icdc9")]  public string BidPRsqnIcdc9  { get; set; } = "0";
        [JsonPropertyName("bidp_rsqn_icdc10")] public string BidPRsqnIcdc10 { get; set; } = "0";

        // ===== 총 잔량 / 총 증감 =====
        /// <summary>총 매도호가 잔량</summary>
        [JsonPropertyName("total_askp_rsqn")]
        public string TotalAskPRsqn { get; set; } = "0";

        /// <summary>총 매수호가 잔량</summary>
        [JsonPropertyName("total_bidp_rsqn")]
        public string TotalBidPRsqn { get; set; } = "0";

        /// <summary>총 매도호가 잔량 증감</summary>
        [JsonPropertyName("total_askp_rsqn_icdc")]
        public string TotalAskPRsqnIcdc { get; set; } = "0";

        /// <summary>총 매수호가 잔량 증감</summary>
        [JsonPropertyName("total_bidp_rsqn_icdc")]
        public string TotalBidPRsqnIcdc { get; set; } = "0";

        // ===== 시간외 =====
        /// <summary>시간외 총 매도호가 증감</summary>
        [JsonPropertyName("ovtm_total_askp_icdc")]
        public string OvtmTotalAskPIcdc { get; set; } = "0";

        /// <summary>시간외 총 매수호가 증감</summary>
        [JsonPropertyName("ovtm_total_bidp_icdc")]
        public string OvtmTotalBidPIcdc { get; set; } = "0";

        /// <summary>시간외 총 매도호가 잔량</summary>
        [JsonPropertyName("ovtm_total_askp_rsqn")]
        public string OvtmTotalAskPRsqn { get; set; } = "0";

        /// <summary>시간외 총 매수호가 잔량</summary>
        [JsonPropertyName("ovtm_total_bidp_rsqn")]
        public string OvtmTotalBidPRsqn { get; set; } = "0";

        /// <summary>순매수 호가 잔량</summary>
        [JsonPropertyName("ntby_aspr_rsqn")]
        public string NtbyAsprRsqn { get; set; } = "0";

        /// <summary>
        /// 신 장운영 구분 코드
        /// 첫 번째 비트: 1=장개시전 2=장중 3=장종료후 4=시간외단일가 7=일반Buy-in 8=당일Buy-in
        /// 두 번째 비트: 0=보통 1=종가 2=대량 3=바스켓 7=정리매매 8=Buy-in
        /// </summary>
        [JsonPropertyName("new_mkop_cls_code")]
        public string NewMkopClsCode { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== output2 ? 예상체결 정보 DTO =====
    // =====================================================================

    public class InquireAskingPriceOutput2
    {
        /// <summary>예상 장운영 구분 코드</summary>
        [JsonPropertyName("antc_mkop_cls_code")]
        public string AntcMkopClsCode { get; set; } = string.Empty;

        /// <summary>주식 현재가</summary>
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

        /// <summary>주식 기준가</summary>
        [JsonPropertyName("stck_sdpr")]
        public string StckSdpr { get; set; } = "0";

        /// <summary>예상 체결가</summary>
        [JsonPropertyName("antc_cnpr")]
        public string AntcCnpr { get; set; } = "0";

        /// <summary>예상 체결 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("antc_cntg_vrss_sign")]
        public string AntcCntgVrssSign { get; set; } = string.Empty;

        /// <summary>예상 체결 대비</summary>
        [JsonPropertyName("antc_cntg_vrss")]
        public string AntcCntgVrss { get; set; } = "0";

        /// <summary>예상 체결 전일 대비율 (%)</summary>
        [JsonPropertyName("antc_cntg_prdy_ctrt")]
        public string AntcCntgPrdyCtrt { get; set; } = "0";

        /// <summary>예상 거래량</summary>
        [JsonPropertyName("antc_vol")]
        public string AntcVol { get; set; } = "0";

        /// <summary>주식 단축 종목코드</summary>
        [JsonPropertyName("stck_shrn_iscd")]
        public string StckShrnIscd { get; set; } = string.Empty;

        /// <summary>VI 적용 구분 코드</summary>
        [JsonPropertyName("vi_cls_code")]
        public string ViClsCode { get; set; } = string.Empty;
    }
}
