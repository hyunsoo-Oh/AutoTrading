using KisRestAPI.Models.Realtime;

namespace KisRestAPI.Realtime
{
    // =====================================================================
    // ===== 국내지수 실시간프로그램매매 빌더 통합 파일 [실시간-028] =====
    // TR_ID: H0UPPGM0 (실전 전용, 모의투자 미지원)
    // Validator / Parser를 하나로 모은다.
    //
    // 왜 프로그램매매 데이터가 자동매매에서 중요한가?
    // - 차익/비차익 프로그램매매의 매도·매수 현황은 기관·외국인의 수급 방향을 보여준다.
    // - 프로그램 순매수가 급감하면 시장 하락 압력이 커지므로
    //   매매 전략의 공격성을 줄이거나 방어 모드로 전환할 수 있다.
    //
    // 왜 별도 파일인가?
    // - TR_ID(H0UPPGM0)가 지수체결(H0UPCNT0)·지수예상체결(H0UPANC0)과 다르다.
    // - 88개 필드로 구조가 완전히 다르며, 프로그램매매 고유의 차익/비차익 분류가 있다.
    // =====================================================================

    // ===== 업종구분코드 검증 =====
    internal static class RealtimeIndexPgmValidator
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

    // ===== ^로 구분된 국내지수 실시간프로그램매매 데이터를 DTO로 파싱 =====
    internal static class RealtimeIndexPgmParser
    {
        /// <summary>국내지수 실시간프로그램매매 필드 수 (API 문서 기준 88개)</summary>
        public const int FieldCount = 88;

        /// <summary>
        /// 하나의 국내지수 실시간프로그램매매 레코드(^로 구분된 88개 필드)를 RealtimeIndexPgmData로 변환한다.
        /// </summary>
        public static RealtimeIndexPgmData Parse(string[] fields)
        {
            if (fields == null || fields.Length < FieldCount)
            {
                throw new ArgumentException(
                    $"국내지수 실시간프로그램매매 필드 수가 부족합니다. 필요={FieldCount}, 실제={fields?.Length ?? 0}");
            }

            return new RealtimeIndexPgmData
            {
                // ===== 업종 구분 =====
                BstpClsCode              = fields[0],

                // ===== 시간 =====
                BsopHour                 = fields[1],

                // ===== 차익/비차익 매도·매수 위탁·자기 체결량 =====
                ArbtSelnEntmCnqn         = fields[2],
                ArbtSelnOnslCnqn         = fields[3],
                ArbtShnuEntmCnqn         = fields[4],
                ArbtShnuOnslCnqn         = fields[5],
                NabtSelnEntmCnqn         = fields[6],
                NabtSelnOnslCnqn         = fields[7],
                NabtShnuEntmCnqn         = fields[8],
                NabtShnuOnslCnqn         = fields[9],

                // ===== 차익/비차익 매도·매수 위탁·자기 체결금액 =====
                ArbtSelnEntmCntgAmt      = fields[10],
                ArbtSelnOnslCntgAmt      = fields[11],
                ArbtShnuEntmCntgAmt      = fields[12],
                ArbtShnuOnslCntgAmt      = fields[13],
                NabtSelnEntmCntgAmt      = fields[14],
                NabtSelnOnslCntgAmt      = fields[15],
                NabtShnuEntmCntgAmt      = fields[16],
                NabtShnuOnslCntgAmt      = fields[17],

                // ===== 차익 합계 =====
                ArbtSmtnSelnVol          = fields[18],
                ArbtSmtmSelnVolRate      = fields[19],
                ArbtSmtnSelnTrPbmn       = fields[20],
                ArbtSmtmSelnTrPbmnRate   = fields[21],
                ArbtSmtnShnuVol          = fields[22],
                ArbtSmtmShnuVolRate      = fields[23],
                ArbtSmtnShnuTrPbmn       = fields[24],
                ArbtSmtmShnuTrPbmnRate   = fields[25],
                ArbtSmtnNtbyQty          = fields[26],
                ArbtSmtmNtbyQtyRate      = fields[27],
                ArbtSmtnNtbyTrPbmn       = fields[28],
                ArbtSmtmNtbyTrPbmnRate   = fields[29],

                // ===== 비차익 합계 =====
                NabtSmtnSelnVol          = fields[30],
                NabtSmtmSelnVolRate      = fields[31],
                NabtSmtnSelnTrPbmn       = fields[32],
                NabtSmtmSelnTrPbmnRate   = fields[33],
                NabtSmtnShnuVol          = fields[34],
                NabtSmtmShnuVolRate      = fields[35],
                NabtSmtnShnuTrPbmn       = fields[36],
                NabtSmtmShnuTrPbmnRate   = fields[37],
                NabtSmtnNtbyQty          = fields[38],
                NabtSmtmNtbyQtyRate      = fields[39],
                NabtSmtnNtbyTrPbmn       = fields[40],
                NabtSmtmNtbyTrPbmnRate   = fields[41],

                // ===== 전체 위탁 =====
                WholEntmSelnVol          = fields[42],
                EntmSelnVolRate          = fields[43],
                WholEntmSelnTrPbmn       = fields[44],
                EntmSelnTrPbmnRate       = fields[45],
                WholEntmShnuVol          = fields[46],
                EntmShnuVolRate          = fields[47],
                WholEntmShnuTrPbmn       = fields[48],
                EntmShnuTrPbmnRate       = fields[49],
                WholEntmNtbyQt           = fields[50],
                EntmNtbyQtyRat           = fields[51],
                WholEntmNtbyTrPbmn       = fields[52],
                EntmNtbyTrPbmnRate       = fields[53],

                // ===== 전체 자기 =====
                WholOnslSelnVol          = fields[54],
                OnslSelnVolRate          = fields[55],
                WholOnslSelnTrPbmn       = fields[56],
                OnslSelnTrPbmnRate       = fields[57],
                WholOnslShnuVol          = fields[58],
                OnslShnuVolRate          = fields[59],
                WholOnslShnuTrPbmn       = fields[60],
                OnslShnuTrPbmnRate       = fields[61],
                WholOnslNtbyQty          = fields[62],
                OnslNtbyQtyRate          = fields[63],
                WholOnslNtbyTrPbmn       = fields[64],
                OnslNtbyTrPbmnRate       = fields[65],

                // ===== 총합계 =====
                TotalSelnQty             = fields[66],
                WholSelnVolRate          = fields[67],
                TotalSelnTrPbmn          = fields[68],
                WholSelnTrPbmnRate       = fields[69],
                ShnuCntgSmtn             = fields[70],
                WholShunVolRate          = fields[71],
                TotalShnuTrPbmn          = fields[72],
                WholShunTrPbmnRate       = fields[73],
                WholNtbyQty              = fields[74],
                WholSmtmNtbyQtyRate      = fields[75],
                WholNtbyTrPbmn           = fields[76],
                WholNtbyTrPbmnRate       = fields[77],

                // ===== 차익/비차익 위탁·자기 순매수 =====
                ArbtEntmNtbyQty          = fields[78],
                ArbtEntmNtbyTrPbmn       = fields[79],
                ArbtOnslNtbyQty          = fields[80],
                ArbtOnslNtbyTrPbmn       = fields[81],
                NabtEntmNtbyQty          = fields[82],
                NabtEntmNtbyTrPbmn       = fields[83],
                NabtOnslNtbyQty          = fields[84],
                NabtOnslNtbyTrPbmn       = fields[85],

                // ===== 누적 =====
                AcmlVol                  = fields[86],
                AcmlTrPbmn               = fields[87]
            };
        }

        /// <summary>
        /// 복수 건의 국내지수 실시간프로그램매매 데이터를 파싱한다.
        ///
        /// 프레임의 DataCount 값에 따라 ^로 구분된 전체 필드를
        /// FieldCount(88) 단위로 잘라서 각각 파싱한다.
        /// </summary>
        public static List<RealtimeIndexPgmData> ParseMultiple(string rawPayload, int dataCount)
        {
            string[] allFields = rawPayload.Split('^');
            var results = new List<RealtimeIndexPgmData>(dataCount);

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
