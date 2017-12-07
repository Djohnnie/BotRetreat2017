using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Client.Base;
using BotRetreat2017.Client.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.Routes;

namespace BotRetreat2017.Client
{
    public class StatisticsClient : ClientBase, IStatisticsClient
    {
        public StatisticsClient() : base("http://botretreatstatistics.azurewebsites.net") { }

        public Task<List<TeamStatisticDto>> GetTeamStatistics(String teamName, String teamPassword)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(teamName)] = teamName,
                [nameof(teamPassword)] = teamPassword
            };
            return Get<List<TeamStatisticDto>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_STATISTICS_TEAM}", parameters);
        }

        public Task<List<BotStatisticDto>> GetBotStatistics(String teamName, String teamPassword, String arenaName)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(teamName)] = teamName,
                [nameof(teamPassword)] = teamPassword,
                [nameof(arenaName)] = arenaName
            };
            return Get<List<BotStatisticDto>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_STATISTICS_BOT}", parameters);
        }
    }
}