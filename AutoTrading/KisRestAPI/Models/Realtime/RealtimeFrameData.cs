namespace KisRestAPI.Models.Realtime
{
    /// <summary>
    /// WebSocket 실시간 데이터 프레임 파싱 결과
    ///
    /// 원본 형식: "0|H0STCNT0|001|005930^123929^73100^..."
    /// - IsEncrypted: 암호화 여부 (0=평문, 1=암호화)
    /// - TrId: 거래 ID (ex. H0STCNT0)
    /// - DataCount: 데이터 건수
    /// - RawPayload: ^로 구분된 원시 데이터 전문
    /// </summary>
    public class RealtimeFrameData
    {
        /// <summary>암호화 여부 (true = 복호화 필요)</summary>
        public bool IsEncrypted { get; set; }

        /// <summary>거래 ID (ex. H0STCNT0)</summary>
        public string TrId { get; set; } = string.Empty;

        /// <summary>데이터 건수</summary>
        public int DataCount { get; set; }

        /// <summary>^로 구분된 원시 데이터 (복호화 후의 평문)</summary>
        public string RawPayload { get; set; } = string.Empty;
    }
}
