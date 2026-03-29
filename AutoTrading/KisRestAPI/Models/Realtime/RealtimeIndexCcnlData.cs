namespace KisRestAPI.Models.Realtime
{
    /// <summary>
    /// 국내지수 실시간체결 데이터 [실시간-026]
    ///
    /// TR_ID: H0UPCNT0 (실전 전용, 모의투자 미지원)
    /// ^로 구분된 30개 필드를 순서대로 파싱하여 담는다.
    ///
    /// 왜 이 데이터가 중요한가?
    /// - 국내지수 실시간체결은 KOSPI, KOSDAQ 등 업종지수의 실시간 변동을 알려준다.
    /// - 자동매매 프로그램에서는 시장 전체 흐름을 파악하여
    ///   매매 전략의 공격성을 조절하거나, 시장 급변 시 방어 로직을 작동시킨다.
    /// - 실전 전용 API이므로 모의투자 환경에서는 구독할 수 없다.
    ///
    /// 왜 업종구분코드로 구독하는가?
    /// - 기존 실시간체결가(H0STCNT0)는 종목코드(6자리)를 tr_key로 사용하지만,
    ///   국내지수는 업종구분코드(예: "0001"=코스피)를 tr_key로 사용한다.
    /// - 이것이 첫 번째 지수(업종) 기반 구독 유형이다.
    /// </summary>
    public class RealtimeIndexCcnlData
    {
        // ===== 업종 구분 =====

        /// <summary>[0] 업종 구분 코드 (예: 0001=코스피, 1001=코스닥)</summary>
        public string BstpClsCode { get; set; } = string.Empty;

        // ===== 시간 =====

        /// <summary>[1] 영업 시간 (HHMMSS)</summary>
        public string BsopHour { get; set; } = string.Empty;

        // ===== 지수 현재가 =====

        /// <summary>[2] 현재가 지수</summary>
        public string PrprNmix { get; set; } = string.Empty;

        /// <summary>[3] 전일 대비 부호 (1:상한, 2:상승, 3:보합, 4:하한, 5:하락)</summary>
        public string PrdyVrssSign { get; set; } = string.Empty;

        /// <summary>[4] 업종 지수 전일 대비</summary>
        public string BstpNmixPrdyVrss { get; set; } = string.Empty;

        // ===== 거래량 / 거래대금 =====

        /// <summary>[5] 누적 거래량</summary>
        public string AcmlVol { get; set; } = string.Empty;

        /// <summary>[6] 누적 거래 대금</summary>
        public string AcmlTrPbmn { get; set; } = string.Empty;

        /// <summary>[7] 건별 거래량</summary>
        public string PcasVol { get; set; } = string.Empty;

        /// <summary>[8] 건별 거래 대금</summary>
        public string PcasTrPbmn { get; set; } = string.Empty;

        // ===== 전일 대비율 =====

        /// <summary>[9] 전일 대비율 (%)</summary>
        public string PrdyCtrt { get; set; } = string.Empty;

        // ===== 시가 / 최고가 / 최저가 =====

        /// <summary>[10] 시가 지수</summary>
        public string OprcNmix { get; set; } = string.Empty;

        /// <summary>[11] 지수 최고가</summary>
        public string NmixHgpr { get; set; } = string.Empty;

        /// <summary>[12] 지수 최저가</summary>
        public string NmixLwpr { get; set; } = string.Empty;

        // ===== 시가 대비 =====

        /// <summary>[13] 시가 대비 지수 현재가</summary>
        public string OprcVrssNmixPrpr { get; set; } = string.Empty;

        /// <summary>[14] 시가 대비 지수 부호</summary>
        public string OprcVrssNmixSign { get; set; } = string.Empty;

        // ===== 최고가 대비 =====

        /// <summary>[15] 최고가 대비 지수 현재가</summary>
        public string HgprVrssNmixPrpr { get; set; } = string.Empty;

        /// <summary>[16] 최고가 대비 지수 부호</summary>
        public string HgprVrssNmixSign { get; set; } = string.Empty;

        // ===== 최저가 대비 =====

        /// <summary>[17] 최저가 대비 지수 현재가</summary>
        public string LwprVrssNmixPrpr { get; set; } = string.Empty;

        /// <summary>[18] 최저가 대비 지수 부호</summary>
        public string LwprVrssNmixSign { get; set; } = string.Empty;

        // ===== 전일 종가 대비 비율 =====

        /// <summary>[19] 전일 종가 대비 시가 비율 (%)</summary>
        public string PrdyClprVrssOprcRate { get; set; } = string.Empty;

        /// <summary>[20] 전일 종가 대비 최고가 비율 (%)</summary>
        public string PrdyClprVrssHgprRate { get; set; } = string.Empty;

        /// <summary>[21] 전일 종가 대비 최저가 비율 (%)</summary>
        public string PrdyClprVrssLwprRate { get; set; } = string.Empty;

        // ===== 종목 수 통계 =====

        /// <summary>[22] 상한 종목 수</summary>
        public string UplmIssuCnt { get; set; } = string.Empty;

        /// <summary>[23] 상승 종목 수</summary>
        public string AscnIssuCnt { get; set; } = string.Empty;

        /// <summary>[24] 보합 종목 수</summary>
        public string StnrIssuCnt { get; set; } = string.Empty;

        /// <summary>[25] 하락 종목 수</summary>
        public string DownIssuCnt { get; set; } = string.Empty;

        /// <summary>[26] 하한 종목 수</summary>
        public string LslmIssuCnt { get; set; } = string.Empty;

        // ===== 기세 종목 수 =====

        /// <summary>[27] 기세 상승 종목 수</summary>
        public string QtqtAscnIssuCnt { get; set; } = string.Empty;

        /// <summary>[28] 기세 하락 종목 수</summary>
        public string QtqtDownIssuCnt { get; set; } = string.Empty;

        // ===== TICK 대비 =====

        /// <summary>[29] TICK 대비</summary>
        public string TickVrss { get; set; } = string.Empty;
    }
}
