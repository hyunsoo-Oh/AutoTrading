using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoTrading.Features.Models.Api.Accounts
{
    /// <summary>
    /// 주식잔고조회 API의 루트 응답 DTO
    /// </summary>
    public class InquireBalanceResponse
    {
        /// <summary>
        /// 성공/실패 코드
        /// 0이면 성공
        /// </summary>
        [JsonPropertyName("rt_cd")]
        public string RtCd { get; set; } = string.Empty;

        /// <summary>
        /// 응답 코드
        /// </summary>
        [JsonPropertyName("msg_cd")]
        public string MsgCd { get; set; } = string.Empty;

        /// <summary>
        /// 응답 메시지
        /// </summary>
        [JsonPropertyName("msg1")]
        public string Msg1 { get; set; } = string.Empty;

        /// <summary>
        /// 연속조회 검색조건
        /// 다음 페이지 요청 시 사용
        /// </summary>
        [JsonPropertyName("ctx_area_fk100")]
        public string CtxAreaFk100 { get; set; } = string.Empty;

        /// <summary>
        /// 연속조회 키
        /// 다음 페이지 요청 시 사용
        /// </summary>
        [JsonPropertyName("ctx_area_nk100")]
        public string CtxAreaNk100 { get; set; } = string.Empty;

        /// <summary>
        /// 종목별 잔고 목록
        /// </summary>
        [JsonPropertyName("output1")]
        public List<InquireBalanceItem> Output1 { get; set; } = new();

        /// <summary>
        /// 계좌 요약 정보
        /// 문서상 배열 구조라 List로 받습니다.
        /// </summary>
        [JsonPropertyName("output2")]
        public List<InquireBalanceSummary> Output2 { get; set; } = new();
    }
}
