using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Market
{
    // =====================================================================
    // ===== 국내업종 현재지수 요청 DTO =====
    // [v1_국내주식-063] GET /uapi/domestic-stock/v1/quotations/inquire-index-price
    //
    // [유의사항]
    // - 실전 계좌 전용 API ? 모의투자 미지원
    // - FID_COND_MRKT_DIV_CODE : 업종(U) 고정
    // - FID_INPUT_ISCD 주요 코드:
    //   0001 = 코스피, 1001 = 코스닥, 2001 = 코스피200
    //   (전체 업종코드는 포탈 FAQ '종목정보 다운로드(국내) - 업종코드' 참조)
    // =====================================================================

    public class InquireIndexPriceRequest
    {
        /// <summary>FID 조건 시장 분류 코드 (U : 업종 고정)</summary>
        public string FID_COND_MRKT_DIV_CODE { get; set; } = "U";

        /// <summary>
        /// FID 입력 종목코드 (업종코드)
        /// 0001=코스피, 1001=코스닥, 2001=코스피200
        /// </summary>
        public string FID_INPUT_ISCD { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 국내업종 현재지수 응답 DTO =====
    // =====================================================================

    public class InquireIndexPriceResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>업종 현재지수 상세 (단일 객체)</summary>
        [JsonPropertyName("output")]
        public InquireIndexPriceOutput? Output { get; set; }
    }

    // =====================================================================
    // ===== 국내업종 현재지수 output DTO =====
    // =====================================================================

    public class InquireIndexPriceOutput
    {
        // ===== 현재지수 / 전일 대비 =====

        /// <summary>업종 지수 현재가</summary>
        [JsonPropertyName("bstp_nmix_prpr")]
        public string BstpNmixPrpr { get; set; } = "0";

        /// <summary>업종 지수 전일 대비</summary>
        [JsonPropertyName("bstp_nmix_prdy_vrss")]
        public string BstpNmixPrdyVrss { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>업종 지수 전일 대비율 (%)</summary>
        [JsonPropertyName("bstp_nmix_prdy_ctrt")]
        public string BstpNmixPrdyCtrt { get; set; } = "0";

        // ===== 거래량 / 거래대금 =====

        /// <summary>누적 거래량</summary>
        [JsonPropertyName("acml_vol")]
        public string AcmlVol { get; set; } = "0";

        /// <summary>전일 거래량</summary>
        [JsonPropertyName("prdy_vol")]
        public string PrdyVol { get; set; } = "0";

        /// <summary>누적 거래 대금</summary>
        [JsonPropertyName("acml_tr_pbmn")]
        public string AcmlTrPbmn { get; set; } = "0";

        /// <summary>전일 거래 대금</summary>
        [JsonPropertyName("prdy_tr_pbmn")]
        public string PrdyTrPbmn { get; set; } = "0";

        // ===== 시가 =====

        /// <summary>업종 지수 시가</summary>
        [JsonPropertyName("bstp_nmix_oprc")]
        public string BstpNmixOprc { get; set; } = "0";

        /// <summary>전일 지수 대비 지수 시가</summary>
        [JsonPropertyName("prdy_nmix_vrss_nmix_oprc")]
        public string PrdyNmixVrssNmixOprc { get; set; } = "0";

        /// <summary>시가 대비 현재가 부호</summary>
        [JsonPropertyName("oprc_vrss_prpr_sign")]
        public string OprcVrssProprSign { get; set; } = string.Empty;

        /// <summary>업종 지수 시가 전일 대비율 (%)</summary>
        [JsonPropertyName("bstp_nmix_oprc_prdy_ctrt")]
        public string BstpNmixOprcPrdyCtrt { get; set; } = "0";

        // ===== 최고가 =====

        /// <summary>업종 지수 최고가</summary>
        [JsonPropertyName("bstp_nmix_hgpr")]
        public string BstpNmixHgpr { get; set; } = "0";

        /// <summary>전일 지수 대비 지수 최고가</summary>
        [JsonPropertyName("prdy_nmix_vrss_nmix_hgpr")]
        public string PrdyNmixVrssNmixHgpr { get; set; } = "0";

        /// <summary>최고가 대비 현재가 부호</summary>
        [JsonPropertyName("hgpr_vrss_prpr_sign")]
        public string HgprVrssPrprSign { get; set; } = string.Empty;

        /// <summary>업종 지수 최고가 전일 대비율 (%)</summary>
        [JsonPropertyName("bstp_nmix_hgpr_prdy_ctrt")]
        public string BstpNmixHgprPrdyCtrt { get; set; } = "0";

        // ===== 최저가 =====

        /// <summary>업종 지수 최저가</summary>
        [JsonPropertyName("bstp_nmix_lwpr")]
        public string BstpNmixLwpr { get; set; } = "0";

        /// <summary>전일 종가 대비 최저가</summary>
        [JsonPropertyName("prdy_clpr_vrss_lwpr")]
        public string PrdyClprVrssLwpr { get; set; } = "0";

        /// <summary>최저가 대비 현재가 부호</summary>
        [JsonPropertyName("lwpr_vrss_prpr_sign")]
        public string LwprVrssPrprSign { get; set; } = string.Empty;

        /// <summary>전일 종가 대비 최저가 비율 (%)</summary>
        [JsonPropertyName("prdy_clpr_vrss_lwpr_rate")]
        public string PrdyClprVrssLwprRate { get; set; } = "0";

        // ===== 등락 종목 수 =====

        /// <summary>상승 종목 수</summary>
        [JsonPropertyName("ascn_issu_cnt")]
        public string AscnIssuCnt { get; set; } = "0";

        /// <summary>상한 종목 수</summary>
        [JsonPropertyName("uplm_issu_cnt")]
        public string UplmIssuCnt { get; set; } = "0";

        /// <summary>보합 종목 수</summary>
        [JsonPropertyName("stnr_issu_cnt")]
        public string StnrIssuCnt { get; set; } = "0";

        /// <summary>하락 종목 수</summary>
        [JsonPropertyName("down_issu_cnt")]
        public string DownIssuCnt { get; set; } = "0";

        /// <summary>하한 종목 수</summary>
        [JsonPropertyName("lslm_issu_cnt")]
        public string LslmIssuCnt { get; set; } = "0";

        // ===== 연중 고저 =====

        /// <summary>연중 업종지수 최고가</summary>
        [JsonPropertyName("dryy_bstp_nmix_hgpr")]
        public string DryyBstpNmixHgpr { get; set; } = "0";

        /// <summary>연중 최고가 대비 현재가 비율 (%)</summary>
        [JsonPropertyName("dryy_hgpr_vrss_prpr_rate")]
        public string DryyHgprVrssPrprRate { get; set; } = "0";

        /// <summary>연중 업종지수 최고가 일자 (YYYYMMDD)</summary>
        [JsonPropertyName("dryy_bstp_nmix_hgpr_date")]
        public string DryyBstpNmixHgprDate { get; set; } = string.Empty;

        /// <summary>연중 업종지수 최저가</summary>
        [JsonPropertyName("dryy_bstp_nmix_lwpr")]
        public string DryyBstpNmixLwpr { get; set; } = "0";

        /// <summary>연중 최저가 대비 현재가 비율 (%)</summary>
        [JsonPropertyName("dryy_lwpr_vrss_prpr_rate")]
        public string DryyLwprVrssPrprRate { get; set; } = "0";

        /// <summary>연중 업종지수 최저가 일자 (YYYYMMDD)</summary>
        [JsonPropertyName("dryy_bstp_nmix_lwpr_date")]
        public string DryyBstpNmixLwprDate { get; set; } = string.Empty;

        // ===== 호가 잔량 =====

        /// <summary>총 매도호가 잔량</summary>
        [JsonPropertyName("total_askp_rsqn")]
        public string TotalAskpRsqn { get; set; } = "0";

        /// <summary>총 매수호가 잔량</summary>
        [JsonPropertyName("total_bidp_rsqn")]
        public string TotalBidpRsqn { get; set; } = "0";

        /// <summary>매도 잔량 비율 (%)</summary>
        [JsonPropertyName("seln_rsqn_rate")]
        public string SelnRsqnRate { get; set; } = "0";

        /// <summary>매수 잔량 비율 (%)</summary>
        [JsonPropertyName("shnu_rsqn_rate")]
        public string ShnuRsqnRate { get; set; } = "0";

        /// <summary>순매수 잔량</summary>
        [JsonPropertyName("ntby_rsqn")]
        public string NtbyRsqn { get; set; } = "0";
    }
}
