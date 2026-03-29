using KisRestAPI.Common;
using KisRestAPI.Models.Auth;

namespace KisRestAPI.Auth
{
    /// <summary>
    /// 인증 서비스 인터페이스
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// 현재 전역 환경 기준으로 접근 토큰을 가져온다.
        /// - 유효한 토큰이 있으면 재사용
        /// - 없으면 새로 발급
        /// </summary>
        Task<TokenResponse> GetAccessTokenAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 특정 환경(Mock/Live)의 토큰을 발급/반환한다.
        /// 전역 환경을 변경하지 않으므로 동시성 안전하다.
        /// </summary>
        Task<TokenResponse> GetAccessTokenAsync(KisTradingMode mode, CancellationToken cancellationToken = default);

        /// <summary>
        /// 현재 전역 환경 기준으로 HashKey를 생성한다.
        /// </summary>
        Task<string> GetHashKeyAsync<T>(T requestBody, CancellationToken cancellationToken = default);

        /// <summary>
        /// 특정 환경(Mock/Live) 기준으로 HashKey를 생성한다.
        /// 전역 환경을 변경하지 않으므로 동시성 안전하다.
        /// </summary>
        Task<string> GetHashKeyAsync<T>(T requestBody, KisTradingMode mode, CancellationToken cancellationToken = default);

        // ===== WebSocket Approval Key =====

        /// <summary>
        /// 현재 전역 환경 기준으로 WebSocket 접속키(Approval Key)를 발급/반환한다.
        /// </summary>
        Task<string> GetApprovalKeyAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 특정 환경(Mock/Live)의 WebSocket 접속키(Approval Key)를 발급/반환한다.
        /// </summary>
        Task<string> GetApprovalKeyAsync(KisTradingMode mode, CancellationToken cancellationToken = default);

        // ===== 캐시된 토큰을 강제로 무효화한다 =====
        void InvalidateToken();
    }
}
