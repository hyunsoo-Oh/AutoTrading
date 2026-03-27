using AutoTrading.Services.KoreaInvest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Configuration
{
    /// <summary>
    /// appsettings.json의 ApiSettings 섹션 전체를 받는 클래스
    /// </summary>
    public class ApiSettings
    {
        /// <summary>초기 거래 모드 (Mock 또는 Live)</summary>
        public string TradingMode { get; set; } = "Mock";

        /// <summary>
        /// 모의투자용 API 설정 묶음
        /// </summary>
        public ApiEndpointSettings Mock { get; set; } = new();

        /// <summary>
        /// 실전투자용 API 설정 묶음
        /// </summary>
        public ApiEndpointSettings Live { get; set; } = new();

        /// <summary>
        /// 현재 TradingMode 값에 따라 실제 사용할 설정을 반환
        /// </summary>
        public ApiEndpointSettings GetCurrent(KisTradingMode mode)
        {
            return mode == KisTradingMode.Live
                ? Live
                : Mock;
        }
    }
}
