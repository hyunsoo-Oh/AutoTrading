using KisRestAPI.Models.Realtime;

namespace KisRestAPI.Common.WebSocket
{
    /// <summary>
    /// WebSocket 실시간 데이터 프레임을 파싱한다.
    ///
    /// 원본 형식: "0|H0STCNT0|001|005930^123929^73100^..."
    /// 파이프(|) 구분자 4파트:
    ///   [0] 암호화 여부 (0=평문, 1=암호화)
    ///   [1] TR_ID
    ///   [2] 데이터 건수
    ///   [3] 페이로드 (^로 구분된 필드들)
    ///
    /// JSON 응답(구독 확인)은 이 파서의 대상이 아니다.
    /// </summary>
    internal static class KisWebSocketFrameParser
    {
        /// <summary>
        /// 파이프(|)로 구분된 프레임 문자열을 파싱한다.
        /// JSON이거나 형식이 올바르지 않으면 null을 반환한다.
        /// </summary>
        public static RealtimeFrameData? TryParse(string rawMessage)
        {
            if (string.IsNullOrWhiteSpace(rawMessage))
                return null;

            // ===== JSON 응답은 파이프 프레임이 아니다 =====
            if (rawMessage.TrimStart().StartsWith('{'))
                return null;

            // ===== 파이프로 4파트 분리 =====
            // payload 안에도 | 가 올 수 있으므로 최대 4개까지만 분리한다.
            string[] parts = rawMessage.Split('|', 4);
            if (parts.Length < 4)
                return null;

            bool isEncrypted = parts[0].Trim() == "1";

            if (!int.TryParse(parts[2].Trim(), out int dataCount))
                dataCount = 1;

            return new RealtimeFrameData
            {
                IsEncrypted = isEncrypted,
                TrId = parts[1].Trim(),
                DataCount = dataCount,
                RawPayload = parts[3]
            };
        }
    }
}
