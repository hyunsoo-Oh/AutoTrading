using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Market
{
    // =====================================================================
    // ===== 주식현재가 투자자 요청 DTO =====
    // [v1_국내주식-012] GET /uapi/domestic-stock/v1/quotations/inquire-investor
    // 개인/외국인/기관의 순매수 수량·거래대금을 일자별로 조회한다.
    // =====================================================================

    public class InquireInvestorRequest
    {
        /// <summary>조건 시장 분류 코드 (J:KRX, NX:NXT, UN:통합)</summary>
        public string FID_COND_MRKT_DIV_CODE { get; set; } = "J";

        /// <summary>입력 종목코드 (ex 005930 삼성전자)</summary>
        public string FID_INPUT_ISCD { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== 주식현재가 투자자 응답 DTO =====
    // =====================================================================

    public class InquireInvestorResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>일자별 투자자 동향 배열</summary>
        [JsonPropertyName("output")]
        public List<InquireInvestorItem> Output { get; set; } = new();
    }

    // =====================================================================
    // ===== 주식현재가 투자자 output 항목 DTO =====
    // 개인(prsn) / 외국인(frgn) / 기관계(orgn) 3주체의
    // 순매수 수량·거래대금, 매수·매도 거래량·거래대금을 담는다.
    //
    // [유의사항]
    // - 외국인 = 외국인투자등록 + 기타 외국인
    // - 당일 데이터는 장 종료 후 제공
    // =====================================================================

    public class InquireInvestorItem
    {
        /// <summary>주식 영업 일자 (YYYYMMDD)</summary>
        [JsonPropertyName("stck_bsop_date")]
        public string StckBsopDate { get; set; } = string.Empty;

        /// <summary>주식 종가</summary>
        [JsonPropertyName("stck_clpr")]
        public string StckClpr { get; set; } = "0";

        /// <summary>전일 대비</summary>
        [JsonPropertyName("prdy_vrss")]
        public string PrdyVrss { get; set; } = "0";

        /// <summary>전일 대비 부호 (1:상한 2:상승 3:보합 4:하한 5:하락)</summary>
        [JsonPropertyName("prdy_vrss_sign")]
        public string PrdyVrssSign { get; set; } = string.Empty;

        // ===== 순매수 수량 =====

        /// <summary>개인 순매수 수량</summary>
        [JsonPropertyName("prsn_ntby_qty")]
        public string PrsnNtbyQty { get; set; } = "0";

        /// <summary>외국인 순매수 수량</summary>
        [JsonPropertyName("frgn_ntby_qty")]
        public string FrgnNtbyQty { get; set; } = "0";

        /// <summary>기관계 순매수 수량</summary>
        [JsonPropertyName("orgn_ntby_qty")]
        public string OrgnNtbyQty { get; set; } = "0";

        // ===== 순매수 거래대금 =====

        /// <summary>개인 순매수 거래 대금</summary>
        [JsonPropertyName("prsn_ntby_tr_pbmn")]
        public string PrsnNtbyTrPbmn { get; set; } = "0";

        /// <summary>외국인 순매수 거래 대금</summary>
        [JsonPropertyName("frgn_ntby_tr_pbmn")]
        public string FrgnNtbyTrPbmn { get; set; } = "0";

        /// <summary>기관계 순매수 거래 대금</summary>
        [JsonPropertyName("orgn_ntby_tr_pbmn")]
        public string OrgnNtbyTrPbmn { get; set; } = "0";

        // ===== 매수 거래량 =====

        /// <summary>개인 매수 거래량</summary>
        [JsonPropertyName("prsn_shnu_vol")]
        public string PrsnShnuVol { get; set; } = "0";

        /// <summary>외국인 매수 거래량</summary>
        [JsonPropertyName("frgn_shnu_vol")]
        public string FrgnShnuVol { get; set; } = "0";

        /// <summary>기관계 매수 거래량</summary>
        [JsonPropertyName("orgn_shnu_vol")]
        public string OrgnShnuVol { get; set; } = "0";

        // ===== 매수 거래대금 =====

        /// <summary>개인 매수 거래 대금</summary>
        [JsonPropertyName("prsn_shnu_tr_pbmn")]
        public string PrsnShnuTrPbmn { get; set; } = "0";

        /// <summary>외국인 매수 거래 대금</summary>
        [JsonPropertyName("frgn_shnu_tr_pbmn")]
        public string FrgnShnuTrPbmn { get; set; } = "0";

        /// <summary>기관계 매수 거래 대금</summary>
        [JsonPropertyName("orgn_shnu_tr_pbmn")]
        public string OrgnShnuTrPbmn { get; set; } = "0";

        // ===== 매도 거래량 =====

        /// <summary>개인 매도 거래량</summary>
        [JsonPropertyName("prsn_seln_vol")]
        public string PrsnSelnVol { get; set; } = "0";

        /// <summary>외국인 매도 거래량</summary>
        [JsonPropertyName("frgn_seln_vol")]
        public string FrgnSelnVol { get; set; } = "0";

        /// <summary>기관계 매도 거래량</summary>
        [JsonPropertyName("orgn_seln_vol")]
        public string OrgnSelnVol { get; set; } = "0";

        // ===== 매도 거래대금 =====

        /// <summary>개인 매도 거래 대금</summary>
        [JsonPropertyName("prsn_seln_tr_pbmn")]
        public string PrsnSelnTrPbmn { get; set; } = "0";

        /// <summary>외국인 매도 거래 대금</summary>
        [JsonPropertyName("frgn_seln_tr_pbmn")]
        public string FrgnSelnTrPbmn { get; set; } = "0";

        /// <summary>기관계 매도 거래 대금</summary>
        [JsonPropertyName("orgn_seln_tr_pbmn")]
        public string OrgnSelnTrPbmn { get; set; } = "0";
    }
}
