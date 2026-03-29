# 주식 자동매매 프로그램

## 📋 각 페이지 상세 내용 정리

### 1. 📊 Dashboard (메인 랜딩 페이지)

- 실시간 잔고 / 총 수익률 (전일 대비 % + 원)   
- 예수금 + 총 평가금액 + 레버리지율
- 보유종목 테이블 (종목명, 수량, 평균단가, 현재가, 손익률, 비중)  
- 핵심 지표 카드 (Sharpe, MDD, Win Rate, Regime 확률)
- 포트폴리오 파이 차트 + 섹터 분포
- 오늘 전략 상태 배너 (“Momentum 전략 ON • Bull Regime 94%”)

### 2. 🌍 Market Regime

- 대세장/하락장 게이지 + HMM 확률 (Bull 87% / Bear 13%)
- 주도 테마 Top 7 + 바닥 테마 Top 7 (카드 형태)
- 200일 SMA, ADX, VIX/KOSPI 변동성 지수
- Regime 전환 히스토리 타임라인
- “전략 자동 전환 ON/OFF” 토글 + 수동 강제 전환 버튼

### 3. 💰 Asset (자산 관리 전용)

- 전체 자산 배분 (현금 / 주식 / ETF / 옵션 비중)
- 종목별 상세 리스트 (정렬: 수익률순, 비중순, 위험도순)
- 상관관계 Heatmap
- 포트폴리오 최적화 버튼 (VaR 계산, 리밸런싱 제안)
- 자산군 별 필터 (코스피 / 코스닥 / 미국 / 테마)

### 4. 📋 Orders (거래할 기업 목록)

- 오늘 매수/매도 후보 테이블 (종목 + 신호강도 + 예상수익 + 이유)
- 필터: Bull 전용 / Bear 전용 / 모든 신호
- 일괄 매수·매도 버튼 + 개별 승인 체크박스
- 관심종목 워치리스트 + 추가 버튼

### 5. ⚙️ Strategy

전략 선택 ComboBox (Momentum / Mean-Reversion / Dual / Custom)
파라미터 설정: 익절 % / 손절 % / 청산 조건 / ATR 배수 / 최대 포지션 비중
익절·손절·전체청산 버튼 (긴급)
전략별 토글 스위치 + 실시간 활성화 상태
“파라미터 저장 후 즉시 적용” 버튼

### 6. 📈 Report

전략별 수익률 테이블 (기간 선택: 1주/1개월/3개월/YTD/전체)
리스크 지표 (MDD, VaR, Sharpe, Sortino, Calmar)
월별/일별 수익 곡선 + 드로다운 차트
Regime별 성과 비교 (Bull 때 vs Bear 때)
Export (Excel / PDF) 버튼

### 7. 🔬 Backtest

기간 선택 + Walk-Forward 옵션
전략 비교 멀티 차트 (내 전략 vs Buy&Hold vs 다른 전략)
파라미터 슬라이더 + “지금 바로 테스트” 버튼
Monte-Carlo 시뮬레이션 + MDD 분포
“이 설정으로 실전 적용” → Strategy 페이지로 보내기 버튼

### 8. 📜 Logging

실시간 거래 로그 (시간 / 종목 / 매수·매도 / 가격 / 수량 / 전략이유)
전략 전환 로그 (“09:15 Bull → Momentum 활성화”)
에러·예외 로그 + “재시도” 버튼
Telegram/Discord 발송 내역
필터 + 검색 + 로그 내보내기

## 대세장 & 하락장
#### **[대세장]**: 최근의 저점에서 20% 이상 상승하는 기간을 대세장
#### **[하락장]**: 시장의 정점에서 20% 이상 하락하는 기간

가격 추세(Price/Trend)로 ‘현재 국면’을 먼저 판정하고,  
거시·펀더멘털(Macro/Fundamental)과 시장심리(Sentiment/Positioning)로 ‘지속 가능성’을 교차검증

### 추세 확인 방법
1. **다우 이론(Dow Theory)**
2. **이동평균선(Moving Average)**: 200일 이동평균선과 골든·데드 크로스
3. **VIX 지수 (변동성 지수)**
4. **풋-콜 비율(Put-Call Ratio)**


## 주식 거래 전략
### RSI < 30 (과매도) AND 주가가 볼린저 밴드 하단 이탈 AND 거래량

극심한 패닉 셀링 구간에서 발생하는 통계적 불균형을 이용해 단기 반등(Short-term Rebound) 수익

#### [구성 (Components)]

- Momentum Filter: 14일 기준 RSI (Relative Strength Index)
- Volatility Filter: 20일 기준 볼린저 밴드 ($K=2$)
- Intensity Filter: 5일 평균 거래량 대비 200% 이상 급증 (Volume Spike)

### 상대적 강도(Relative Strength) AND 자금의 흐름(Money Flow) 추적 상태머신

#### [목적 (Purpose)]

테마별 모멘텀의 가속도($Acceleration$)와 감속도($Deceleration$)를 측정하여,  
자금이 유입되는 초기 단계에 진입하고 유출되는 단계에서 이탈하는 테마 순환매(Theme Rotation) 자동화

#### [구성 (Composition)]

- Universe Manager: 시장의 테마주들을 그룹핑하고 관리(JSON/DB).
- Momentum Scorer: 테마별 상대 강도(RS) 및 자금 유입 강도 계산.
- Rotation Logic: 테마 간 순위 변동을 감지하는 트리거(Trigger).
- Portfolio Rebalancer: 신규 테마 진입 및 기존 테마 청산 실행.



```
> Easy
RSI 전략 : 횡보장에서 강하나 강한 추세에서 취약

이동평균 교차 전략 : 승률은 낮으나 큰 추세에서 고수익

볼린저 밴드 평균 회귀 전략 : 박스권에서 매우 효율적, 손절 라인 필수

순환매 전략 : 특정 테마로 유동성이 쏠렸다가 빠져나가는 유동성 전이 현상

> Difficult
통계적 차익거래 및 페어 트레이딩

팩터 모델 기반 멀티 전략

머신러닝/딥러닝 기반 가격 예측 모델
```