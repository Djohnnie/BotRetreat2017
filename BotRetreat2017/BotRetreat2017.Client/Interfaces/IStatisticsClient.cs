using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Client.Interfaces
{
    public interface IStatisticsClient
    {
        Task<List<TeamStatisticDto>> GetTeamStatistics(String teamName, String teamPassword);

        Task<List<BotStatisticDto>> GetBotStatistics(String teamName, String teamPassword, String arenaName);
    }
}