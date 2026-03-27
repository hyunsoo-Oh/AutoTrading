# 새 API 추가 시 작업 위치

이 문서는 **자동 주식매매 프로그램**에서 **새로운 API를 추가**할 때  
어떤 파일을 **어디에 만들고**, 각 계층이 무슨 책임을 가지며,  
최종적으로 **어떻게 화면까지 연결하는지**를 일관되게 설명하기 위한 가이드  

- 화면(View)과 API 호출 로직(Service)을 분리하기 위해  

- 기능이 늘어나도 파일 위치를 쉽게 찾고 유지보수하기 위해
- 초급 개발자도 “어디를 수정해야 하는지” 바로 알 수 있게 하기 위해
- 나중에 API 변경, 화면 변경, 테스트 코드 작성 시 영향 범위를 줄이기 위해

```
① Features/Models/Api/{도메인}/   ← Request, Response DTO
        ↓
② Services/KoreaInvest/{도메인}/  ← Interface, Service, UrlBuilder,
                                     QueryStringBuilder, HeaderBuilder, TrIdProvider
        ↓
③ Features/Views/Interfaces/     ← IXxxView 인터페이스
④ Features/Views/Contents/       ← UserControl 화면
        ↓
⑤ Features/Presenters/Contents/  ← XxxPresenter (View + Service 연결)
        ↓
⑥ Program.cs                     ← 조립 & 등록
```

## Models — 요청/응답 DTO
### Features/Models/Api/{도메인명}/
#### API 요청/응답 데이터를 담는 DTO 정의
> DTO는 API 통신 시 주고받는 데이터를 담는 객체
>
> - Request DTO: 서버에 보낼 값
>  
> - Response DTO: 서버에서 받은 값  
> - 필요 시 Raw Response와 화면 표시용 Model을 분리 가능  

#### 예시 코드
```C#
// ===== 잔고 조회 요청 DTO =====
// API 호출 시 필요한 입력값만 보관한다.
// Header는 HTTP
// Body는 JSON (DTO, JsonPropertyName)
public sealed class InquireBalanceRequest
{
    public string AccountNo { get; set; } = string.Empty;
    public string ProductCode { get; set; } = "01";
    public string AfterHoursFlag { get; set; } = "N";
    public string OfflineFlag { get; set; } = "N";
}
```

```C#
// ===== 잔고 조회 응답 DTO =====
// API 응답 중 화면/비즈니스 로직에서 사용할 값들을 구조화한다.
public sealed class InquireBalanceResponse
{
    public string RtCd { get; set; } = string.Empty;
    public string MsgCd { get; set; } = string.Empty;
    public string Msg1 { get; set; } = string.Empty;

    public List<InquireBalanceItem> Items { get; set; } = new();
}

public sealed class InquireBalanceItem
{
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal BuyPrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal ProfitLoss { get; set; }
}
```

## Services — API 호출 로직
### Services/KoreaInvest/{도메인}/
#### 실제 API 호출, URL/헤더/TR ID 구성
> 실제 API 호출 전용 계층
>
> - API 엔드포인트 결정
>  
> - QueryString, Header 생성 
> - TR ID 선택, HttpClient 호출, 응답 파싱
> - 예외 처리 및 로그 처리

```C#
// ===== 잔고 조회 서비스 인터페이스 =====
// Presenter는 구현체가 아니라 인터페이스에 의존해야 테스트와 교체가 쉬워진다.
public interface IBalanceService
{
    Task<InquireBalanceResponse> GetBalanceAsync(InquireBalanceRequest request, CancellationToken cancellationToken = default);
}
```

```C#
// ===== 잔고 조회 실제 서비스 =====
// HttpClient를 사용하여 서버와 통신하고 DTO로 변환한다.
public sealed class BalanceService : IBalanceService
{
    private readonly HttpClient _httpClient;
    private readonly IBalanceHeaderBuilder _headerBuilder;
    private readonly IBalanceUrlBuilder _urlBuilder;
    private readonly IBalanceQueryStringBuilder _queryStringBuilder;

    public BalanceService(
        HttpClient httpClient,
        IBalanceHeaderBuilder headerBuilder,
        IBalanceUrlBuilder urlBuilder,
        IBalanceQueryStringBuilder queryStringBuilder)
    {
        _httpClient = httpClient;
        _headerBuilder = headerBuilder;
        _urlBuilder = urlBuilder;
        _queryStringBuilder = queryStringBuilder;
    }

    public async Task<InquireBalanceResponse> GetBalanceAsync(
        InquireBalanceRequest request,
        CancellationToken cancellationToken = default)
    {
        // ===== URL 생성 =====
        string path = _urlBuilder.Build();
        string queryString = _queryStringBuilder.Build(request);
        string requestUrl = $"{path}?{queryString}";

        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);

        // ===== Header 구성 =====
        foreach (var header in _headerBuilder.Build())
        {
            httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        using HttpResponseMessage response = await _httpClient.SendAsync(httpRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        InquireBalanceResponse? result =
            await response.Content.ReadFromJsonAsync<InquireBalanceResponse>(cancellationToken: cancellationToken);

        return result ?? new InquireBalanceResponse
        {
            RtCd = "1",
            MsgCd = "NULL_RESPONSE",
            Msg1 = "응답 본문이 비어 있습니다."
        };
    }
}
```

```C#
// ===== URL 경로 생성기 =====
// 엔드포인트 경로를 별도 분리해두면 API 경로 변경 시 수정 위치가 명확해진다.
public interface IBalanceUrlBuilder
{
    string Build();
}

public sealed class BalanceUrlBuilder : IBalanceUrlBuilder
{
    public string Build()
    {
        return "/uapi/domestic-stock/v1/trading/inquire-balance";
    }
}
```

```C#
// ===== QueryString 생성기 =====
// 요청 DTO를 URL 파라미터로 변환하는 책임만 가진다.
public interface IBalanceQueryStringBuilder
{
    string Build(InquireBalanceRequest request);
}

public sealed class BalanceQueryStringBuilder : IBalanceQueryStringBuilder
{
    public string Build(InquireBalanceRequest request)
    {
        return string.Join("&", new[]
        {
            $"CANO={Uri.EscapeDataString(request.AccountNo)}",
            $"ACNT_PRDT_CD={Uri.EscapeDataString(request.ProductCode)}",
            $"AFHR_FLPR_YN={Uri.EscapeDataString(request.AfterHoursFlag)}",
            $"OFL_YN={Uri.EscapeDataString(request.OfflineFlag)}"
        });
    }
}
```


## Views — 결과를 표시할 화면
### Features/Views/{Interfaces or Contents}
#### Interfaces: Presenter가 View를 제어하기 위한 인터페이스 정의
#### Contents: 화면(UserControl) 구현
> Presenter가 View를 직접 Form/UserControl 타입으로 다루지 않고
인터페이스를 통해 제어
>
> - 

```C#
// ===== 잔고 화면 인터페이스 =====
// Presenter는 이 인터페이스만 알고, 실제 UserControl 구현은 몰라야 한다.
public interface IBalanceView
{
    event EventHandler SearchRequested;

    string AccountNo { get; }
    string ProductCode { get; }

    void SetLoading(bool isLoading);
    void ShowBalance(IReadOnlyList<InquireBalanceItem> items);
    void ShowMessage(string message);
    void ShowError(string message);
}
```

```C#
public partial class BalancePage : UserControl, IBalanceView
{
    public event EventHandler? SearchRequested;

    public string AccountNo => textBoxAccountNo.Text.Trim();
    public string ProductCode => textBoxProductCode.Text.Trim();

    public BalancePage()
    {
        InitializeComponent();
        WireEvents();
    }

    private void WireEvents()
    {
        // ===== 조회 버튼 클릭 시 Presenter에 이벤트 전달 =====
        buttonSearch.Click += (s, e) => SearchRequested?.Invoke(this, EventArgs.Empty);
    }

    public void SetLoading(bool isLoading)
    {
        buttonSearch.Enabled = !isLoading;
        progressBar.Visible = isLoading;
    }

    public void ShowBalance(IReadOnlyList<InquireBalanceItem> items)
    {
        dataGridViewBalance.DataSource = null;
        dataGridViewBalance.DataSource = items.ToList();
    }

    public void ShowMessage(string message)
    {
        labelStatus.Text = message;
    }

    public void ShowError(string message)
    {
        MessageBox.Show(message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```


## Presenters — 비즈니스 로직
### Features/Presenters/Contents/
#### Contents: View와 Service 연결, 흐름 제어
```C#
// ===== 잔고 화면 Presenter =====
// View 입력을 받아 Service를 호출하고, 결과를 다시 View에 반영한다.
public sealed class BalancePresenter
{
    private readonly IBalanceView _view;
    private readonly IBalanceService _service;

    public BalancePresenter(IBalanceView view, IBalanceService service)
    {
        _view = view;
        _service = service;

        BindEvents();
    }

    private void BindEvents()
    {
        _view.SearchRequested += OnSearchRequested;
    }

    private async void OnSearchRequested(object? sender, EventArgs e)
    {
        try
        {
            // ===== 입력 검증 =====
            if (string.IsNullOrWhiteSpace(_view.AccountNo))
            {
                _view.ShowError("계좌번호를 입력하세요.");
                return;
            }

            _view.SetLoading(true);
            _view.ShowMessage("잔고 조회 중...");

            var request = new InquireBalanceRequest
            {
                AccountNo = _view.AccountNo,
                ProductCode = string.IsNullOrWhiteSpace(_view.ProductCode) ? "01" : _view.ProductCode
            };

            InquireBalanceResponse response = await _service.GetBalanceAsync(request);

            if (response.RtCd != "0")
            {
                _view.ShowError($"조회 실패: {response.Msg1}");
                return;
            }

            _view.ShowBalance(response.Items);
            _view.ShowMessage("잔고 조회 완료");
        }
        catch (Exception ex)
        {
            // ===== 예외는 사용자 메시지와 내부 로그를 구분하는 것이 좋다 =====
            _view.ShowError($"잔고 조회 중 오류가 발생했습니다.\n{ex.Message}");
        }
        finally
        {
            _view.SetLoading(false);
        }
    }
}
```


## 조립 — Program.cs에 등록
```C#
// ===== Services 생성 =====
var xxxService = new KiaXxxService(...);

// ===== Views 생성 =====
contentViews["Xxx"] = new XxxPage(서비스들...);

// ===== Presenters 생성 =====
// (Presenter는 보통 Content View 내부에서 생성하거나, 여기서 주입)
```

```C#
internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // ===== 공통 HttpClient 생성 =====
        // BaseAddress는 API 서버의 공통 주소를 둔다.
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://openapi.example.com")
        };

        // ===== 환경값 준비 =====
        string accessToken = "YOUR_ACCESS_TOKEN";
        string appKey = "YOUR_APP_KEY";
        string appSecret = "YOUR_APP_SECRET";
        bool isMock = true;

        // ===== Builder / Provider 생성 =====
        IBalanceTrIdProvider trIdProvider = new BalanceTrIdProvider(isMock);
        IBalanceHeaderBuilder headerBuilder = new BalanceHeaderBuilder(accessToken, appKey, appSecret, trIdProvider);
        IBalanceUrlBuilder urlBuilder = new BalanceUrlBuilder();
        IBalanceQueryStringBuilder queryStringBuilder = new BalanceQueryStringBuilder();

        // ===== Service 생성 =====
        IBalanceService balanceService = new BalanceService(
            httpClient,
            headerBuilder,
            urlBuilder,
            queryStringBuilder);

        // ===== View 생성 =====
        var balancePage = new BalancePage();

        // ===== Presenter 생성 =====
        // Presenter 생성 시 이벤트 연결까지 함께 수행된다.
        var balancePresenter = new BalancePresenter(balancePage, balanceService);

        // ===== 메인 폼 조립 =====
        var mainForm = new MainForm();
        mainForm.SetContent("Balance", balancePage);

        Application.Run(mainForm);
    }
}
```