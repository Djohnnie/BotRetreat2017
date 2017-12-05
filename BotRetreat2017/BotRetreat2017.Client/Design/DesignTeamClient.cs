using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Client.Interfaces;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Client.Design
{
    public class DesignTeamClient : BaseDesignClient, ITeamClient
    {
        public Task<List<TeamDto>> GetAllTeams()
        {
            return Task.FromResult(new List<TeamDto>()
            {
                new TeamDto {Name = "TeamDto 1", Statistics = new List<TeamStatisticDto>
                {
                    RandomTeamStatistic("Arena 1", "TeamDto 1"),
                    RandomTeamStatistic("Arena 2", "TeamDto 1"),
                }},
                new TeamDto {Name = "TeamDto 2", Statistics = new List<TeamStatisticDto>
                {
                    RandomTeamStatistic("Arena 1", "TeamDto 2"),
                    RandomTeamStatistic("Arena 2", "TeamDto 2"),
                }},
                new TeamDto {Name = "TeamDto 3", Statistics = new List<TeamStatisticDto>
                {
                    RandomTeamStatistic("Arena 1", "TeamDto 3"),
                    RandomTeamStatistic("Arena 2", "TeamDto 3"),
                }}
            });
        }

        public Task<TeamDto> GetTeam(String name, String password)
        {
            return Task.FromResult(new TeamDto { Name = name });
        }

        public Task<TeamDto> CreateTeam(TeamRegistrationDto team)
        {
            return Task.FromResult(new TeamDto());
        }

        public Task<TeamDto> EditTeam(TeamRegistrationDto team)
        {
            return Task.FromResult(new TeamDto());
        }

        public Task RemoveTeam(Guid teamId)
        {
            return Task.CompletedTask;
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
    }
}