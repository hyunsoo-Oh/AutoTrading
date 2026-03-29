using KisRestAPI.Models.Realtime;

namespace KisRestAPI.Realtime
{
    // =====================================================================
    // ===== 국내주식 실시간회원사 빌더 통합 파일 [실시간-047] =====
    // TR_ID: H0STMBC0 (실전 전용, 모의투자 미지원)
    // Validator / Parser를 하나로 모은다.
    // =====================================================================

    // ===== 종목코드 검증 =====
    internal static class RealtimeMbcrValidator
    {
        public static void Validate(string stockCode)
        {
            if (string.IsNullOrWhiteSpace(stockCode))
                throw new ArgumentException("종목코드가 비어 있습니다.", nameof(stockCode));
        }
    }

    // ===== ^로 구분된 회원사 데이터를 DTO로 파싱 =====
    internal static class RealtimeMbcrParser
    {
        /// <summary>실시간회원사 필드 수 (API 문서 기준 78개)</summary>
        public const int FieldCount = 78;

        /// <summary>
        /// 하나의 회원사 레코드(^로 구분된 78개 필드)를 RealtimeMbcrData로 변환한다.
        /// </summary>
        public static RealtimeMbcrData Parse(string[] fields)
        {
            if (fields == null || fields.Length < FieldCount)
            {
                throw new ArgumentException(
                    $"실시간회원사 필드 수가 부족합니다. 필요={FieldCount}, 실제={fields?.Length ?? 0}");
            }

            return new RealtimeMbcrData
            {
                // ===== 종목 코드 =====
                StockCode           = fields[0],

                // ===== 매도 회원사명 1~5 (한글) =====
                SelnMbcrName1       = fields[1],
                SelnMbcrName2       = fields[2],
                SelnMbcrName3       = fields[3],
                SelnMbcrName4       = fields[4],
                SelnMbcrName5       = fields[5],

                // ===== 매수 회원사명 1~5 (한글) =====
                ByovMbcrName1       = fields[6],
                ByovMbcrName2       = fields[7],
                ByovMbcrName3       = fields[8],
                ByovMbcrName4       = fields[9],
                ByovMbcrName5       = fields[10],

                // ===== 총 매도 수량 1~5 =====
                TotalSelnQty1       = fields[11],
                TotalSelnQty2       = fields[12],
                TotalSelnQty3       = fields[13],
                TotalSelnQty4       = fields[14],
                TotalSelnQty5       = fields[15],

                // ===== 총 매수 수량 1~5 =====
                TotalShnuQty1       = fields[16],
                TotalShnuQty2       = fields[17],
                TotalShnuQty3       = fields[18],
                TotalShnuQty4       = fields[19],
                TotalShnuQty5       = fields[20],

                // ===== 매도 거래원 구분 1~5 =====
                SelnMbcrGlobYn1     = fields[21],
                SelnMbcrGlobYn2     = fields[22],
                SelnMbcrGlobYn3     = fields[23],
                SelnMbcrGlobYn4     = fields[24],
                SelnMbcrGlobYn5     = fields[25],

                // ===== 매수 거래원 구분 1~5 =====
                ShnuMbcrGlobYn1     = fields[26],
                ShnuMbcrGlobYn2     = fields[27],
                ShnuMbcrGlobYn3     = fields[28],
                ShnuMbcrGlobYn4     = fields[29],
                ShnuMbcrGlobYn5     = fields[30],

                // ===== 매도 거래원 코드 1~5 =====
                SelnMbcrNo1         = fields[31],
                SelnMbcrNo2         = fields[32],
                SelnMbcrNo3         = fields[33],
                SelnMbcrNo4         = fields[34],
                SelnMbcrNo5         = fields[35],

                // ===== 매수 거래원 코드 1~5 =====
                ShnuMbcrNo1         = fields[36],
                ShnuMbcrNo2         = fields[37],
                ShnuMbcrNo3         = fields[38],
                ShnuMbcrNo4         = fields[39],
                ShnuMbcrNo5         = fields[40],

                // ===== 매도 회원사 비중 1~5 =====
                SelnMbcrRlim1       = fields[41],
                SelnMbcrRlim2       = fields[42],
                SelnMbcrRlim3       = fields[43],
                SelnMbcrRlim4       = fields[44],
                SelnMbcrRlim5       = fields[45],

                // ===== 매수 회원사 비중 1~5 =====
                ShnuMbcrRlim1       = fields[46],
                ShnuMbcrRlim2       = fields[47],
                ShnuMbcrRlim3       = fields[48],
                ShnuMbcrRlim4       = fields[49],
                ShnuMbcrRlim5       = fields[50],

                // ===== 매도 수량 증감 1~5 =====
                SelnQtyIcdc1        = fields[51],
                SelnQtyIcdc2        = fields[52],
                SelnQtyIcdc3        = fields[53],
                SelnQtyIcdc4        = fields[54],
                SelnQtyIcdc5        = fields[55],

                // ===== 매수 수량 증감 1~5 =====
                ShnuQtyIcdc1        = fields[56],
                ShnuQtyIcdc2        = fields[57],
                ShnuQtyIcdc3        = fields[58],
                ShnuQtyIcdc4        = fields[59],
                ShnuQtyIcdc5        = fields[60],

                // ===== 외국계 합산 =====
                GlobTotalSelnQty      = fields[61],
                GlobTotalShnuQty      = fields[62],
                GlobTotalSelnQtyIcdc  = fields[63],
                GlobTotalShnuQtyIcdc  = fields[64],
                GlobNtbyQty           = fields[65],
                GlobSelnRlim          = fields[66],
                GlobShnuRlim          = fields[67],

                // ===== 매도 영문 회원사명 1~5 =====
                SelnMbcrEngName1    = fields[68],
                SelnMbcrEngName2    = fields[69],
                SelnMbcrEngName3    = fields[70],
                SelnMbcrEngName4    = fields[71],
                SelnMbcrEngName5    = fields[72],

                // ===== 매수 영문 회원사명 1~5 =====
                ByovMbcrEngName1    = fields[73],
                ByovMbcrEngName2    = fields[74],
                ByovMbcrEngName3    = fields[75],
                ByovMbcrEngName4    = fields[76],
                ByovMbcrEngName5    = fields[77]
            };
        }

        /// <summary>
        /// 복수 건의 회원사 데이터를 파싱한다.
        ///
        /// 프레임의 DataCount 값에 따라 ^로 구분된 전체 필드를
        /// FieldCount(78) 단위로 잘라서 각각 파싱한다.
        /// </summary>
        public static List<RealtimeMbcrData> ParseMultiple(string rawPayload, int dataCount)
        {
            string[] allFields = rawPayload.Split('^');
            var results = new List<RealtimeMbcrData>(dataCount);

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
