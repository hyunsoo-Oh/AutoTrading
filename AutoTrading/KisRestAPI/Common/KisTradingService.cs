using KisRestAPI.Configuration;

namespace KisRestAPI.Common
{
    /// <summary>
    /// 현재 사용 중인 한국투자증권 서버 환경(모의/실전)을 메모리에서 관리
    /// </summary>
    public class KisTradingService : Http.IKisTradingService
    {
        private readonly ApiSettings _apiSettings;
        private KisTradingMode _currentEnvironment = KisTradingMode.Live;

        /// <summary>
        /// 현재 서버 환경을 반환
        /// </summary>
        public KisTradingMode CurrentEnvironment => _currentEnvironment;

        /// <inheritdoc />
        public event EventHandler<KisTradingMode>? EnvironmentChanged;

        public KisTradingService(ApiSettings apiSettings)
        {
            _apiSettings = apiSettings;
        }

        /// <summary>
        /// 내부 전용 ? 이벤트 없이 환경만 변경한다. (토큰 갱신 루프 등)
        /// </summary>
        public void SetEnvironment(KisTradingMode environment)
        {
            _currentEnvironment = environment;
        }

        /// <summary>
        /// 사용자 액션 ? 환경을 변경하고 EnvironmentChanged 이벤트를 발생시킨다.
        /// </summary>
        public void SwitchEnvironment(KisTradingMode environment)
        {
            _currentEnvironment = environment;
            EnvironmentChanged?.Invoke(this, environment);
        }

        public ApiEndpointSettings GetCurrentSettings()
        {
            return _currentEnvironment == KisTradingMode.Live
                ? _apiSettings.Live
                : _apiSettings.Mock;
        }

        /// <summary>
        /// 현재 환경의 불변 스냅샷을 생성한다.
        /// 요청 시작 시점에 호출하면 요청 종료까지 동일한 설정을 보장한다.
        ///
        /// 왜 로컬 변수에 한 번만 읽는가?
        /// - _currentEnvironment를 두 번 읽으면 그 사이에 SetEnvironment/SwitchEnvironment가
        ///   끼어들어 mode와 settings가 서로 다른 환경으로 섞일 수 있다.
        /// - 로컬 변수에 한 번만 읽으면 항상 같은 시점의 mode/settings 조합이 보장된다.
        /// </summary>
        public KisRequestContext CreateSnapshot()
        {
            KisTradingMode mode = _currentEnvironment;
            ApiEndpointSettings settings = mode == KisTradingMode.Live
                ? _apiSettings.Live
                : _apiSettings.Mock;
            return new KisRequestContext(mode, settings);
        }
    }
}
