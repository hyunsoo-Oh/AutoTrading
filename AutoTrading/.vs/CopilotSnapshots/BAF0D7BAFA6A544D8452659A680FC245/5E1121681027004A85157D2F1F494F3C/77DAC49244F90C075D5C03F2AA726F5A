using AutoTrading.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Services.KoreaInvest.Common
{
    /// <summary>
    /// 현재 선택된 한국투자증권 서버 환경을 관리하는 서비스 인터페이스
    /// </summary>
    public interface IKiaTradingService
    {
        KiaTradingMode CurrentEnvironment { get; }
        void SetEnvironment(KiaTradingMode environment);
        ApiEndpointSettings GetCurrentSettings();
    }
}
