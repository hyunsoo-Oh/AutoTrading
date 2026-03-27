using AutoTrading.Features.Models.Api.Accounts;

namespace AutoTrading.Services.KoreaInvest.Accounts
{
    /// <summary>
    /// 주식잔고조회_실현손익 요청 검증기
    ///
    /// 왜 사전 검증이 중요한가?
    /// - 계좌 정보 없이 조회하면 API 오류가 발생하므로 미리 차단한다.
    /// - 실전투자 전용 API이므로 환경 체크는 TrIdProvider에서 담당한다.
    /// </summary>
    public static class InquireBalanceRlzPlRequestValidator
    {
        public static void Validate(InquireBalanceRlzPlRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.CANO))
            {
                throw new ArgumentException("계좌번호(CANO)가 비어 있습니다.");
            }

            if (string.IsNullOrWhiteSpace(request.ACNT_PRDT_CD))
            {
                throw new ArgumentException("계좌상품코드(ACNT_PRDT_CD)가 비어 있습니다.");
            }
        }
    }
}
