using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Client.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.Utilities;

namespace BotRetreat2017.Client.Design
{
    public class DesignStatisticsClient : BaseDesignClient, IStatisticsClient
    {
        public Task<List<TeamStatisticDto>> GetTeamStatistics(String teamName, String teamPassword)
        {
            return Task.FromResult(new List<TeamStatisticDto>
            {
                RandomTeamStatistic("Arena 1", teamName),
                RandomTeamStatistic("Arena 2", teamName),
                RandomTeamStatistic("Arena 3", teamName)
            });
        }

        public Task<List<BotStatisticDto>> GetBotStatistics(String teamName, String teamPassword, String arenaName)
        {
            return Task.FromResult(new List<BotStatisticDto>
            {
                RandomBotStatistic("Bot 1","Arena 1"),
                RandomBotStatistic("Bot 2","Arena 2"),
                RandomBotStatistic("Bot 3","Arena 3")
            });
        }

        private TeamStatisticDto RandomTeamStatistic(String arenaName, String teamName)
        {
            return new TeamStatisticDto
            {
                ArenaName = arenaName,
                TeamName = teamName,
                NumberOfDeployments = RandomInt16(),
                NumberOfLiveBots = RandomInt16(),
                NumberOfDeadBots = RandomInt16(),
                TotalNumberOfKills = RandomInt16(),
                TotalNumberOfDeaths = RandomInt16(),
                TotalPhysicalDamageDone = RandomInt32(),
                TotalStaminaConsumed = RandomInt32(),
                AverageBotLife = RandomTimeSpan()
            };
        }

        private BotStatisticDto RandomBotStatistic(String botName, String arenaName)
        {
            return new BotStatisticDto
            {
                BotName = botName,
                ArenaName = arenaName,
                PhysicalHealth = new HealthDto { Maximum = RandomInt16(), Current = RandomInt16(), Drain = RandomByte() },
                Stamina = new HealthDto { Maximum = RandomInt16(), Current = RandomInt16(), Drain = RandomByte() },
                LastAction = RandomEnum<LastActionDto>(),
                Location = new PositionDto { X = RandomByte(), Y = RandomByte() },
                Orientation = RandomEnum<OrientationDto>(),
                Script = "MoveForward();".Base64Encode(),
                TotalPhysicalDamageDone = RandomInt16(),
                TotalNumberOfKills = RandomByte(),
                BotLife = RandomTimeSpan()
            };
        }
    }
}