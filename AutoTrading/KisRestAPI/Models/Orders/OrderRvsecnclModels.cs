using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Orders
{
    // =====================================================================
    // ===== 주식 주문 정정/취소 요청 DTO =====
    // POST 방식 — Body에 JSON으로 전송
    // 응답은 OrderCashResponse를 재사용한다.
    // =====================================================================

    public sealed class OrderRvsecnclRequest
    {
        [JsonPropertyName("CANO")]
        public string Cano { get; set; } = string.Empty;

        [JsonPropertyName("ACNT_PRDT_CD")]
        public string AcntPrdtCd { get; set; } = "01";

        [JsonPropertyName("KRX_FWDG_ORD_ORGNO")]
        public string KrxFwdgOrdOrgno { get; set; } = string.Empty;

        [JsonPropertyName("ORGN_ODNO")]
        public string OrgnOdno { get; set; } = string.Empty;

        [JsonPropertyName("ORD_DVSN")]
        public string OrdDvsn { get; set; } = "00";

        [JsonPropertyName("RVSE_CNCL_DVSN_CD")]
        public string RvseCnclDvsnCd { get; set; } = string.Empty;

        [JsonPropertyName("ORD_QTY")]
        public string OrdQty { get; set; } = string.Empty;

        [JsonPropertyName("ORD_UNPR")]
        public string OrdUnpr { get; set; } = string.Empty;

        [JsonPropertyName("QTY_ALL_ORD_YN")]
        public string QtyAllOrdYn { get; set; } = "Y";

        [JsonPropertyName("CNDT_PRIC")]
        public string CndtPric { get; set; } = "0";

        [JsonPropertyName("EXCG_ID_DVSN_CD")]
        public string ExcgIdDvsnCd { get; set; } = "KRX";
    }
}
