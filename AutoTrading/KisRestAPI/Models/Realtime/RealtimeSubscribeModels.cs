using System.Text.Json.Serialization;

namespace KisRestAPI.Models.Realtime
{
    // =====================================================================
    // ===== WebSocket 구독 요청 메시지 DTO =====
    // 구독(tr_type="1") / 해제(tr_type="2") 모두 동일 형식
    // =====================================================================

    public class RealtimeSubscribeMessage
    {
        [JsonPropertyName("header")]
        public RealtimeSubscribeHeader Header { get; set; } = new();

        [JsonPropertyName("body")]
        public RealtimeSubscribeBody Body { get; set; } = new();
    }

    public class RealtimeSubscribeHeader
    {
        [JsonPropertyName("approval_key")]
        public string ApprovalKey { get; set; } = string.Empty;

        [JsonPropertyName("custtype")]
        public string CustType { get; set; } = "P";

        /// <summary>1: 등록, 2: 해제</summary>
        [JsonPropertyName("tr_type")]
        public string TrType { get; set; } = "1";

        [JsonPropertyName("content-type")]
        public string ContentType { get; set; } = "utf-8";
    }

    public class RealtimeSubscribeBody
    {
        [JsonPropertyName("input")]
        public RealtimeSubscribeInput Input { get; set; } = new();
    }

    public class RealtimeSubscribeInput
    {
        [JsonPropertyName("tr_id")]
        public string TrId { get; set; } = string.Empty;

        [JsonPropertyName("tr_key")]
        public string TrKey { get; set; } = string.Empty;
    }

    // =====================================================================
    // ===== WebSocket 구독 응답 JSON DTO =====
    // 정상 등록 시 body.msg1 = "SUBSCRIBE SUCCESS"
    // 암호화 대상인 경우 body.output.iv / key가 포함된다
    // =====================================================================

    public class RealtimeSubscribeResponse
    {
        [JsonPropertyName("header")]
        public RealtimeSubscribeResponseHeader? Header { get; set; }

        [JsonPropertyName("body")]
        public RealtimeSubscribeResponseBody? Body { get; set; }
    }

    public class RealtimeSubscribeResponseHeader
    {
        [JsonPropertyName("tr_id")]
        public string TrId { get; set; } = string.Empty;

        [JsonPropertyName("tr_key")]
        public string TrKey { get; set; } = string.Empty;

        [JsonPropertyName("encrypt")]
        public string Encrypt { get; set; } = "N";
    }

    public class RealtimeSubscribeResponseBody
    {
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        [JsonPropertyName("output")]
        public RealtimeSubscribeOutput? Output { get; set; }
    }

    public class RealtimeSubscribeOutput
    {
        /// <summary>AES-256 복호화에 필요한 IV (암호화 대상 구독에서만 사용)</summary>
        [JsonPropertyName("iv")]
        public string Iv { get; set; } = string.Empty;

        /// <summary>AES-256 복호화에 필요한 Key (암호화 대상 구독에서만 사용)</summary>
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;
    }
}
