using AutoTrading.Features.Models.Api.Orders;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 주식정정취소가능주문조회 요청 검증기
    /// </summary>
    public static class InquirePsblRvsecnclRequestValidator
    {
        public static void Validate(InquirePsblRvsecnclRequest request)
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

            // ===== 조회구분1 =====
            if (request.INQR_DVSN_1 != "0" && request.INQR_DVSN_1 != "1")
            {
                throw new ArgumentException(
                    "조회구분1(INQR_DVSN_1)은 \"0\"(주문) 또는 \"1\"(종목)만 가능합니다.");
            }

            // ===== 조회구분2 =====
            if (request.INQR_DVSN_2 != "0" && request.INQR_DVSN_2 != "1" && request.INQR_DVSN_2 != "2")
            {
                throw new ArgumentException(
                    "조회구분2(INQR_DVSN_2)는 \"0\"(전체), \"1\"(매도), \"2\"(매수)만 가능합니다.");
            }
        }
    }
}
