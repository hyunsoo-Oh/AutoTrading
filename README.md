# 주식 자동매매 프로그램

## 주식 화면 페이지

1. **DashBoard Page**
2. 

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