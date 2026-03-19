# 
| 계층         | 직접 접근 가능 대상                       |
| ---------- | --------------------------------- |
| View       | Presenter만                        |
| Presenter  | View 인터페이스, Service               |
| Service    | Repository, Infrastructure, Model |
| Repository | DB, Model, Infrastructure         |
| Controls   | Model 정도만, Service 직접 호출 금지       |
| Logging    | 전 계층에서 사용 가능하되 진입점은 통일            |

```
AutoTrading
├── Program.cs                 // WinForms 진입점
├── AppBootstrapper.cs         // 앱 시작 초기화
├── Views                      // Form, Panel, Dialog 등 화면
├── Presenters                 // 화면 흐름 제어
├── Models                     // 계좌, 주문, 체결, 시세, 전략 데이터 모델
├── Services                   // 키움 API, 계좌 처리, 주문 검증, 전략 실행
├── Repositories               // SQLite 저장/조회
├── Infrastructure             // UI Dispatcher, Timer, OpenAPI 래퍼, 요청 스케줄링
├── Controls                   // 재사용 가능한 UserControl
├── Logging                    // 로그 기록 및 보관 정책
├── Configuration              // 설정 로드/저장/검증
└── Data                       // SQL 스키마, 마이그레이션, SQLite DB 파일
```

## 프로젝트 구조

```
AutoTrading
├── Program.cs                         // WinForms 진입점, 앱 시작
├── AppBootstrapper.cs                 // 전체 의존성 생성/초기화, 시작 구성
├── ServiceRegistration.cs             // Service, Repository, Presenter 등록
│
├── Views
│   ├── Interfaces
│   │   ├── IMainView.cs               // 메인 화면 계약
│   │   ├── IAccountView.cs            // 계좌 화면 계약
│   │   ├── IOrderView.cs              // 주문 화면 계약
│   │   ├── IWatchListView.cs          // 관심종목 화면 계약
│   │   ├── IConditionView.cs          // 조건검색 화면 계약
│   │   ├── ILogView.cs                // 로그 화면 계약
│   │   ├── IStrategyView.cs           // 전략 화면 계약
│   │   └── ISettingsView.cs           // 설정 화면 계약
│   │
│   ├── Forms
│   │   ├── MainForm.cs                // 메인 UI, Presenter와 연결되는 최상위 화면
│   │   └── SettingsForm.cs            // 설정 화면
│   │
│   └── Panels
│       ├── AccountPanelView.cs        // 계좌/잔고 표시 화면
│       ├── OrderPanelView.cs          // 주문 입력/주문 상태 화면
│       ├── WatchListPanelView.cs      // 관심종목 화면
│       ├── ConditionPanelView.cs      // 조건검색 화면
│       ├── LogPanelView.cs            // 로그 표시 화면
│       └── StrategyPanelView.cs       // 전략 상태/실행 화면
│
├── Presenters
│   ├── Base
│   │   └── PresenterBase.cs           // 공통 Presenter 기반 클래스
│   │
│   ├── Main
│   │   └── MainPresenter.cs           // 메인 화면 흐름 제어
│   ├── Account
│   │   └── AccountPresenter.cs        // 계좌 화면 흐름 제어
│   ├── Order
│   │   └── OrderPresenter.cs          // 주문 입력/검증/결과 흐름 제어
│   ├── WatchList
│   │   └── WatchListPresenter.cs      // 관심종목 관리 흐름 제어
│   ├── Condition
│   │   └── ConditionPresenter.cs      // 조건검색 흐름 제어
│   ├── Strategy
│   │   └── StrategyPresenter.cs       // 전략 실행/중지 흐름 제어
│   └── Settings
│       └── SettingsPresenter.cs       // 설정 로드/저장 흐름 제어
│
├── Models
│   ├── Common
│   │   ├── OperationResult.cs         // 작업 성공/실패 결과
│   │   ├── ValidationResult.cs        // 검증 결과
│   │   └── AppSetting.cs              // 설정 항목 모델
│   │
│   ├── Account
│   │   ├── AccountInfo.cs             // 계좌 기본 정보
│   │   ├── AccountSummary.cs          // 예수금/평가금액 등 계좌 요약
│   │   ├── AccountSnapshot.cs         // 특정 시점 계좌 상태 저장용 모델
│   │   └── Position.cs                // 보유 종목 정보
│   │
│   ├── Trading
│   │   ├── OrderRequest.cs            // 주문 요청 데이터
│   │   ├── OrderRecord.cs             // 주문 이력 데이터
│   │   ├── ExecutionRecord.cs         // 체결 이력 데이터
│   │   ├── OrderState.cs              // 주문 상태 모델
│   │   ├── OrderSide.cs               // 매수/매도 구분 enum
│   │   ├── OrderType.cs               // 지정가/시장가 구분 enum
│   │   └── OrderStatus.cs             // 접수/체결/취소 상태 enum
│   │
│   ├── Market
│   │   ├── WatchListItem.cs           // 관심종목 항목
│   │   ├── StockBasicInfo.cs          // 종목 기본 정보
│   │   ├── RealTimeQuote.cs           // 실시간 시세 정보
│   │   ├── MarketStatusInfo.cs        // 장 상태 정보
│   │   └── ConnectionState.cs         // 로그인/서버 연결 상태
│   │
│   ├── Strategy
│   │   ├── ConditionItem.cs           // 조건검색식 정보
│   │   ├── ConditionSearchResult.cs   // 조건검색 결과
│   │   ├── StrategyConfig.cs          // 전략 설정값
│   │   ├── StrategyRunRecord.cs       // 전략 실행 기록
│   │   └── TradingMode.cs             // 테스트/실거래 모드 enum
│   │
│   ├── Logging
│   │   ├── LogService.cs              // 로그 진입점
│   │   └── FileLogWriter.cs           // 파일 로그 저장
│   │
│   └── Configuration
│       ├── DatabaseOptions.cs         // DB 설정
│       ├── LogOptions.cs              // 로그 설정
│       ├── KiwoomOptions.cs           // 키움 API 관련 설정
│       ├── UiOptions.cs               // UI 관련 설정
│       └── TradingSafetyOptions.cs    // 주문 안전장치 설정
│
├── Services
│   ├── Interfaces
│   │   ├── IApplicationStateService.cs // 앱 전역 상태 관리 계약
│   │   ├── IAccountService.cs          // 계좌 처리 계약
│   │   ├── IWatchListService.cs        // 관심종목 처리 계약
│   │   ├── IOrderValidationService.cs  // 주문 검증 계약
│   │   ├── ITradingGuardService.cs     // 주문 안전장치 계약
│   │   ├── IStrategyExecutionService.cs // 전략 실행 계약
│   │   ├── IStateMonitorService.cs     // 상태 감시 계약
│   │   ├── IRecoveryService.cs         // 복구 처리 계약
│   │   ├── IKiwoomSessionService.cs    // 로그인/세션 계약
│   │   ├── IKiwoomTrService.cs         // TR 요청 처리 계약
│   │   ├── IKiwoomRealtimeService.cs   // 실시간 수신 계약
│   │   ├── IKiwoomOrderService.cs      // 주문 요청 계약
│   │   └── IKiwoomConditionService.cs  // 조건검색 계약
│   │
│   ├── Kiwoom
│   │   ├── KiwoomSessionService.cs    // 로그인, 연결 상태 관리
│   │   ├── KiwoomTrService.cs         // TR 요청/응답 처리
│   │   ├── KiwoomRealtimeService.cs   // 실시간 등록/해제/수신 처리
│   │   ├── KiwoomOrderService.cs      // 주문 요청/정정/취소 처리
│   │   └── KiwoomConditionService.cs  // 조건검색 목록/실행 처리
│   │
│   ├── Trading
│   │   ├── AccountService.cs          // 계좌 데이터 가공/조회
│   │   ├── WatchListService.cs        // 관심종목 저장/조회/동기화
│   │   ├── OrderValidationService.cs  // 주문 전 입력/상태 검증
│   │   ├── TradingGuardService.cs     // 비정상 상태 주문 차단
│   │   └── StateMonitorService.cs     // 연결/장상태/주문 가능 상태 점검
│   │
│   ├── Strategy
│   │   ├── StrategyEngine.cs          // 전략 판단 로직
│   │   └── StrategyExecutionService.cs // 전략 실행 흐름
│   │
│   └── Application
│       ├── ApplicationStateService.cs // 전역 상태 저장
│       └── RecoveryService.cs         // 재시작 시 상태 복구
│
├── Repositories
│   ├── Interfaces
│   │   ├── IDbConnectionFactory.cs    // DB 연결 생성 계약
│   │   ├── IDbInitializer.cs          // DB 초기화 계약
│   │   ├── IAppSettingsRepository.cs  // 설정 저장소 계약
│   │   ├── IWatchListRepository.cs    // 관심종목 저장소 계약
│   │   ├── IOrderRepository.cs        // 주문 저장소 계약
│   │   ├── IExecutionRepository.cs    // 체결 저장소 계약
│   │   ├── IPositionRepository.cs     // 포지션 저장소 계약
│   │   ├── IAccountSnapshotRepository.cs // 계좌 스냅샷 저장소 계약
│   │   ├── IConditionRepository.cs    // 조건식 저장소 계약
│   │   ├── IStrategyRunRepository.cs  // 전략 실행 이력 저장소 계약
│   │   └── ISystemLogRepository.cs    // 로그 저장소 계약
│   │
│   ├── Sqlite
│   │   ├── SqliteConnectionFactory.cs // SQLite 연결 생성
│   │   ├── SqliteDbInitializer.cs     // 테이블 생성/스키마 초기화
│   │   ├── AppSettingsRepository.cs   // 설정 저장/조회
│   │   ├── WatchListRepository.cs     // 관심종목 저장/조회
│   │   ├── OrderRepository.cs         // 주문 이력 저장/조회
│   │   ├── ExecutionRepository.cs     // 체결 이력 저장/조회
│   │   ├── PositionRepository.cs      // 포지션 저장/조회
│   │   ├── AccountSnapshotRepository.cs // 계좌 스냅샷 저장/조회
│   │   ├── ConditionRepository.cs     // 조건식 저장/조회
│   │   ├── StrategyRunRepository.cs   // 전략 실행 기록 저장/조회
│   │   └── SystemLogRepository.cs     // 로그 저장/조회
│   │
│   └── Common
│       └── RepositoryBase.cs          // 공통 DB 실행 보조 기반 클래스
│
├── Infrastructure
│   ├── Bootstrap
│   │   └── DependencyContainer.cs     // 객체 생성/주입 관리
│   │
│   ├── Threading
│   │   ├── IUiDispatcher.cs           // UI 스레드 호출 계약
│   │   └── UiDispatcher.cs            // Control.Invoke 래핑
│   │
│   ├── Scheduling
│   │   ├── SafeTimer.cs               // 예외 안전 타이머
│   │   ├── PeriodicTaskRunner.cs      // 주기 작업 공통 실행기
│   │   └── ClockTicker.cs             // 시계/시간 갱신용 주기 작업
│   │
│   ├── Events
│   │   ├── IEventBus.cs               // 내부 이벤트 버스 계약
│   │   └── EventBus.cs                // 화면/서비스 간 이벤트 전달
│   │
│   ├── Kiwoom
│   │   ├── IKiwoomControlAdapter.cs   // AxKHOpenAPI 래핑 계약
│   │   ├── KiwoomControlAdapter.cs    // OpenAPI 직접 접근 래퍼
│   │   ├── IScreenNumberManager.cs    // 화면번호 관리 계약
│   │   ├── ScreenNumberManager.cs     // 화면번호 발급/회수
│   │   ├── ITrRateLimiter.cs          // TR 요청 제한 계약
│   │   ├── TrRateLimiter.cs           // 과도한 TR 요청 방지
│   │   ├── ITrRequestScheduler.cs     // TR 요청 큐 계약
│   │   ├── TrRequestScheduler.cs      // 순차 TR 요청 처리
│   │   ├── IKiwoomErrorTranslator.cs  // 오류코드 해석 계약
│   │   ├── KiwoomErrorTranslator.cs   // 에러코드 → 메시지 변환
│   │   ├── IRealtimeFieldParser.cs    // 실시간 데이터 파서 계약
│   │   ├── RealtimeFieldParser.cs     // FID 수신값 파싱
│   │   ├── IChejanParser.cs           // 체결 데이터 파서 계약
│   │   └── ChejanParser.cs            // 주문/체결 이벤트 파싱
│   │
│   ├── Resilience
│   │   ├── RetryPolicyExecutor.cs     // 재시도 정책 실행기
│   │   └── GlobalExceptionHandler.cs  // 전역 예외 처리
│   │
│   └── Utilities
│       ├── AppPathProvider.cs         // DB/로그/설정 경로 관리
│       ├── SystemClock.cs             // 현재 시각 제공
│       └── Debouncer.cs               // 연속 이벤트 묶음 처리
│
├── Controls
│   ├── Common
│   │   ├── SectionHeaderControl.cs    // 섹션 제목 공통 컨트롤
│   │   ├── DoubleBufferedPanel.cs     // 깜빡임 방지 패널
│   │   └── DoubleBufferedDataGridView.cs // 깜빡임 방지 그리드
│   │
│   ├── Cards
│   │   ├── KpiCardControl.cs          // KPI 카드
│   │   ├── AccountSummaryCardControl.cs // 계좌 요약 카드
│   │   └── StatusCardControl.cs       // 상태 표시 카드
│   │
│   ├── Grids
│   │   ├── WatchListGridControl.cs    // 관심종목 그리드
│   │   ├── PositionGridControl.cs     // 보유종목 그리드
│   │   ├── OrderHistoryGridControl.cs // 주문 이력 그리드
│   │   └── ExecutionHistoryGridControl.cs // 체결 이력 그리드
│   │
│   └── Status
│       ├── StatusBadgeControl.cs      // 상태 뱃지
│       └── MarketStatusStripControl.cs // 상단/하단 상태 바
│
├── Logging
│   ├── Writers
│   │   ├── ILogWriter.cs              // 로그 출력 계약
│   │   ├── FileLogWriter.cs           // 파일 로그 저장
│   │   └── DbLogWriter.cs             // DB 로그 저장
│   │
│   ├── Formatters
│   │   ├── ILogFormatter.cs           // 로그 포맷 계약
│   │   └── DefaultLogFormatter.cs     // 기본 로그 문자열 포맷
│   │
│   ├── Policies
│   │   ├── LogRetentionPolicy.cs      // 로그 보관 정책
│   │   └── LogMaskingHelper.cs        // 민감정보 마스킹
│   │
│   └── LogService.cs                  // 로그 기록 통합 진입점
│
├── Configuration
│   ├── Options
│   │   ├── AppConfig.cs               // 전체 설정 루트
│   │   ├── DatabaseOptions.cs         // DB 옵션
│   │   ├── LogOptions.cs              // 로그 옵션
│   │   ├── KiwoomOptions.cs           // 키움 옵션
│   │   ├── UiOptions.cs               // UI 옵션
│   │   ├── StrategyOptions.cs         // 전략 옵션
│   │   └── TradingSafetyOptions.cs    // 안전장치 옵션
│   │
│   ├── Loaders
│   │   ├── IConfigurationService.cs   // 설정 서비스 계약
│   │   ├── ConfigurationService.cs    // 설정 읽기/저장
│   │   └── ConfigurationLoader.cs     // 파일에서 설정 로드
│   │
│   └── Validators
│       └── ConfigurationValidator.cs  // 설정값 검증
│
├── Resources
│   ├── Icons                          // 아이콘 리소스
│   ├── Images                         // 이미지 리소스
│   ├── Strings.resx                   // 공통 문자열 리소스
│   ├── Icons.resx                     // 아이콘 리소스 관리
│   ├── ThemePalette.cs                // 공통 색상/간격/폰트 기준
│   └── ResourceKeys.cs                // 리소스 키 상수
│
└── Data
    ├── Schema
    │   ├── 001_init.sql               // 초기 테이블 생성
    │   └── 002_indexes.sql            // 인덱스/추가 스키마
    ├── Migrations
    │   └── 003_add_strategy_runs.sql  // DB 변경 스크립트
    ├── Seed
    │   └── seed_appsettings.sql       // 초기 설정 데이터
    └── autotrading.db                 // SQLite DB 파일
```