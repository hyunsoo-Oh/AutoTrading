using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Features.Models.Api.Accounts
{
    /// <summary>
    /// 주식잔고조회 API의 Query Parameter DTO
    /// </summary>
    public class InquireBalanceRequest
    {
        /// <summary>
        /// 계좌번호 앞 8자리
        /// </summary>
        public string CANO { get; set; } = string.Empty;

        /// <summary>
        /// 계좌상품코드 뒤 2자리
        /// </summary>
        public string ACNT_PRDT_CD { get; set; } = string.Empty;

        /// <summary>
        /// 시간외단일가 / 거래소 여부
        /// N: 기본값
        /// Y: 시간외단일가
        /// X: NXT 정규장
        /// </summary>
        public string AFHR_FLPR_YN { get; set; } = "N";

        /// <summary>
        /// 오프라인 여부
        /// 문서상 기본은 공란
        /// </summary>
        public string OFL_YN { get; set; } = string.Empty;

        /// <summary>
        /// 조회구분
        /// 01 : 대출일별
        /// 현재는 문서 기준 기본값처럼 01로 시작
        /// </summary>
        public string INQR_DVSN { get; set; } = "01";

        /// <summary>
        /// 단가구분
        /// 01 : 기본값
        /// </summary>
        public string UNPR_DVSN { get; set; } = "01";

        /// <summary>
        /// 펀드결제분 포함 여부
        /// N : 포함하지 않음
        /// Y : 포함
        /// </summary>
        public string FUND_STTL_ICLD_YN { get; set; } = "N";

        /// <summary>
        /// 융자금액 자동상환 여부
        /// N : 기본값
        /// </summary>
        public string FNCG_AMT_AUTO_RDPT_YN { get; set; } = "N";

        /// <summary>
        /// 처리구분
        /// 00 : 전일매매포함
        /// 01 : 전일매매미포함
        /// </summary>
        public string PRCS_DVSN { get; set; } = "00";

        /// <summary>
        /// 연속조회 검색조건
        /// 첫 조회 시 공란
        /// 다음 페이지 조회 시 이전 응답의 값을 넣음
        /// </summary>
        public string CTX_AREA_FK100 { get; set; } = string.Empty;

        /// <summary>
        /// 연속조회 키
        /// 첫 조회 시 공란
        /// 다음 페이지 조회 시 이전 응답의 값을 넣음
        /// </summary>
        public string CTX_AREA_NK100 { get; set; } = string.Empty;
    }
}
