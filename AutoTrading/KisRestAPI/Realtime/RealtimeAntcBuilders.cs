using KisRestAPI.Models.Realtime;

namespace KisRestAPI.Realtime
{
    // =====================================================================
    // ===== 국내주식 실시간예상체결 빌더 통합 파일 [실시간-041] =====
    // TR_ID: H0STANC0 (실전 전용, 모의투자 미지원)
    // Validator / Parser를 하나로 모은다.
    // =====================================================================

    // ===== 종목코드 검증 =====
    internal static class RealtimeAntcValidator
    {
        public static void Validate(string stockCode)
        {
            if (string.IsNullOrWhiteSpace(stockCode))
                throw new ArgumentException("종목코드가 비어 있습니다.", nameof(stockCode));
        }
    }

    // ===== ^로 구분된 예상체결 데이터를 DTO로 파싱 =====
    internal static class RealtimeAntcParser
    {
        /// <summary>실시간예상체결 필드 수 (API 문서 기준 45개)</summary>
        public const int FieldCount = 45;

        /// <summary>
        /// 하나의 예상체결 레코드(^로 구분된 45개 필드)를 RealtimeAntcData로 변환한다.
        /// </summary>
        public static RealtimeAntcData Parse(string[] fields)
        {
            if (fields == null || fields.Length < FieldCount)
            {
                throw new ArgumentException(
                    $"실시간예상체결 필드 수가 부족합니다. 필요={FieldCount}, 실제={fields?.Length ?? 0}");
            }

            return new RealtimeAntcData
            {
                // ===== 종목 / 체결 기본 =====
                StockCode            = fields[0],
                CntgHour             = fields[1],
                CurrentPrice         = fields[2],
                PrdyVrssSign         = fields[3],
                PrdyVrss             = fields[4],
                PrdyCtrt             = fields[5],
                WghnAvrgStckPrc      = fields[6],

                // ===== 시가 / 고가 / 저가 =====
                StckOprc             = fields[7],
                StckHgpr             = fields[8],
                StckLwpr             = fields[9],

                // ===== 호가 =====
                AskPrice1            = fields[10],
                BidPrice1            = fields[11],

                // ===== 거래량 / 거래대금 =====
                CntgVol              = fields[12],
                AcmlVol              = fields[13],
                AcmlTrPbmn           = fields[14],

                // ===== 매도/매수 체결 건수 =====
                SellCntgCount        = fields[15],
                BuyCntgCount         = fields[16],
                NetBuyCntgCount      = fields[17],
                Cttr                 = fields[18],
                TotalSellQty         = fields[19],
                TotalBuyQty          = fields[20],

                // ===== 체결 구분 / 매수비율 =====
                CntgClsCode          = fields[21],
                BuyRate              = fields[22],
                PrdyVolVrssRate      = fields[23],

                // ===== 시가/고가/저가 대비 =====
                OprcHour             = fields[24],
                OprcVrssPrprSign     = fields[25],
                OprcVrssPrpr         = fields[26],
                HgprHour             = fields[27],
                HgprVrssPrprSign     = fields[28],
                HgprVrssPrpr         = fields[29],
                LwprHour             = fields[30],
                LwprVrssPrprSign     = fields[31],
                LwprVrssPrpr         = fields[32],

                // ===== 장운영 / 거래정지 =====
                BsopDate             = fields[33],
                NewMkopClsCode       = fields[34],
                TrhtYn               = fields[35],

                // ===== 호가 잔량 =====
                AskQty1              = fields[36],
                BidQty1              = fields[37],
                TotalAskQty          = fields[38],
                TotalBidQty          = fields[39],

                // ===== 기타 =====
                VolTnrt              = fields[40],
                PrdySameHourAcmlVol  = fields[41],
                PrdySameHourAcmlVolRate = fields[42],
                HourClsCode          = fields[43],
                MrktTrtmClsCode      = fields[44]
            };
        }

        /// <summary>
        /// 복수 건의 예상체결 데이터를 파싱한다.
        ///
        /// 프레임의 DataCount 값에 따라 ^로 구분된 전체 필드를
        /// FieldCount(45) 단위로 잘라서 각각 파싱한다.
        /// </summary>
        public static List<RealtimeAntcData> ParseMultiple(string rawPayload, int dataCount)
        {
            string[] allFields = rawPayload.Split('^');
            var results = new List<RealtimeAntcData>(dataCount);

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
