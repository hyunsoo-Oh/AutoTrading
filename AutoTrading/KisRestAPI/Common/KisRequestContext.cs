using KisRestAPI.Configuration;

namespace KisRestAPI.Common
{
    /// <summary>
    /// 요청 시작 시점의 거래 환경 스냅샷
    ///
    /// 왜 필요한가?
    /// - KisTradingService.CurrentEnvironment는 전역 mutable 상태이다.
    /// - 비동기 메서드 실행 중간에 환경이 바뀌면 URL/TR ID/토큰이 서로 다른 환경에서 섞일 수 있다.
    /// - 요청 시작 시 한 번 스냅샷을 찍어 해당 요청은 끝까지 동일한 설정을 사용하도록 보장한다.
    ///
    /// 사용 방식 (라이브러리 내부):
    /// - 각 서비스 메서드 진입 시 CaptureContext()로 스냅샷 생성
    /// - 이후 ctx.Mode / ctx.Settings만 사용
    ///
    /// 사용 방식 (앱 개발자):
    /// - var ctx = tradingService.CreateSnapshot();
    /// - 필요할 때 ctx.Mode, ctx.Settings로 스냅샷 값을 참조
    /// </summary>
    public sealed record KisRequestContext(
        /// <summary>스냅샷 시점의 거래 모드 (Mock / Live)</summary>
        KisTradingMode Mode,
        /// <summary>스냅샷 시점의 API 접속 설정 (BaseUrl, AppKey, AppSecret, AccountNumber)</summary>
        ApiEndpointSettings Settings);
}
