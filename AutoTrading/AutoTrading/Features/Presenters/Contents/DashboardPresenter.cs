using KisRestAPI.Configuration;
using KisRestAPI.Models.Accounts;
using KisRestAPI.Models.Market;
using KisRestAPI.Models.Overseas;
using AutoTrading.Features.Views.Interfaces;
using KisRestAPI.Accounts;
using KisRestAPI.Auth;
using KisRestAPI.Common;
using KisRestAPI.Common.Http;
using KisRestAPI.Market;
using KisRestAPI.Overseas;

namespace AutoTrading.Features.Presenters.Dashboard
{
    /// <summary>
    /// Dashboard의 비즈니스 로직 담당 Presenter
    ///
    /// MainPresenter와 동일한 패턴:
    /// - View(IDashboardView)만 알고 구체적 컨트롤은 모른다.
    /// - KIA InquireBalance API를 호출해 결과를 View에 전달한다.
    ///
    /// 모드 전환 즉시 갱신:
    /// - IKisTradingService.EnvironmentChanged 이벤트를 구독한다.
    /// - 전환 시 캐시된 데이터를 즉시 표시하고, 백그라운드에서 최신 데이터를 조회한다.
    /// </summary>
    public class DashboardPresenter : IDisposable
    {
        private readonly IDashboardView _view;
        private readonly IAuthService _authService;
        private readonly ApiSettings _apiSettings;
        private readonly IKisTradingService _kisTradingService;

        private readonly HttpClient _httpClient;
        private readonly IAccountService _accountService;
        private readonly IMarketService _marketService;
        private readonly IOverseasMarketService _overseasMarketService;

        /// <summary>모드별 마지막 조회 결과 캐시</summary>
        private readonly Dictionary<KisTradingMode, BalanceSummaryData> _cache = new();

        /// <summary>
        /// 금리 카드 토글 상태 ? false: 한국 금리 / true: 미국 금리
        /// 3초마다 번갈아 가며 표시한다.
        /// </summary>
        private bool _showUsInterestRate = false;

        public DashboardPresenter(
            IDashboardView view,
            IAuthService authService,
            ApiSettings apiSettings,
            IKisTradingService kisTradingService)
        {
            _view = view;
            _authService = authService;
            _apiSettings = apiSettings;
            _kisTradingService = kisTradingService;

            _httpClient = new HttpClient();
            _accountService = new KisAccountService(_httpClient, kisTradingService, authService);
            _marketService = new KisMarketService(_httpClient, kisTradingService, authService);
            _overseasMarketService = new KisOverseasMarketService(_httpClient, kisTradingService, authService);

            // 사용자가 모드를 전환할 때마다 즉시 갱신
            _kisTradingService.EnvironmentChanged += OnEnvironmentChanged;
        }

        /// <summary>
        /// 모드 전환 이벤트 핸들러 ?
        /// 캐시가 있으면 즉시 표시하고, 최신 데이터를 백그라운드에서 조회한다.
        /// </summary>
        private async void OnEnvironmentChanged(object? sender, KisTradingMode newMode)
        {
            Console.WriteLine($"[DASHBOARD] 모드 전환 감지: {newMode} ? 즉시 갱신 시작");

            if (_cache.TryGetValue(newMode, out BalanceSummaryData? cached))
            {
                _view.UpdateBalanceSummary(
                    cached.TotalEvaluation, cached.Deposits,
                    cached.ProfitLoss, cached.PurchaseAmount);
            }

            await RefreshBalanceAsync();
        }

        /// <summary>
        /// 잔고조회 API 호출 후 View 카드 갱신 + 결과를 모드별 캐시에 저장
        /// 타이머 Tick 및 모드 전환 이벤트에서 호출된다.
        /// </summary>
        public async Task RefreshBalanceAsync()
        {
            try
            {
                KisTradingMode mode = _kisTradingService.CurrentEnvironment;
                ApiEndpointSettings settings = _kisTradingService.GetCurrentSettings();

                var request = new InquireBalanceRequest
                {
                    CANO = settings.AccountNumber,
                    ACNT_PRDT_CD = InquireBalanceAccountProductCodeProvider.Get(mode),
                    AFHR_FLPR_YN = "N",
                    OFL_YN = "",
                    INQR_DVSN = "01",
                    UNPR_DVSN = "01",
                    FUND_STTL_ICLD_YN = "N",
                    FNCG_AMT_AUTO_RDPT_YN = "N",
                    PRCS_DVSN = "00",
                    CTX_AREA_FK100 = "",
                    CTX_AREA_NK100 = ""
                };

                InquireBalanceResponse? response = await _accountService.InquireBalanceAsync(request);

                if (response?.RtCd != "0" || response.Output2 == null || response.Output2.Count == 0)
                {
                    Console.WriteLine($"[DASHBOARD] 잔고조회 실패: {response?.Msg1}");
                    return;
                }

                InquireBalanceSummary summary = response.Output2[0];

                decimal.TryParse(summary.TotalEvaluationAmount,    out decimal totalEval);
                decimal.TryParse(summary.DepositTotalAmount,       out decimal deposits);
                decimal.TryParse(summary.EvaluationProfitLossTotal, out decimal profitLoss);
                decimal.TryParse(summary.PurchaseAmountTotal,      out decimal purchase);

                // ===== 비중 계산용 분모: 주식 평가금액 합계 =====
                // TotalEvaluationAmount(tot_evlu_amt)는 예수금 포함 전체 자산이므로
                // 종목 비중 계산에는 EvaluationAmountTotal(evlu_amt_smtl_amt)을 사용한다.
                decimal.TryParse(summary.EvaluationAmountTotal, out decimal stockEvalTotal);

                // 모드별로 결과를 캐시해 다음 전환 시 즉시 표시에 활용
                _cache[mode] = new BalanceSummaryData(totalEval, deposits, profitLoss, purchase);

                _view.UpdateBalanceSummary(totalEval, deposits, profitLoss, purchase);

                // ===== 보유종목 그리드 갱신 =====
                _view.UpdateHoldings(response.Output1, stockEvalTotal);

                Console.WriteLine($"[DASHBOARD] [{mode}] 갱신 완료 ? 총평가: {totalEval:N0}  예수금: {deposits:N0}  보유종목: {response.Output1.Count}개");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DASHBOARD] 잔고조회 오류: {ex.Message}");
            }
        }

        // =====================================================================
        // ===== StockCard 4종 갱신 =====
        // 코스피 / S&P500 / 환율 / 금리 ? 각각 별도 API를 호출한다.
        // 모의투자 환경에서는 실전 전용 API가 동작하지 않으므로 조용히 스킵한다.
        // =====================================================================

        /// <summary>
        /// StockCard 4종을 한 번에 갱신한다.
        /// 실전 전용 API 실패 시 개별 카드만 스킵하고 나머지는 계속 진행한다.
        /// </summary>
        public async Task RefreshStockCardsAsync()
        {
            // ===== 실전 전용 API는 모의 환경에서 동작하지 않으므로 사전 체크 =====
            bool isLive = _kisTradingService.CurrentEnvironment == KisTradingMode.Live;

            // ===== 4개 카드 갱신을 병렬로 수행하여 응답 대기 시간을 최소화 =====
            await Task.WhenAll(
                RefreshKospiCardAsync(isLive),
                RefreshSnpCardAsync(),
                RefreshExchangeRateCardAsync(),
                RefreshInterestRateCardAsync(isLive)
            );
        }

        // =====================================================================
        // ===== 코스피 ? 국내업종 현재지수[v1_국내주식-063] + 일봉[v1_국내주식-016] =====
        // 현재지수 API로 현재가/전일대비를 가져오고,
        // 해외주식 기간별시세 API(시장코드 N)로 2주간 일봉을 가져온다.
        // =====================================================================

        private async Task RefreshKospiCardAsync(bool isLive)
        {
            if (!isLive) return;

            try
            {
                // ===== 현재가 + 전일대비 =====
                var indexRequest = new InquireIndexPriceRequest
                {
                    FID_COND_MRKT_DIV_CODE = "U",
                    FID_INPUT_ISCD = "0001"   // 코스피
                };
                InquireIndexPriceResponse indexResp = await _marketService.InquireIndexPriceAsync(indexRequest);
                InquireIndexPriceOutput? o = indexResp.Output;
                if (o == null) return;

                double.TryParse(o.BstpNmixPrpr, out double currentPrice);
                double.TryParse(o.BstpNmixPrdyVrss, out double changePrice);
                double.TryParse(o.BstpNmixPrdyCtrt, out double changeRate);

                // ===== 전일대비 부호 반영 ? 하락(4,5)이면 음수로 변환 =====
                if (o.PrdyVrssSign == "4" || o.PrdyVrssSign == "5")
                {
                    changePrice = -Math.Abs(changePrice);
                    changeRate = -Math.Abs(changeRate);
                }

                // ===== 2주간 일봉 (해외주식 기간별시세 API의 N:해외지수 코드로 KOSPI 조회) =====
                // 국내지수 일봉은 별도 API가 없으므로
                // 해외주식 기간별시세의 N(해외지수) 마켓으로 KOSPI 코드를 조회한다.
                string endDate = DateTime.Now.ToString("yyyyMMdd");
                string startDate = DateTime.Now.AddDays(-14).ToString("yyyyMMdd");

                var chartRequest = new InquireOvrsDailyChartPriceRequest
                {
                    FID_COND_MRKT_DIV_CODE = "N",
                    FID_INPUT_ISCD = "KOSPI",
                    FID_INPUT_DATE_1 = startDate,
                    FID_INPUT_DATE_2 = endDate,
                    FID_PERIOD_DIV_CODE = "D"
                };

                List<double> points = new();
                try
                {
                    InquireOvrsDailyChartPriceResponse chartResp =
                        await _overseasMarketService.InquireOvrsDailyChartPriceAsync(chartRequest);

                    // output2를 날짜 오름차순으로 정렬 후 종가를 추출
                    points = chartResp.Output2
                        .Where(c => !string.IsNullOrEmpty(c.StckBsopDate))
                        .OrderBy(c => c.StckBsopDate)
                        .Select(c => { double.TryParse(c.OvrsNmixPrpr, out double v); return v; })
                        .Where(v => v > 0)
                        .ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DASHBOARD] 코스피 일봉 조회 실패 (현재가만 표시): {ex.Message}");
                }

                _view.UpdateStockCard("kospi", "KOSPI", currentPrice, changePrice, changeRate, points);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DASHBOARD] 코스피 카드 갱신 실패: {ex.Message}");
            }
        }

        // =====================================================================
        // ===== S&P500 ? 해외주식 종목/지수/환율 기간별시세[v1_해외주식-012] =====
        // 시장코드 N(해외지수), 종목코드 SPX로 2주간 일봉 + 현재가를 가져온다.
        // 실전/모의 모두 동일 TR ID(FHKST03030100)로 조회 가능.
        // =====================================================================

        private async Task RefreshSnpCardAsync()
        {
            try
            {
                string endDate = DateTime.Now.ToString("yyyyMMdd");
                string startDate = DateTime.Now.AddDays(-14).ToString("yyyyMMdd");

                var request = new InquireOvrsDailyChartPriceRequest
                {
                    FID_COND_MRKT_DIV_CODE = "N",
                    FID_INPUT_ISCD = "SPX",      // S&P 500
                    FID_INPUT_DATE_1 = startDate,
                    FID_INPUT_DATE_2 = endDate,
                    FID_PERIOD_DIV_CODE = "D"
                };

                InquireOvrsDailyChartPriceResponse resp =
                    await _overseasMarketService.InquireOvrsDailyChartPriceAsync(request);

                InquireOvrsDailyChartPriceOutput1? o1 = resp.Output1;
                if (o1 == null) return;

                double.TryParse(o1.OvrsNmixPrpr, out double currentPrice);
                double.TryParse(o1.OvrsNmixPrdyVrss, out double changePrice);
                double.TryParse(o1.PrdyCtrt, out double changeRate);

                // ===== 전일대비 부호 반영 =====
                if (o1.PrdyVrssSign == "4" || o1.PrdyVrssSign == "5")
                {
                    changePrice = -Math.Abs(changePrice);
                    changeRate = -Math.Abs(changeRate);
                }

                List<double> points = resp.Output2
                    .Where(c => !string.IsNullOrEmpty(c.StckBsopDate))
                    .OrderBy(c => c.StckBsopDate)
                    .Select(c => { double.TryParse(c.OvrsNmixPrpr, out double v); return v; })
                    .Where(v => v > 0)
                    .ToList();

                _view.UpdateStockCard("snp", "S&P 500", currentPrice, changePrice, changeRate, points);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DASHBOARD] S&P500 카드 갱신 실패: {ex.Message}");
            }
        }

        // =====================================================================
        // ===== 환율 ? 해외주식 종목/지수/환율 기간별시세[v1_해외주식-012] =====
        // 시장코드 X(환율), 종목코드 FX@KRW로 USD/KRW 환율을 조회한다.
        // =====================================================================

        private async Task RefreshExchangeRateCardAsync()
        {
            try
            {
                string endDate = DateTime.Now.ToString("yyyyMMdd");
                string startDate = DateTime.Now.AddDays(-14).ToString("yyyyMMdd");

                var request = new InquireOvrsDailyChartPriceRequest
                {
                    FID_COND_MRKT_DIV_CODE = "X",
                    FID_INPUT_ISCD = "FX@KRW",   // USD/KRW 환율
                    FID_INPUT_DATE_1 = startDate,
                    FID_INPUT_DATE_2 = endDate,
                    FID_PERIOD_DIV_CODE = "D"
                };

                InquireOvrsDailyChartPriceResponse resp =
                    await _overseasMarketService.InquireOvrsDailyChartPriceAsync(request);

                InquireOvrsDailyChartPriceOutput1? o1 = resp.Output1;
                if (o1 == null) return;

                double.TryParse(o1.OvrsNmixPrpr, out double currentPrice);
                double.TryParse(o1.OvrsNmixPrdyVrss, out double changePrice);
                double.TryParse(o1.PrdyCtrt, out double changeRate);

                if (o1.PrdyVrssSign == "4" || o1.PrdyVrssSign == "5")
                {
                    changePrice = -Math.Abs(changePrice);
                    changeRate = -Math.Abs(changeRate);
                }

                List<double> points = resp.Output2
                    .Where(c => !string.IsNullOrEmpty(c.StckBsopDate))
                    .OrderBy(c => c.StckBsopDate)
                    .Select(c => { double.TryParse(c.OvrsNmixPrpr, out double v); return v; })
                    .Where(v => v > 0)
                    .ToList();

                _view.UpdateStockCard("exchangeRate", "USD/KRW", currentPrice, changePrice, changeRate, points);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DASHBOARD] 환율 카드 갱신 실패: {ex.Message}");
            }
        }

        // =====================================================================
        // ===== 금리 ? 금리 종합(국내채권/금리)[국내주식-155] =====
        // output1에서 미국 금리(US Treasury), output2에서 한국 금리(국고채 3년)를
        // 3초마다 한국 3년 금리 ↔ 미국 10년 금리를 번갈아 표시한다.
        //
        // 금리 API는 일별 스냅샷(당일 1건)만 반환하므로 스파크라인은
        // 해외주식 기간별시세 API에서 국채 지수를 가져와 보충한다.
        // =====================================================================

        private async Task RefreshInterestRateCardAsync(bool isLive)
        {
            if (!isLive) return;

            try
            {
                var request = new CompInterestRequest
                {
                    FID_COND_MRKT_DIV_CODE = "I",
                    FID_COND_SCR_DIV_CODE  = "20702",
                    FID_DIV_CLS_CODE       = "1",
                    FID_DIV_CLS_CODE1      = ""
                };

                CompInterestResponse resp = await _marketService.CompInterestAsync(request);

                foreach (CompInterestOutput1Item item in resp.Output1)
                    Console.WriteLine($"[금리 output1] BcdtCode={item.BcdtCode} | 종목명={item.HtsKorIsnm} | 금리={item.BondMnrtPrpr}%");

                foreach (CompInterestOutput2Item item in resp.Output2)
                    Console.WriteLine($"[금리 output2] BcdtCode={item.BcdtCode} | 종목명={item.HtsKorIsnm} | 금리={item.BondMnrtPrpr}%");

                // ===== 코드 고정 매핑 =====
                // output1 = 미국 금리지표, output2 = 한국 채권/금리
                const string UsBcdtCode = "Y0202";   // 미국 10년T-NOTE 수익률
                const string KrBcdtCode = "Y0101";   // 국고채 3년

                CompInterestOutput1Item? usItem = resp.Output1.FirstOrDefault(x =>
                    string.Equals(x.BcdtCode?.Trim(), UsBcdtCode, StringComparison.OrdinalIgnoreCase));

                CompInterestOutput2Item? krItem = resp.Output2.FirstOrDefault(x =>
                    string.Equals(x.BcdtCode?.Trim(), KrBcdtCode, StringComparison.OrdinalIgnoreCase));

                if (usItem == null || krItem == null)
                {
                    string output1Codes = string.Join(", ", resp.Output1
                        .Select(x => x.BcdtCode?.Trim())
                        .Where(x => !string.IsNullOrEmpty(x))
                        .Distinct());

                    string output2Codes = string.Join(", ", resp.Output2
                        .Select(x => x.BcdtCode?.Trim())
                        .Where(x => !string.IsNullOrEmpty(x))
                        .Distinct());

                    Console.WriteLine(
                        $"[DASHBOARD] 금리 코드 매칭 실패 | Y0202={(usItem != null)} Y0101={(krItem != null)} " +
                        $"| output1=[{output1Codes}] | output2=[{output2Codes}]");
                    return;
                }

                double usRate = 0, usChange = 0, usChangeRate = 0;
                double.TryParse(usItem.BondMnrtPrpr,     out usRate);
                double.TryParse(usItem.BondMnrtPrdyVrss, out usChange);
                double.TryParse(usItem.PrdyCtrt,         out usChangeRate);
                string usName = $"미국 금리 ({usItem.HtsKorIsnm})";

                if (usItem.PrdyVrssSign == "4" || usItem.PrdyVrssSign == "5")
                {
                    usChange = -Math.Abs(usChange);
                    usChangeRate = -Math.Abs(usChangeRate);
                }

                double krRate = 0, krChange = 0, krChangeRate = 0;
                double.TryParse(krItem.BondMnrtPrpr,     out krRate);
                double.TryParse(krItem.BondMnrtPrdyVrss, out krChange);
                double.TryParse(krItem.BstpNmixPrdyCtrt, out krChangeRate);
                string krName = $"한국 금리 ({krItem.HtsKorIsnm})";

                if (krItem.PrdyVrssSign == "4" || krItem.PrdyVrssSign == "5")
                {
                    krChange = -Math.Abs(krChange);
                    krChangeRate = -Math.Abs(krChangeRate);
                }

                // ===== 3개월 스파크라인 =====
                string sparkBcdtCode = _showUsInterestRate ? UsBcdtCode : KrBcdtCode;
                List<double> sparkPoints = new();
                try
                {
                    string endDate   = DateTime.Now.ToString("yyyyMMdd");
                    string startDate = DateTime.Now.AddDays(-90).ToString("yyyyMMdd");

                    var chartRequest = new InquireOvrsDailyChartPriceRequest
                    {
                        FID_COND_MRKT_DIV_CODE = "I",
                        FID_INPUT_ISCD         = sparkBcdtCode,
                        FID_INPUT_DATE_1       = startDate,
                        FID_INPUT_DATE_2       = endDate,
                        FID_PERIOD_DIV_CODE    = "D"
                    };

                    InquireOvrsDailyChartPriceResponse chartResp =
                        await _overseasMarketService.InquireOvrsDailyChartPriceAsync(chartRequest);

                    sparkPoints = chartResp.Output2
                        .Where(c => !string.IsNullOrEmpty(c.StckBsopDate))
                        .OrderBy(c => c.StckBsopDate)
                        .Select(c => { double.TryParse(c.OvrsNmixPrpr, out double v); return v; })
                        .Where(v => v > 0)
                        .ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DASHBOARD] 금리 스파크라인 조회 실패: {ex.Message}");
                }

                // ===== 토글: 한국 금리 ↔ 미국 금리 =====
                if (_showUsInterestRate)
                    _view.UpdateStockCard("interestRate", usName, usRate, usChange, usChangeRate, sparkPoints);
                else
                    _view.UpdateStockCard("interestRate", krName, krRate, krChange, krChangeRate, sparkPoints);

                _showUsInterestRate = !_showUsInterestRate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DASHBOARD] 금리 카드 갱신 실패: {ex.Message}");
            }
        }
        public void Dispose()
        {
            _kisTradingService.EnvironmentChanged -= OnEnvironmentChanged;
            _httpClient.Dispose();
        }

        /// <summary>모드별 잔고 캐시 데이터</summary>
        private record BalanceSummaryData(
            decimal TotalEvaluation,
            decimal Deposits,
            decimal ProfitLoss,
            decimal PurchaseAmount);
    }
}







