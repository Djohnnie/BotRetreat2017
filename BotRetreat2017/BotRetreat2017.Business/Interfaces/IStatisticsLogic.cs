using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Business.Interfaces
{
    public interface IStatisticsLogic : ILogic
    {
        Task<List<TeamStatisticDto>> GetTeamStatistics(String teamName, String teamPassword);

        Task<List<BotStatisticDto>> GetBotStatistics(String teamName, String teamPassword, String arenaName);
    }
}