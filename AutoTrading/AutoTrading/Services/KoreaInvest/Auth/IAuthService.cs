using AutoTrading.Features.Models.Api.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Services.KoreaInvest.Auth
{
    /// <summary>
    /// 인증 서비스 인터페이스
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// 접근 토큰을 가져온다.
        /// - 유효한 토큰이 있으면 재사용
        /// - 없으면 새로 발급
        /// </summary>
        Task<TokenResponse?> GetAccessTokenAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// POST 요청 Body 기준으로 HashKey를 생성한다.
        /// </summary>
        Task<string> GetHashKeyAsync<T>(T requestBody, CancellationToken cancellationToken = default);

        // ===== 캐시된 토큰을 강제로 무효화한다 =====
        // 테스트 또는 외부에서 연결 끊김을 시뮬레이션할 때 사용한다.
        // 호출 후 GetAccessTokenAsync를 실행하면 서버에서 새 토큰을 발급받는다.
        // ===== =====
        void InvalidateToken();
    }
}
