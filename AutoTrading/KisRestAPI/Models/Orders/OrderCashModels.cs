using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Orders
{
    // =====================================================================
    // ===== 주식 현금 주문 요청 DTO =====
    // POST 방식 — Body에 JSON으로 전송
    // =====================================================================

    public sealed class OrderCashRequest
    {
        [JsonPropertyName("CANO")]
        public string Cano { get; set; } = string.Empty;

        [JsonPropertyName("ACNT_PRDT_CD")]
        public string AcntPrdtCd { get; set; } = "01";

        [JsonPropertyName("PDNO")]
        public string PdNo { get; set; } = string.Empty;

        [JsonPropertyName("SLL_TYPE")]
        public string SllType { get; set; } = "01";

        [JsonPropertyName("ORD_DVSN")]
        public string OrdDvsn { get; set; } = "00";

        [JsonPropertyName("ORD_QTY")]
        public string OrdQty { get; set; } = string.Empty;

        [JsonPropertyName("ORD_UNPR")]
        public string OrdUnpr { get; set; } = string.Empty;

        [JsonPropertyName("CNDT_PRIC")]
        public string CndtPric { get; set; } = "0";

        [JsonPropertyName("EXCG_ID_DVSN_CD")]
        public string ExcgIdDvsnCd { get; set; } = "KRX";
    }

    // =====================================================================
    // ===== 주식 현금 주문 응답 DTO =====
    // 주문 정정/취소(OrderRvsecncl)도 동일한 응답 구조를 사용한다.
    // =====================================================================

    public sealed class OrderCashResponse
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        [JsonPropertyName("output")]
        public OrderOutput? Output { get; set; }

        public class OrderOutput
        {
            [JsonPropertyName("KRX_FWDG_ORD_ORGNO")]
            public string KrxFwdgOrdOrgno { get; set; } = string.Empty;

            [JsonPropertyName("ODNO")]
            public string Odno { get; set; } = string.Empty;

            [JsonPropertyName("ORD_TMD")]
            public string OrdTmd { get; set; } = string.Empty;
        }
    }
}
