using System.Text.Json.Serialization;

namespace AutoTrading.Features.Models.Api.Accounts
{
    /// <summary>
    /// 주식잔고조회_실현손익 output1 배열의 각 종목 항목 DTO
    ///
    /// 일반 잔고조회(InquireBalanceItem)와 유사하지만
    /// 실현손익 조회에 특화된 필드(전일/금일 매매수량, 등락율 등)가 추가되어 있다.
    /// </summary>
    public sealed class InquireBalanceRlzPlItem
    {
        /// <summary>종목코드</summary>
        [JsonPropertyName("pdno")]
        public string ProductCode { get; set; } = string.Empty;

        /// <summary>종목명</summary>
        [JsonPropertyName("prdt_name")]
        public string ProductName { get; set; } = string.Empty;

        /// <summary>매매구분명 (매수/매도)</summary>
        [JsonPropertyName("trad_dvsn_name")]
        public string TradDvsnName { get; set; } = string.Empty;

        /// <summary>전일매수수량</summary>
        [JsonPropertyName("bfdy_buy_qty")]
        public string BfdyBuyQty { get; set; } = string.Empty;

        /// <summary>전일매도수량</summary>
        [JsonPropertyName("bfdy_sll_qty")]
        public string BfdySllQty { get; set; } = string.Empty;

        /// <summary>금일매수수량</summary>
        [JsonPropertyName("thdt_buyqty")]
        public string ThdtBuyQty { get; set; } = string.Empty;

        /// <summary>금일매도수량</summary>
        [JsonPropertyName("thdt_sll_qty")]
        public string ThdtSllQty { get; set; } = string.Empty;

        /// <summary>보유수량</summary>
        [JsonPropertyName("hldg_qty")]
        public string HoldingQuantity { get; set; } = string.Empty;

        /// <summary>주문가능수량</summary>
        [JsonPropertyName("ord_psbl_qty")]
        public string OrderableQuantity { get; set; } = string.Empty;

        /// <summary>매입평균가격 (매입금액 ÷ 보유수량)</summary>
        [JsonPropertyName("pchs_avg_pric")]
        public string PurchaseAveragePrice { get; set; } = string.Empty;

        /// <summary>매입금액</summary>
        [JsonPropertyName("pchs_amt")]
        public string PurchaseAmount { get; set; } = string.Empty;

        /// <summary>현재가</summary>
        [JsonPropertyName("prpr")]
        public string CurrentPrice { get; set; } = string.Empty;

        /// <summary>평가금액</summary>
        [JsonPropertyName("evlu_amt")]
        public string EvaluationAmount { get; set; } = string.Empty;

        /// <summary>평가손익금액 (평가금액 - 매입금액)</summary>
        [JsonPropertyName("evlu_pfls_amt")]
        public string EvaluationProfitLossAmount { get; set; } = string.Empty;

        /// <summary>평가손익율</summary>
        [JsonPropertyName("evlu_pfls_rt")]
        public string EvaluationProfitLossRate { get; set; } = string.Empty;

        /// <summary>평가수익율</summary>
        [JsonPropertyName("evlu_erng_rt")]
        public string EvaluationEarningRate { get; set; } = string.Empty;

        /// <summary>전일대비증감</summary>
        [JsonPropertyName("bfdy_cprs_icdc")]
        public string BfdyCprsIcdc { get; set; } = string.Empty;

        /// <summary>등락율</summary>
        [JsonPropertyName("fltt_rt")]
        public string FlttRt { get; set; } = string.Empty;

        /// <summary>대출일자</summary>
        [JsonPropertyName("loan_dt")]
        public string LoanDt { get; set; } = string.Empty;

        /// <summary>대출금액</summary>
        [JsonPropertyName("loan_amt")]
        public string LoanAmt { get; set; } = string.Empty;

        /// <summary>만기일자</summary>
        [JsonPropertyName("expd_dt")]
        public string ExpdDt { get; set; } = string.Empty;
    }
}
