using KisRestAPI.Models.Realtime;

namespace KisRestAPI.Realtime
{
    // =====================================================================
    // ===== 국내주식 실시간체결통보 빌더 통합 파일 [실시간-005] =====
    // TR_ID: H0STCNI0 (실전) / H0STCNI9 (모의)
    // Validator / Parser를 하나로 모은다.
    //
    // 왜 체결통보는 다른 빌더와 검증 방식이 다른가?
    // - 체결통보의 tr_key는 종목코드가 아니라 HTS ID이다.
    // - 종목코드 형식 검증이 아닌 단순 비어있음 여부만 검증한다.
    // =====================================================================

    // ===== HTS ID 검증 =====
    internal static class RealtimeCcnlNotifyValidator
    {
        public static void Validate(string htsId)
        {
            if (string.IsNullOrWhiteSpace(htsId))
                throw new ArgumentException("HTS ID가 비어 있습니다.", nameof(htsId));
        }
    }

    // ===== ^로 구분된 체결통보 데이터를 DTO로 파싱 =====
    internal static class RealtimeCcnlNotifyParser
    {
        /// <summary>실시간체결통보 필드 수 (API 문서 기준 26개)</summary>
        public const int FieldCount = 26;

        /// <summary>
        /// 하나의 체결통보 레코드(^로 구분된 26개 필드)를 RealtimeCcnlNotifyData로 변환한다.
        /// </summary>
        public static RealtimeCcnlNotifyData Parse(string[] fields)
        {
            if (fields == null || fields.Length < FieldCount)
            {
                throw new ArgumentException(
                    $"실시간체결통보 필드 수가 부족합니다. 필요={FieldCount}, 실제={fields?.Length ?? 0}");
            }

            return new RealtimeCcnlNotifyData
            {
                // ===== 고객 / 계좌 =====
                CustId          = fields[0],
                AcntNo          = fields[1],

                // ===== 주문 기본 =====
                OderNo          = fields[2],
                OoderNo         = fields[3],
                SelnByovCls     = fields[4],
                RctfCls         = fields[5],
                OderKind        = fields[6],
                OderCond        = fields[7],

                // ===== 종목 / 체결 =====
                StckShrnIscd    = fields[8],
                CntgQty         = fields[9],
                CntgUnpr        = fields[10],
                StckCntgHour    = fields[11],

                // ===== 상태 구분 =====
                RfusYn          = fields[12],
                CntgYn          = fields[13],
                AcptYn          = fields[14],

                // ===== 지점 / 주문수량 =====
                BrncNo          = fields[15],
                OderQty         = fields[16],
                AcntName        = fields[17],

                // ===== 호가조건 / 거래소 =====
                OrdCondPrc      = fields[18],
                OrdExgGb        = fields[19],

                // ===== 기타 =====
                PopupYn         = fields[20],
                Filler          = fields[21],
                CrdtCls         = fields[22],
                CrdtLoanDate    = fields[23],
                CntgIsnm40      = fields[24],
                OderPrc         = fields[25]
            };
        }

        /// <summary>
        /// 복수 건의 체결통보 데이터를 파싱한다.
        ///
        /// 체결통보는 일반적으로 1건씩 수신되지만,
        /// 프레임 구조상 복수 건을 지원한다.
        /// </summary>
        public static List<RealtimeCcnlNotifyData> ParseMultiple(string rawPayload, int dataCount)
        {
            string[] allFields = rawPayload.Split('^');
            var results = new List<RealtimeCcnlNotifyData>(dataCount);

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
