using System.Text.Json.Serialization;

namespace AutoTrading.Features.Models.Api.Orders
{
    /// <summary>
    /// 주식정정취소가능주문조회 응답 DTO
    /// </summary>
    public sealed class InquirePsblRvsecnclResponse
    {
        /// <summary>성공 실패 여부 ("0": 성공)</summary>
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        /// <summary>응답코드</summary>
        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        /// <summary>응답메세지</summary>
        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>
        /// 정정/취소 가능 주문 목록
        /// 한 번에 최대 50건, 이후는 연속조회로 확인
        /// </summary>
        [JsonPropertyName("output")]
        public List<InquirePsblRvsecnclItem>? Output { get; set; }
    }

    /// <summary>
    /// 정정/취소 가능 주문 1건
    ///
    /// 왜 이 클래스가 중요한가?
    /// - PsblQty(가능수량)이 실제 정정/취소 가능한 수량이다.
    ///   이 수량을 초과해서 정정/취소 주문을 넣으면 거부된다.
    /// - Odno(주문번호)가 정정취소 주문의 ORGN_ODNO로 사용된다.
    /// - OrdGnoBrno(주문채번지점번호)가 KRX_FWDG_ORD_ORGNO로 사용된다.
    /// </summary>
    public sealed class InquirePsblRvsecnclItem
    {
        /// <summary>주문채번지점번호 — 정정취소 시 KRX_FWDG_ORD_ORGNO로 사용</summary>
        [JsonPropertyName("ord_gno_brno")]
        public string OrdGnoBrno { get; set; } = string.Empty;

        /// <summary>주문번호 — 정정취소 시 ORGN_ODNO로 사용</summary>
        [JsonPropertyName("odno")]
        public string Odno { get; set; } = string.Empty;

        /// <summary>원주문번호 (정정/취소 주문인 경우)</summary>
        [JsonPropertyName("orgn_odno")]
        public string OrgnOdno { get; set; } = string.Empty;

        /// <summary>주문구분명</summary>
        [JsonPropertyName("ord_dvsn_name")]
        public string OrdDvsnName { get; set; } = string.Empty;

        /// <summary>종목번호 (뒤 6자리)</summary>
        [JsonPropertyName("pdno")]
        public string Pdno { get; set; } = string.Empty;

        /// <summary>종목명</summary>
        [JsonPropertyName("prdt_name")]
        public string PrdtName { get; set; } = string.Empty;

        /// <summary>정정취소구분명 ("정정" 또는 "취소")</summary>
        [JsonPropertyName("rvse_cncl_dvsn_name")]
        public string RvseCnclDvsnName { get; set; } = string.Empty;

        /// <summary>주문수량</summary>
        [JsonPropertyName("ord_qty")]
        public string OrdQty { get; set; } = string.Empty;

        /// <summary>주문단가</summary>
        [JsonPropertyName("ord_unpr")]
        public string OrdUnpr { get; set; } = string.Empty;

        /// <summary>주문시각 (HHMMSS)</summary>
        [JsonPropertyName("ord_tmd")]
        public string OrdTmd { get; set; } = string.Empty;

        /// <summary>총체결수량</summary>
        [JsonPropertyName("tot_ccld_qty")]
        public string TotCcldQty { get; set; } = string.Empty;

        /// <summary>총체결금액</summary>
        [JsonPropertyName("tot_ccld_amt")]
        public string TotCcldAmt { get; set; } = string.Empty;

        /// <summary>
        /// 정정/취소 가능수량
        /// 정정취소 주문 시 이 수량을 초과할 수 없다.
        /// </summary>
        [JsonPropertyName("psbl_qty")]
        public string PsblQty { get; set; } = string.Empty;

        /// <summary>매도매수구분코드 ("01": 매도, "02": 매수)</summary>
        [JsonPropertyName("sll_buy_dvsn_cd")]
        public string SllBuyDvsnCd { get; set; } = string.Empty;

        /// <summary>주문구분코드 ("00": 지정가, "01": 시장가 등)</summary>
        [JsonPropertyName("ord_dvsn_cd")]
        public string OrdDvsnCd { get; set; } = string.Empty;

        /// <summary>운용사지정주문번호</summary>
        [JsonPropertyName("mgco_aptm_odno")]
        public string MgcoAptmOdno { get; set; } = string.Empty;

        /// <summary>거래소구분코드</summary>
        [JsonPropertyName("excg_dvsn_cd")]
        public string ExcgDvsnCd { get; set; } = string.Empty;

        /// <summary>거래소ID구분코드 (KRX, NXT, SOR)</summary>
        [JsonPropertyName("excg_id_dvsn_cd")]
        public string ExcgIdDvsnCd { get; set; } = string.Empty;

        /// <summary>거래소ID구분명</summary>
        [JsonPropertyName("excg_id_dvsn_name")]
        public string ExcgIdDvsnName { get; set; } = string.Empty;

        /// <summary>스톱지정가 조건가격</summary>
        [JsonPropertyName("stpm_cndt_pric")]
        public string StpmCndtPric { get; set; } = string.Empty;

        /// <summary>스톱지정가 효력발생여부</summary>
        [JsonPropertyName("stpm_efct_occr_yn")]
        public string StpmEfctOccrYn { get; set; } = string.Empty;
    }
}
