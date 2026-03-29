using KisRestAPI.Models.Realtime;

namespace KisRestAPI.Realtime
{
    // =====================================================================
    // ===== 국내지수 실시간체결 빌더 통합 파일 [실시간-026] =====
    // TR_ID: H0UPCNT0 (실전 전용, 모의투자 미지원)
    // Validator / Parser를 하나로 모은다.
    //
    // 왜 지수 실시간체결이 자동매매에서 중요한가?
    // - KOSPI/KOSDAQ 등 업종지수의 실시간 흐름을 파악하여
    //   시장 전체 방향성에 따라 매매 전략의 공격성을 조절할 수 있다.
    // - 지수 급변 시 전체 포지션 방어 로직을 작동시킬 수 있다.
    //
    // 왜 기존 종목코드 검증과 다른가?
    // - 국내주식 실시간체결가(H0STCNT0)는 6자리 종목코드를 사용하지만,
    //   국내지수(H0UPCNT0)는 업종구분코드(예: "0001"=코스피)를 사용한다.
    // - 업종구분코드는 길이/형식이 종목코드와 다르므로 별도 Validator를 둔다.
    // =====================================================================

    // ===== 업종구분코드 검증 =====
    internal static class RealtimeIndexCcnlValidator
    {
        /// <summary>
        /// 업종구분코드를 검증한다.
        ///
        /// 왜 종목코드 검증과 분리하는가?
        /// - 주식 종목코드는 6자리 숫자이지만, 업종구분코드는 4자리 등 형식이 다르다.
        /// - 빈 값만 차단하고 나머지는 서버에 위임하여 유연하게 처리한다.
        /// </summary>
        public static void Validate(string sectorCode)
        {
            if (string.IsNullOrWhiteSpace(sectorCode))
                throw new ArgumentException("업종구분코드가 비어 있습니다.", nameof(sectorCode));
        }
    }

    // ===== ^로 구분된 국내지수 실시간체결 데이터를 DTO로 파싱 =====
    internal static class RealtimeIndexCcnlParser
    {
        /// <summary>국내지수 실시간체결 필드 수 (API 문서 기준 30개)</summary>
        public const int FieldCount = 30;

        /// <summary>
        /// 하나의 국내지수 실시간체결 레코드(^로 구분된 30개 필드)를 RealtimeIndexCcnlData로 변환한다.
        /// </summary>
        public static RealtimeIndexCcnlData Parse(string[] fields)
        {
            if (fields == null || fields.Length < FieldCount)
            {
                throw new ArgumentException(
                    $"국내지수 실시간체결 필드 수가 부족합니다. 필요={FieldCount}, 실제={fields?.Length ?? 0}");
            }

            return new RealtimeIndexCcnlData
            {
                // ===== 업종 구분 =====
                BstpClsCode          = fields[0],

                // ===== 시간 =====
                BsopHour             = fields[1],

                // ===== 지수 현재가 =====
                PrprNmix             = fields[2],
                PrdyVrssSign         = fields[3],
                BstpNmixPrdyVrss     = fields[4],

                // ===== 거래량 / 거래대금 =====
                AcmlVol              = fields[5],
                AcmlTrPbmn           = fields[6],
                PcasVol              = fields[7],
                PcasTrPbmn           = fields[8],

                // ===== 전일 대비율 =====
                PrdyCtrt             = fields[9],

                // ===== 시가 / 최고가 / 최저가 =====
                OprcNmix             = fields[10],
                NmixHgpr             = fields[11],
                NmixLwpr             = fields[12],

                // ===== 시가 대비 =====
                OprcVrssNmixPrpr     = fields[13],
                OprcVrssNmixSign     = fields[14],

                // ===== 최고가 대비 =====
                HgprVrssNmixPrpr     = fields[15],
                HgprVrssNmixSign     = fields[16],

                // ===== 최저가 대비 =====
                LwprVrssNmixPrpr     = fields[17],
                LwprVrssNmixSign     = fields[18],

                // ===== 전일 종가 대비 비율 =====
                PrdyClprVrssOprcRate = fields[19],
                PrdyClprVrssHgprRate = fields[20],
                PrdyClprVrssLwprRate = fields[21],

                // ===== 종목 수 통계 =====
                UplmIssuCnt          = fields[22],
                AscnIssuCnt          = fields[23],
                StnrIssuCnt          = fields[24],
                DownIssuCnt          = fields[25],
                LslmIssuCnt          = fields[26],

                // ===== 기세 종목 수 =====
                QtqtAscnIssuCnt      = fields[27],
                QtqtDownIssuCnt      = fields[28],

                // ===== TICK 대비 =====
                TickVrss             = fields[29]
            };
        }

        /// <summary>
        /// 복수 건의 국내지수 실시간체결 데이터를 파싱한다.
        ///
        /// 프레임의 DataCount 값에 따라 ^로 구분된 전체 필드를
        /// FieldCount(30) 단위로 잘라서 각각 파싱한다.
        /// </summary>
        public static List<RealtimeIndexCcnlData> ParseMultiple(string rawPayload, int dataCount)
        {
            string[] allFields = rawPayload.Split('^');
            var results = new List<RealtimeIndexCcnlData>(dataCount);

            for (int i = 0; i < dataCount; i++)
            {
                int offset = i * FieldCount;
                if (offset + FieldCount > allFields.Length)
                    break;

                string[] fields = allFields[offset..(offset + FieldCount)];
                results.Add(Parse(fields));
            }

            return results;
        }
    }
}
