using AutoTrading.Features.Models.Api.Orders;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 주식주문 정정취소 요청 검증기
    ///
    /// 왜 서비스 호출 전에 검증하는가?
    /// - 잘못된 요청이 API 서버에 도달하면 불필요한 네트워크 비용과 오류 응답이 발생한다.
    /// - 자동매매 프로그램에서 정정/취소는 매우 중요한 작업이므로
    ///   원주문번호, 정정취소구분, 수량 등을 사전에 철저히 검증한다.
    /// </summary>
    public static class OrderRvsecnclRequestValidator
    {
        public static void Validate(OrderRvsecnclRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // ===== 계좌 필수값 =====
            if (string.IsNullOrWhiteSpace(request.Cano))
            {
                throw new ArgumentException("계좌번호(CANO)가 비어 있습니다.");
            }

            if (string.IsNullOrWhiteSpace(request.AcntPrdtCd))
            {
                throw new ArgumentException("계좌상품코드(ACNT_PRDT_CD)가 비어 있습니다.");
            }

            // ===== 원주문 필수값 =====
            // 정정/취소 대상 원주문번호가 없으면 어떤 주문을 정정/취소할지 알 수 없다.
            if (string.IsNullOrWhiteSpace(request.OrgnOdno))
            {
                throw new ArgumentException("원주문번호(ORGN_ODNO)가 비어 있습니다.");
            }

            // ===== 정정취소구분 =====
            // "01": 정정, "02": 취소 — 이 두 값만 허용
            if (request.RvseCnclDvsnCd != "01" && request.RvseCnclDvsnCd != "02")
            {
                throw new ArgumentException(
                    "정정취소구분코드(RVSE_CNCL_DVSN_CD)는 \"01\"(정정) 또는 \"02\"(취소)만 가능합니다. " +
                    $"현재 값: \"{request.RvseCnclDvsnCd}\"");
            }

            // ===== 주문구분 =====
            if (string.IsNullOrWhiteSpace(request.OrdDvsn))
            {
                throw new ArgumentException("주문구분(ORD_DVSN)이 비어 있습니다.");
            }

            // ===== 수량 =====
            if (string.IsNullOrWhiteSpace(request.OrdQty))
            {
                throw new ArgumentException("주문수량(ORD_QTY)이 비어 있습니다.");
            }

            if (!int.TryParse(request.OrdQty, out int qty) || qty <= 0)
            {
                throw new ArgumentException("주문수량(ORD_QTY)은 1 이상의 숫자 문자열이어야 합니다.");
            }

            // ===== 주문단가 =====
            if (string.IsNullOrWhiteSpace(request.OrdUnpr))
            {
                throw new ArgumentException("주문단가(ORD_UNPR)가 비어 있습니다.");
            }

            // ===== 잔량전부주문여부 =====
            if (request.QtyAllOrdYn != "Y" && request.QtyAllOrdYn != "N")
            {
                throw new ArgumentException(
                    "잔량전부주문여부(QTY_ALL_ORD_YN)는 \"Y\" 또는 \"N\"만 가능합니다. " +
                    $"현재 값: \"{request.QtyAllOrdYn}\"");
            }

            // ===== 정정 시 추가 검증 =====
            // 정정(01)이면서 지정가(00)인 경우 단가가 0보다 커야 한다.
            if (request.RvseCnclDvsnCd == "01" && request.OrdDvsn == "00")
            {
                if (!decimal.TryParse(request.OrdUnpr, out decimal price) || price <= 0)
                {
                    throw new ArgumentException(
                        "정정 + 지정가 주문은 주문단가(ORD_UNPR)가 0보다 커야 합니다.");
                }
            }

            // ===== 취소 시 추가 검증 =====
            // 취소(02)이면 단가를 "0"으로 보내는 것을 권장
            if (request.RvseCnclDvsnCd == "02")
            {
                if (request.OrdUnpr != "0")
                {
                    throw new ArgumentException(
                        "취소 주문은 주문단가(ORD_UNPR)를 \"0\"으로 보내는 것을 권장합니다.");
                }
            }
        }
    }
}
