using KisRestAPI.Models.Realtime;

namespace KisRestAPI.Realtime
{
    // =====================================================================
    // ===== 국내주식 실시간체결가 빌더 통합 파일 [실시간-003] =====
    // TR_ID: H0STCNT0 (실전/모의 동일)
    // Validator / Parser를 하나로 모은다.
    // =====================================================================

    // ===== 종목코드 검증 =====
    internal static class RealtimeCcnlValidator
    {
        public static void Validate(string stockCode)
        {
            if (string.IsNullOrWhiteSpace(stockCode))
                throw new ArgumentException("종목코드가 비어 있습니다.", nameof(stockCode));
        }
    }

    // ===== ^로 구분된 체결 데이터를 DTO로 파싱 =====
    internal static class RealtimeCcnlParser
    {
        /// <summary>실시간체결가 필드 수 (API 문서 기준 46개)</summary>
        public const int FieldCount = 46;

        /// <summary>
        /// 하나의 체결 레코드(^로 구분된 46개 필드)를 RealtimeCcnlData로 변환한다.
        /// </summary>
        public static RealtimeCcnlData Parse(string[] fields)
        {
            if (fields == null || fields.Length < FieldCount)
            {
                throw new ArgumentException(
                    $"실시간체결가 필드 수가 부족합니다. 필요={FieldCount}, 실제={fields?.Length ?? 0}");
            }

            return new RealtimeCcnlData
            {
                StockCode            = fields[0],
                CntgHour             = fields[1],
                CurrentPrice         = fields[2],
                PrdyVrssSign         = fields[3],
                PrdyVrss             = fields[4],
                PrdyCtrt             = fields[5],
                WghnAvrgStckPrc      = fields[6],
                StckOprc             = fields[7],
                StckHgpr             = fields[8],
                StckLwpr             = fields[9],
                AskPrice1            = fields[10],
                BidPrice1            = fields[11],
                CntgVol              = fields[12],
                AcmlVol              = fields[13],
                AcmlTrPbmn           = fields[14],
                SellCntgCount        = fields[15],
                BuyCntgCount         = fields[16],
                NetBuyCntgCount      = fields[17],
                Cttr                 = fields[18],
                TotalSellQty         = fields[19],
                TotalBuyQty          = fields[20],
                CcldDvsn             = fields[21],
                BuyRate              = fields[22],
                PrdyVolVrssRate      = fields[23],
                OprcHour             = fields[24],
                OprcVrssPrprSign     = fields[25],
                OprcVrssPrpr         = fields[26],
                HgprHour             = fields[27],
                HgprVrssPrprSign     = fields[28],
                HgprVrssPrpr         = fields[29],
                LwprHour             = fields[30],
                LwprVrssPrprSign     = fields[31],
                LwprVrssPrpr         = fields[32],
                BsopDate             = fields[33],
                NewMkopClsCode       = fields[34],
                TrhtYn               = fields[35],
                AskQty1              = fields[36],
                BidQty1              = fields[37],
                TotalAskQty          = fields[38],
                TotalBidQty          = fields[39],
                VolTnrt              = fields[40],
                PrdySameHourAcmlVol  = fields[41],
                PrdySameHourAcmlVolRate = fields[42],
                HourClsCode          = fields[43],
                MrktTrtmClsCode      = fields[44],
                ViStndPrc            = fields[45]
            };
        }

        /// <summary>
        /// 복수 건의 체결 데이터를 파싱한다.
        ///
        /// 프레임의 DataCount 값에 따라 ^로 구분된 전체 필드를
        /// FieldCount(46) 단위로 잘라서 각각 파싱한다.
        ///
        /// ex) 0|H0STCNT0|004|...  →  4건의 체결 데이터 (46×4 = 184개 필드)
        /// </summary>
        public static List<RealtimeCcnlData> ParseMultiple(string rawPayload, int dataCount)
        {
            string[] allFields = rawPayload.Split('^');
            var results = new List<RealtimeCcnlData>(dataCount);

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
