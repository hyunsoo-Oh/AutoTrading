using AutoTrading.Features.Models.Api.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Services.KoreaInvest.Accounts
{
    /// <summary>
    /// 계좌 관련 API 기능을 제공하는 서비스 인터페이스
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 주식잔고조회 API를 호출하여 역직렬화된 응답 DTO를 반환
        /// </summary>
        Task<InquireBalanceResponse?> InquireBalanceAsync(
            InquireBalanceRequest request,
            CancellationToken cancellationToken = default);
    }
}
