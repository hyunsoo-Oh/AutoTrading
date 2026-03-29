using KisRestAPI.Models.Realtime;

namespace KisRestAPI.Realtime
{
    // =====================================================================
    // ===== 국내주식 실시간호가 빌더 통합 파일 [실시간-004] =====
    // TR_ID: H0STASP0 (실전/모의 동일)
    // Validator / Parser를 하나로 모은다.
    // =====================================================================

    // ===== 종목코드 검증 =====
    internal static class RealtimeAspValidator
    {
        public static void Validate(string stockCode)
        {
            if (string.IsNullOrWhiteSpace(stockCode))
                throw new ArgumentException("종목코드가 비어 있습니다.", nameof(stockCode));
        }
    }

    // ===== ^로 구분된 호가 데이터를 DTO로 파싱 =====
    internal static class RealtimeAspParser
    {
        /// <summary>실시간호가 필드 수 (API 문서 기준 59개)</summary>
        public const int FieldCount = 59;

        /// <summary>
        /// 하나의 호가 레코드(^로 구분된 59개 필드)를 RealtimeAspData로 변환한다.
        /// </summary>
        public static RealtimeAspData Parse(string[] fields)
        {
            if (fields == null || fields.Length < FieldCount)
            {
                throw new ArgumentException(
                    $"실시간호가 필드 수가 부족합니다. 필요={FieldCount}, 실제={fields?.Length ?? 0}");
            }

            return new RealtimeAspData
            {
                // ===== 종목 / 시간 기본 =====
                StockCode           = fields[0],
                BsopHour            = fields[1],
                HourClsCode         = fields[2],

                // ===== 매도호가 1~10 =====
                AskP1               = fields[3],
                AskP2               = fields[4],
                AskP3               = fields[5],
                AskP4               = fields[6],
                AskP5               = fields[7],
                AskP6               = fields[8],
                AskP7               = fields[9],
                AskP8               = fields[10],
                AskP9               = fields[11],
                AskP10              = fields[12],

                // ===== 매수호가 1~10 =====
                BidP1               = fields[13],
                BidP2               = fields[14],
                BidP3               = fields[15],
                BidP4               = fields[16],
                BidP5               = fields[17],
                BidP6               = fields[18],
                BidP7               = fields[19],
                BidP8               = fields[20],
                BidP9               = fields[21],
                BidP10              = fields[22],

                // ===== 매도호가 잔량 1~10 =====
                AskPRsqn1           = fields[23],
                AskPRsqn2           = fields[24],
                AskPRsqn3           = fields[25],
                AskPRsqn4           = fields[26],
                AskPRsqn5           = fields[27],
                AskPRsqn6           = fields[28],
                AskPRsqn7           = fields[29],
                AskPRsqn8           = fields[30],
                AskPRsqn9           = fields[31],
                AskPRsqn10          = fields[32],

                // ===== 매수호가 잔량 1~10 =====
                BidPRsqn1           = fields[33],
                BidPRsqn2           = fields[34],
                BidPRsqn3           = fields[35],
                BidPRsqn4           = fields[36],
                BidPRsqn5           = fields[37],
                BidPRsqn6           = fields[38],
                BidPRsqn7           = fields[39],
                BidPRsqn8           = fields[40],
                BidPRsqn9           = fields[41],
                BidPRsqn10          = fields[42],

                // ===== 총 잔량 / 시간외 잔량 =====
                TotalAskPRsqn       = fields[43],
                TotalBidPRsqn       = fields[44],
                OvtmTotalAskPRsqn   = fields[45],
                OvtmTotalBidPRsqn   = fields[46],

                // ===== 예상 체결 =====
                AntcCnpr            = fields[47],
                AntcCnqn            = fields[48],
                AntcVol             = fields[49],
                AntcCntgVrss        = fields[50],
                AntcCntgVrssSign    = fields[51],
                AntcCntgPrdyCtrt    = fields[52],

                // ===== 누적 거래량 =====
                AcmlVol             = fields[53],

                // ===== 잔량 증감 =====
                TotalAskPRsqnIcdc   = fields[54],
                TotalBidPRsqnIcdc   = fields[55],
                OvtmTotalAskPIcdc   = fields[56],
                OvtmTotalBidPIcdc   = fields[57],

                // ===== 매매 구분 =====
                StckDealClsCode     = fields[58]
            };
        }

        /// <summary>
        /// 복수 건의 호가 데이터를 파싱한다.
        ///
        /// 프레임의 DataCount 값에 따라 ^로 구분된 전체 필드를
        /// FieldCount(59) 단위로 잘라서 각각 파싱한다.
        ///
        /// 호가는 일반적으로 1건이지만, 프레임 구조상 복수 건을 지원한다.
        /// </summary>
        public static List<RealtimeAspData> ParseMultiple(string rawPayload, int dataCount)
        {
            string[] allFields = rawPayload.Split('^');
            var results = new List<RealtimeAspData>(dataCount);

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
