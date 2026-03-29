using KisRestAPI.Models.Realtime;

namespace KisRestAPI.Realtime
{
    // =====================================================================
    // ===== 국내주식 장운영정보 빌더 통합 파일 [실시간-049] =====
    // TR_ID: H0STMKO0 (실전 전용, 모의투자 미지원)
    // Validator / Parser를 하나로 모은다.
    //
    // 왜 장운영정보가 자동매매에서 중요한가?
    // - 장 상태(개시·마감·VI·서킷브레이크)에 따라 매매 전략을 변경해야 한다.
    // - VI 발동 종목은 일정 시간 매매가 중단되므로 주문을 보류해야 한다.
    // - 서킷브레이크 발동 시 전체 시장 매매가 중단된다.
    // =====================================================================

    // ===== 종목코드 검증 =====
    internal static class RealtimeMkopValidator
    {
        public static void Validate(string stockCode)
        {
            if (string.IsNullOrWhiteSpace(stockCode))
                throw new ArgumentException("종목코드가 비어 있습니다.", nameof(stockCode));
        }
    }

    // ===== ^로 구분된 장운영정보 데이터를 DTO로 파싱 =====
    internal static class RealtimeMkopParser
    {
        /// <summary>장운영정보 필드 수 (API 문서 기준 11개)</summary>
        public const int FieldCount = 11;

        /// <summary>
        /// 하나의 장운영정보 레코드(^로 구분된 11개 필드)를 RealtimeMkopData로 변환한다.
        /// </summary>
        public static RealtimeMkopData Parse(string[] fields)
        {
            if (fields == null || fields.Length < FieldCount)
            {
                throw new ArgumentException(
                    $"장운영정보 필드 수가 부족합니다. 필요={FieldCount}, 실제={fields?.Length ?? 0}");
            }

            return new RealtimeMkopData
            {
                // ===== 종목 코드 =====
                StockCode           = fields[0],

                // ===== 거래정지 =====
                TrhtYn              = fields[1],
                TrSuspReasCntt      = fields[2],

                // ===== 장운영 구분 =====
                MkopClsCode         = fields[3],
                AntcMkopClsCode     = fields[4],

                // ===== 임의연장 / 배분 =====
                MrktTrtmClsCode     = fields[5],
                DiviAppClsCode      = fields[6],

                // ===== 종목 상태 =====
                IscdStatClsCode     = fields[7],

                // ===== VI 적용 =====
                ViClsCode           = fields[8],
                OvtmViClsCode       = fields[9],

                // ===== 거래소 =====
                ExchClsCode         = fields[10]
            };
        }

        /// <summary>
        /// 복수 건의 장운영정보 데이터를 파싱한다.
        ///
        /// 프레임의 DataCount 값에 따라 ^로 구분된 전체 필드를
        /// FieldCount(11) 단위로 잘라서 각각 파싱한다.
        /// </summary>
        public static List<RealtimeMkopData> ParseMultiple(string rawPayload, int dataCount)
        {
            string[] allFields = rawPayload.Split('^');
            var results = new List<RealtimeMkopData>(dataCount);

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
