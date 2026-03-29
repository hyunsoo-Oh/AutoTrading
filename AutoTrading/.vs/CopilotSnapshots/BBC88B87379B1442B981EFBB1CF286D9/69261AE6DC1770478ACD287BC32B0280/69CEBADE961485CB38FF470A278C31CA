using AutoTrading.Features.Models.Api.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Services.KoreaInvest.Orders
{
    /// <summary>
    /// 현금 주문 요청 검증기
    /// </summary>
    public static class OrderCashRequestValidator
    {
        public static void Validate(OrderCashRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Cano))
            {
                throw new ArgumentException("계좌번호(CANO)가 비어 있습니다.");
            }

            if (string.IsNullOrWhiteSpace(request.AcntPrdtCd))
            {
                throw new ArgumentException("계좌상품코드(ACNT_PRDT_CD)가 비어 있습니다.");
            }

            if (string.IsNullOrWhiteSpace(request.PdNo))
            {
                throw new ArgumentException("종목코드(PDNO)가 비어 있습니다.");
            }

            if (string.IsNullOrWhiteSpace(request.OrdDvsn))
            {
                throw new ArgumentException("주문구분(ORD_DVSN)이 비어 있습니다.");
            }

            if (string.IsNullOrWhiteSpace(request.OrdQty))
            {
                throw new ArgumentException("주문수량(ORD_QTY)이 비어 있습니다.");
            }

            if (!int.TryParse(request.OrdQty, out int qty) || qty <= 0)
            {
                throw new ArgumentException("주문수량(ORD_QTY)은 1 이상의 숫자 문자열이어야 합니다.");
            }

            if (string.IsNullOrWhiteSpace(request.OrdUnpr))
            {
                throw new ArgumentException("주문단가(ORD_UNPR)가 비어 있습니다.");
            }

            // ===== 주문구분별 최소 검증 =====
            // 00: 지정가
            // 01: 시장가
            if (request.OrdDvsn == "00")
            {
                if (!decimal.TryParse(request.OrdUnpr, out decimal price) || price <= 0)
                {
                    throw new ArgumentException("지정가 주문은 주문단가(ORD_UNPR)가 0보다 커야 합니다.");
                }
            }
            else if (request.OrdDvsn == "01")
            {
                if (request.OrdUnpr != "0")
                {
                    throw new ArgumentException("시장가 주문은 주문단가(ORD_UNPR)를 \"0\"으로 보내는 것을 권장합니다.");
                }
            }
        }
    }
}
