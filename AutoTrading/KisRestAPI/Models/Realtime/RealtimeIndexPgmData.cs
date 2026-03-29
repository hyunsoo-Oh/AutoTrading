namespace KisRestAPI.Models.Realtime
{
    /// <summary>
    /// 국내지수 실시간프로그램매매 데이터 [실시간-028]
    ///
    /// TR_ID: H0UPPGM0 (실전 전용, 모의투자 미지원)
    /// ^로 구분된 88개 필드를 순서대로 파싱하여 담는다.
    ///
    /// 왜 이 데이터가 자동매매에서 중요한가?
    /// - 프로그램매매(차익/비차익)의 매도·매수 현황을 실시간으로 파악할 수 있다.
    /// - 기관·외국인의 프로그램매매 방향은 시장 수급의 핵심 지표이다.
    /// - 프로그램 순매수 급감 시 시장 하락 압력으로 작용하므로
    ///   자동매매 전략의 공격성을 조절하는 데 활용할 수 있다.
    ///
    /// 왜 업종구분코드로 구독하는가?
    /// - 프로그램매매 데이터는 개별 종목이 아닌 업종(시장) 단위로 제공된다.
    /// - tr_key에 업종구분코드(예: "0001"=코스피, "1001"=코스닥)를 사용한다.
    ///
    /// 데이터 구조:
    /// - [0~1] 업종구분 + 영업시간
    /// - [2~9] 차익/비차익 매도·매수 위탁·자기 체결량 (8개)
    /// - [10~17] 차익/비차익 매도·매수 위탁·자기 체결금액 (8개)
    /// - [18~29] 차익 합계 (매도/매수/순매수 거래량·비율·대금·비율) (12개)
    /// - [30~41] 비차익 합계 (12개)
    /// - [42~53] 전체 위탁 (매도/매수/순매수 거래량·비율·대금·비율) (12개)
    /// - [54~65] 전체 자기 (12개)
    /// - [66~77] 총합계 (매도/매수/순매수 거래량·비율·대금·비율) (12개)
    /// - [78~85] 차익/비차익 위탁·자기 순매수 수량·대금 (8개)
    /// - [86~87] 누적 거래량·대금 (2개)
    /// </summary>
    public class RealtimeIndexPgmData
    {
        // ===== 업종 구분 =====

        /// <summary>[0] 업종 구분 코드 (예: 0001=코스피, 1001=코스닥)</summary>
        public string BstpClsCode { get; set; } = string.Empty;

        // ===== 시간 =====

        /// <summary>[1] 영업 시간 (HHMMSS)</summary>
        public string BsopHour { get; set; } = string.Empty;

        // ===== 차익/비차익 매도·매수 위탁·자기 체결량 (8개) =====

        /// <summary>[2] 차익 매도 위탁 체결량</summary>
        public string ArbtSelnEntmCnqn { get; set; } = string.Empty;

        /// <summary>[3] 차익 매도 자기 체결량</summary>
        public string ArbtSelnOnslCnqn { get; set; } = string.Empty;

        /// <summary>[4] 차익 매수 위탁 체결량</summary>
        public string ArbtShnuEntmCnqn { get; set; } = string.Empty;

        /// <summary>[5] 차익 매수 자기 체결량</summary>
        public string ArbtShnuOnslCnqn { get; set; } = string.Empty;

        /// <summary>[6] 비차익 매도 위탁 체결량</summary>
        public string NabtSelnEntmCnqn { get; set; } = string.Empty;

        /// <summary>[7] 비차익 매도 자기 체결량</summary>
        public string NabtSelnOnslCnqn { get; set; } = string.Empty;

        /// <summary>[8] 비차익 매수 위탁 체결량</summary>
        public string NabtShnuEntmCnqn { get; set; } = string.Empty;

        /// <summary>[9] 비차익 매수 자기 체결량</summary>
        public string NabtShnuOnslCnqn { get; set; } = string.Empty;

        // ===== 차익/비차익 매도·매수 위탁·자기 체결금액 (8개) =====

        /// <summary>[10] 차익 매도 위탁 체결 금액</summary>
        public string ArbtSelnEntmCntgAmt { get; set; } = string.Empty;

        /// <summary>[11] 차익 매도 자기 체결 금액</summary>
        public string ArbtSelnOnslCntgAmt { get; set; } = string.Empty;

        /// <summary>[12] 차익 매수 위탁 체결 금액</summary>
        public string ArbtShnuEntmCntgAmt { get; set; } = string.Empty;

        /// <summary>[13] 차익 매수 자기 체결 금액</summary>
        public string ArbtShnuOnslCntgAmt { get; set; } = string.Empty;

        /// <summary>[14] 비차익 매도 위탁 체결 금액</summary>
        public string NabtSelnEntmCntgAmt { get; set; } = string.Empty;

        /// <summary>[15] 비차익 매도 자기 체결 금액</summary>
        public string NabtSelnOnslCntgAmt { get; set; } = string.Empty;

        /// <summary>[16] 비차익 매수 위탁 체결 금액</summary>
        public string NabtShnuEntmCntgAmt { get; set; } = string.Empty;

        /// <summary>[17] 비차익 매수 자기 체결 금액</summary>
        public string NabtShnuOnslCntgAmt { get; set; } = string.Empty;

        // ===== 차익 합계 (12개) =====

        /// <summary>[18] 차익 합계 매도 거래량</summary>
        public string ArbtSmtnSelnVol { get; set; } = string.Empty;

        /// <summary>[19] 차익 합계 매도 거래량 비율</summary>
        public string ArbtSmtmSelnVolRate { get; set; } = string.Empty;

        /// <summary>[20] 차익 합계 매도 거래 대금</summary>
        public string ArbtSmtnSelnTrPbmn { get; set; } = string.Empty;

        /// <summary>[21] 차익 합계 매도 거래대금 비율</summary>
        public string ArbtSmtmSelnTrPbmnRate { get; set; } = string.Empty;

        /// <summary>[22] 차익 합계 매수 거래량</summary>
        public string ArbtSmtnShnuVol { get; set; } = string.Empty;

        /// <summary>[23] 차익 합계 매수 거래량 비율</summary>
        public string ArbtSmtmShnuVolRate { get; set; } = string.Empty;

        /// <summary>[24] 차익 합계 매수 거래 대금</summary>
        public string ArbtSmtnShnuTrPbmn { get; set; } = string.Empty;

        /// <summary>[25] 차익 합계 매수 거래대금 비율</summary>
        public string ArbtSmtmShnuTrPbmnRate { get; set; } = string.Empty;

        /// <summary>[26] 차익 합계 순매수 수량</summary>
        public string ArbtSmtnNtbyQty { get; set; } = string.Empty;

        /// <summary>[27] 차익 합계 순매수 수량 비율</summary>
        public string ArbtSmtmNtbyQtyRate { get; set; } = string.Empty;

        /// <summary>[28] 차익 합계 순매수 거래 대금</summary>
        public string ArbtSmtnNtbyTrPbmn { get; set; } = string.Empty;

        /// <summary>[29] 차익 합계 순매수 거래대금 비율</summary>
        public string ArbtSmtmNtbyTrPbmnRate { get; set; } = string.Empty;

        // ===== 비차익 합계 (12개) =====

        /// <summary>[30] 비차익 합계 매도 거래량</summary>
        public string NabtSmtnSelnVol { get; set; } = string.Empty;

        /// <summary>[31] 비차익 합계 매도 거래량 비율</summary>
        public string NabtSmtmSelnVolRate { get; set; } = string.Empty;

        /// <summary>[32] 비차익 합계 매도 거래 대금</summary>
        public string NabtSmtnSelnTrPbmn { get; set; } = string.Empty;

        /// <summary>[33] 비차익 합계 매도 거래대금 비율</summary>
        public string NabtSmtmSelnTrPbmnRate { get; set; } = string.Empty;

        /// <summary>[34] 비차익 합계 매수 거래량</summary>
        public string NabtSmtnShnuVol { get; set; } = string.Empty;

        /// <summary>[35] 비차익 합계 매수 거래량 비율</summary>
        public string NabtSmtmShnuVolRate { get; set; } = string.Empty;

        /// <summary>[36] 비차익 합계 매수 거래 대금</summary>
        public string NabtSmtnShnuTrPbmn { get; set; } = string.Empty;

        /// <summary>[37] 비차익 합계 매수 거래대금 비율</summary>
        public string NabtSmtmShnuTrPbmnRate { get; set; } = string.Empty;

        /// <summary>[38] 비차익 합계 순매수 수량</summary>
        public string NabtSmtnNtbyQty { get; set; } = string.Empty;

        /// <summary>[39] 비차익 합계 순매수 수량 비율</summary>
        public string NabtSmtmNtbyQtyRate { get; set; } = string.Empty;

        /// <summary>[40] 비차익 합계 순매수 거래 대금</summary>
        public string NabtSmtnNtbyTrPbmn { get; set; } = string.Empty;

        /// <summary>[41] 비차익 합계 순매수 거래대금 비율</summary>
        public string NabtSmtmNtbyTrPbmnRate { get; set; } = string.Empty;

        // ===== 전체 위탁 (12개) =====

        /// <summary>[42] 전체 위탁 매도 거래량</summary>
        public string WholEntmSelnVol { get; set; } = string.Empty;

        /// <summary>[43] 위탁 매도 거래량 비율</summary>
        public string EntmSelnVolRate { get; set; } = string.Empty;

        /// <summary>[44] 전체 위탁 매도 거래 대금</summary>
        public string WholEntmSelnTrPbmn { get; set; } = string.Empty;

        /// <summary>[45] 위탁 매도 거래대금 비율</summary>
        public string EntmSelnTrPbmnRate { get; set; } = string.Empty;

        /// <summary>[46] 전체 위탁 매수 거래량</summary>
        public string WholEntmShnuVol { get; set; } = string.Empty;

        /// <summary>[47] 위탁 매수 거래량 비율</summary>
        public string EntmShnuVolRate { get; set; } = string.Empty;

        /// <summary>[48] 전체 위탁 매수 거래 대금</summary>
        public string WholEntmShnuTrPbmn { get; set; } = string.Empty;

        /// <summary>[49] 위탁 매수 거래대금 비율</summary>
        public string EntmShnuTrPbmnRate { get; set; } = string.Empty;

        /// <summary>[50] 전체 위탁 순매수 수량</summary>
        public string WholEntmNtbyQt { get; set; } = string.Empty;

        /// <summary>[51] 위탁 순매수 수량 비율</summary>
        public string EntmNtbyQtyRat { get; set; } = string.Empty;

        /// <summary>[52] 전체 위탁 순매수 거래 대금</summary>
        public string WholEntmNtbyTrPbmn { get; set; } = string.Empty;

        /// <summary>[53] 위탁 순매수 금액 비율</summary>
        public string EntmNtbyTrPbmnRate { get; set; } = string.Empty;

        // ===== 전체 자기 (12개) =====

        /// <summary>[54] 전체 자기 매도 거래량</summary>
        public string WholOnslSelnVol { get; set; } = string.Empty;

        /// <summary>[55] 자기 매도 거래량 비율</summary>
        public string OnslSelnVolRate { get; set; } = string.Empty;

        /// <summary>[56] 전체 자기 매도 거래 대금</summary>
        public string WholOnslSelnTrPbmn { get; set; } = string.Empty;

        /// <summary>[57] 자기 매도 거래대금 비율</summary>
        public string OnslSelnTrPbmnRate { get; set; } = string.Empty;

        /// <summary>[58] 전체 자기 매수 거래량</summary>
        public string WholOnslShnuVol { get; set; } = string.Empty;

        /// <summary>[59] 자기 매수 거래량 비율</summary>
        public string OnslShnuVolRate { get; set; } = string.Empty;

        /// <summary>[60] 전체 자기 매수 거래 대금</summary>
        public string WholOnslShnuTrPbmn { get; set; } = string.Empty;

        /// <summary>[61] 자기 매수 거래대금 비율</summary>
        public string OnslShnuTrPbmnRate { get; set; } = string.Empty;

        /// <summary>[62] 전체 자기 순매수 수량</summary>
        public string WholOnslNtbyQty { get; set; } = string.Empty;

        /// <summary>[63] 자기 순매수량 비율</summary>
        public string OnslNtbyQtyRate { get; set; } = string.Empty;

        /// <summary>[64] 전체 자기 순매수 거래 대금</summary>
        public string WholOnslNtbyTrPbmn { get; set; } = string.Empty;

        /// <summary>[65] 자기 순매수 대금 비율</summary>
        public string OnslNtbyTrPbmnRate { get; set; } = string.Empty;

        // ===== 총합계 (12개) =====

        /// <summary>[66] 총 매도 수량</summary>
        public string TotalSelnQty { get; set; } = string.Empty;

        /// <summary>[67] 전체 매도 거래량 비율</summary>
        public string WholSelnVolRate { get; set; } = string.Empty;

        /// <summary>[68] 총 매도 거래 대금</summary>
        public string TotalSelnTrPbmn { get; set; } = string.Empty;

        /// <summary>[69] 전체 매도 거래대금 비율</summary>
        public string WholSelnTrPbmnRate { get; set; } = string.Empty;

        /// <summary>[70] 총 매수 수량</summary>
        public string ShnuCntgSmtn { get; set; } = string.Empty;

        /// <summary>[71] 전체 매수 거래량 비율</summary>
        public string WholShunVolRate { get; set; } = string.Empty;

        /// <summary>[72] 총 매수 거래 대금</summary>
        public string TotalShnuTrPbmn { get; set; } = string.Empty;

        /// <summary>[73] 전체 매수 거래대금 비율</summary>
        public string WholShunTrPbmnRate { get; set; } = string.Empty;

        /// <summary>[74] 전체 순매수 수량</summary>
        public string WholNtbyQty { get; set; } = string.Empty;

        /// <summary>[75] 전체 합계 순매수 수량 비율</summary>
        public string WholSmtmNtbyQtyRate { get; set; } = string.Empty;

        /// <summary>[76] 전체 순매수 거래 대금</summary>
        public string WholNtbyTrPbmn { get; set; } = string.Empty;

        /// <summary>[77] 전체 순매수 거래대금 비율</summary>
        public string WholNtbyTrPbmnRate { get; set; } = string.Empty;

        // ===== 차익/비차익 위탁·자기 순매수 (8개) =====

        /// <summary>[78] 차익 위탁 순매수 수량</summary>
        public string ArbtEntmNtbyQty { get; set; } = string.Empty;

        /// <summary>[79] 차익 위탁 순매수 거래 대금</summary>
        public string ArbtEntmNtbyTrPbmn { get; set; } = string.Empty;

        /// <summary>[80] 차익 자기 순매수 수량</summary>
        public string ArbtOnslNtbyQty { get; set; } = string.Empty;

        /// <summary>[81] 차익 자기 순매수 거래 대금</summary>
        public string ArbtOnslNtbyTrPbmn { get; set; } = string.Empty;

        /// <summary>[82] 비차익 위탁 순매수 수량</summary>
        public string NabtEntmNtbyQty { get; set; } = string.Empty;

        /// <summary>[83] 비차익 위탁 순매수 거래 대금</summary>
        public string NabtEntmNtbyTrPbmn { get; set; } = string.Empty;

        /// <summary>[84] 비차익 자기 순매수 수량</summary>
        public string NabtOnslNtbyQty { get; set; } = string.Empty;

        /// <summary>[85] 비차익 자기 순매수 거래 대금</summary>
        public string NabtOnslNtbyTrPbmn { get; set; } = string.Empty;

        // ===== 누적 =====

        /// <summary>[86] 누적 거래량</summary>
        public string AcmlVol { get; set; } = string.Empty;

        /// <summary>[87] 누적 거래 대금</summary>
        public string AcmlTrPbmn { get; set; } = string.Empty;
    }
}
