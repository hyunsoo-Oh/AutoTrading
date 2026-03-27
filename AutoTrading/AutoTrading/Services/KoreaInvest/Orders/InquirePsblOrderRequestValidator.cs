using AutoTrading.Features.Models.Api.Orders;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 매수가능조회 요청 검증기
    ///
    /// 왜 사전 검증이 중요한가?
    /// - 자동매매에서 매수 주문 전 가능 금액/수량 확인은 필수 안전장치다.
    /// - 계좌 정보 없이 조회하면 API 오류가 발생하므로 미리 차단한다.
    /// </summary>
    public static class InquirePsblOrderRequestValidator
    {
        public static void Validate(InquirePsblOrderRequest request)
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

            if (string.IsNullOrWhiteSpace(request.ORD_DVSN))
            {
                throw new ArgumentException("주문구분(ORD_DVSN)이 비어 있습니다.");
            }

            if (string.IsNullOrWhiteSpace(request.CMA_EVLU_AMT_ICLD_YN) ||
                (request.CMA_EVLU_AMT_ICLD_YN != "Y" && request.CMA_EVLU_AMT_ICLD_YN != "N"))
            {
                throw new ArgumentException(
                    "CMA평가금액포함여부(CMA_EVLU_AMT_ICLD_YN)는 \"Y\" 또는 \"N\"이어야 합니다.");
            }

            if (string.IsNullOrWhiteSpace(request.OVRS_ICLD_YN) ||
                (request.OVRS_ICLD_YN != "Y" && request.OVRS_ICLD_YN != "N"))
            {
                throw new ArgumentException(
                    "해외포함여부(OVRS_ICLD_YN)는 \"Y\" 또는 \"N\"이어야 합니다.");
            }

            // ===== 지정가 조회 시 단가 필수 =====
            // 시장가(01)는 단가 공란 허용, 지정가(00)는 단가가 있어야 정확한 수량이 나온다.
            if (request.ORD_DVSN == "00" &&
                !string.IsNullOrWhiteSpace(request.PDNO) &&
                string.IsNullOrWhiteSpace(request.ORD_UNPR))
            {
                throw new ArgumentException(
                    "지정가(ORD_DVSN:00)로 종목 수량 조회 시 주문단가(ORD_UNPR)를 입력해야 합니다.");
            }
        }
    }
}
