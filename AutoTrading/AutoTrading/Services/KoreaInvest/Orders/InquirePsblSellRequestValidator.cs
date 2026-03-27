using AutoTrading.Features.Models.Api.Orders;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 매도가능수량조회 요청 검증기
    ///
    /// 왜 사전 검증이 중요한가?
    /// - 매도 주문 전 실제 보유 수량 확인은 필수 안전장치다.
    /// - 계좌 정보나 종목코드 없이 조회하면 API 오류가 발생하므로 미리 차단한다.
    /// </summary>
    public static class InquirePsblSellRequestValidator
    {
        public static void Validate(InquirePsblSellRequest request)
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

            if (string.IsNullOrWhiteSpace(request.PDNO))
            {
                throw new ArgumentException("종목번호(PDNO)가 비어 있습니다.");
            }
        }
    }
}
