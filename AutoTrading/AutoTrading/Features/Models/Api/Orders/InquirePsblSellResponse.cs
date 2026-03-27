using System.Text.Json.Serialization;

namespace AutoTrading.Features.Models.Api.Orders
{
    /// <summary>
    /// 매도가능수량조회 응답 DTO
    ///
    /// ※ 응답 본문의 키가 "output1"임에 주의
    ///    (매수가능조회의 "output"과 다르다)
    /// </summary>
    public sealed class InquirePsblSellResponse
    {
        /// <summary>성공 실패 여부 ("0": 성공)</summary>
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        /// <summary>응답코드</summary>
        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        /// <summary>응답메시지</summary>
        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>
        /// 응답상세 (단일 객체)
        /// ※ 키 이름이 "output1"
        /// </summary>
        [JsonPropertyName("output1")]
        public InquirePsblSellOutput1? Output1 { get; set; }
    }

    /// <summary>
    /// 매도가능수량조회 응답 상세
    ///
    /// 핵심 필드:
    /// - OrdPsblQty(주문가능수량): 실제 매도 주문에 사용할 수 있는 수량
    /// - CblcQty(잔고수량): 현재 보유 수량 (체결되지 않은 매도 주문 포함)
    /// </summary>
    public sealed class InquirePsblSellOutput1
    {
        /// <summary>상품번호 (종목코드)</summary>
        [JsonPropertyName("pdno")]
        public string Pdno { get; set; } = string.Empty;

        /// <summary>상품명 (종목명)</summary>
        [JsonPropertyName("prdt_name")]
        public string PrdtName { get; set; } = string.Empty;

        /// <summary>매수수량 — 금일 매수한 수량</summary>
        [JsonPropertyName("buy_qty")]
        public string BuyQty { get; set; } = string.Empty;

        /// <summary>매도수량 — 금일 매도한 수량</summary>
        [JsonPropertyName("sll_qty")]
        public string SllQty { get; set; } = string.Empty;

        /// <summary>잔고수량 — 현재 보유 수량 (미체결 매도 포함)</summary>
        [JsonPropertyName("cblc_qty")]
        public string CblcQty { get; set; } = string.Empty;

        /// <summary>비저축수량</summary>
        [JsonPropertyName("nsvg_qty")]
        public string NsvgQty { get; set; } = string.Empty;

        /// <summary>
        /// 주문가능수량 — 매도 주문에 사용할 실제 가능 수량
        /// 이 값이 0이면 매도 주문을 낼 수 없다.
        /// </summary>
        [JsonPropertyName("ord_psbl_qty")]
        public string OrdPsblQty { get; set; } = string.Empty;

        /// <summary>매입평균가격</summary>
        [JsonPropertyName("pchs_avg_pric")]
        public string PchsAvgPric { get; set; } = string.Empty;

        /// <summary>매입금액</summary>
        [JsonPropertyName("pchs_amt")]
        public string PchsAmt { get; set; } = string.Empty;

        /// <summary>현재가</summary>
        [JsonPropertyName("now_pric")]
        public string NowPric { get; set; } = string.Empty;

        /// <summary>평가금액</summary>
        [JsonPropertyName("evlu_amt")]
        public string EvluAmt { get; set; } = string.Empty;

        /// <summary>평가손익금액</summary>
        [JsonPropertyName("evlu_pfls_amt")]
        public string EvluPflsAmt { get; set; } = string.Empty;

        /// <summary>평가손익율</summary>
        [JsonPropertyName("evlu_pfls_rt")]
        public string EvluPflsRt { get; set; } = string.Empty;
    }
}
