using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat2017.Business.Base;
using BotRetreat2017.Business.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.DataAccess;
using BotRetreat2017.Mappers.Interfaces;
using BotRetreat2017.Model;
using BotRetreat2017.Utilities;
using Microsoft.EntityFrameworkCore;
using Crypt = BCrypt.Net.BCrypt;

namespace BotRetreat2017.Business
{
    public class StatisticsLogic : Logic<IBotRetreatDbContext>, IStatisticsLogic
    {
        private readonly IMapper<Team, TeamStatisticDto> _teamMapper;
        private readonly IMapper<Bot, BotStatisticDto> _botMapper;

        public StatisticsLogic(IBotRetreatDbContext dbContext, IMapper<Team, TeamStatisticDto> teamMapper, IMapper<Bot, BotStatisticDto> botMapper) : base(dbContext)
        {
            _teamMapper = teamMapper;
            _botMapper = botMapper;
        }

        public async Task<List<TeamStatisticDto>> GetTeamStatistics(String teamName, String teamPassword)
        {
            using (var sw = new SimpleStopwatch())
            {
                var teamStatistics = new List<TeamStatisticDto>();
                var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name.ToUpper() == teamName.ToUpper());
                if (team == null || !Crypt.EnhancedVerify(teamPassword, team.Password)) return null;
                var arenas = await _dbContext.Arenas.Where(x => x.Active && (!x.Private || x.Name.ToUpper() == teamName.ToUpper())).ToListAsync();
                var arenaIds = arenas.Select(x => x.Id);
                var bots = await _dbContext.Bots.Where(x => x.Deployments.Any(d => arenaIds.Contains(d.ArenaId)))
                    .Include(x => x.Deployments).Include(x => x.Statistics).Include(x => x.PhysicalHealth)
                    .Include(x => x.Stamina).Include(x => x.Location).Include(x => x.LastAttackLocation)
                    .ToListAsync();
                arenas.ForEach(arena =>
                {
                    var bots4Arena = bots.Where(x => x.Deployments.Any(d => d.TeamId == team.Id)).ToList();
                    var teamStatistic = _teamMapper.Map(team);
                    teamStatistic.ArenaId = arena.Id;
                    teamStatistic.ArenaName = arena.Name;
                    teamStatistic.TeamId = team.Id;
                    teamStatistic.TeamName = team.Name;
                    teamStatistic.NumberOfDeployments = arena.Deployments.Count(x => x.Team.Id == team.Id);
                    teamStatistic.NumberOfLiveBots = bots4Arena.Count(x => x.PhysicalHealth.Current > 0);
                    teamStatistic.NumberOfDeadBots = bots4Arena.Count(x => x.PhysicalHealth.Current == 0);
                    var averageBotLife = bots.Where(x => x.Statistics.TimeOfDeath.HasValue)
                        .Select(x => (x.Statistics.TimeOfDeath.Value - x.Statistics.TimeOfBirth).TotalMilliseconds)
                        .AverageOrDefault(Double.MaxValue);
                    teamStatistic.AverageBotLife = averageBotLife == Double.MaxValue
                        ? TimeSpan.MaxValue
                        : TimeSpan.FromMilliseconds(averageBotLife);
                    teamStatistic.TotalNumberOfKills = bots4Arena.Select(x => x.Statistics.Kills).Sum();
                    teamStatistic.TotalNumberOfDeaths = bots4Arena.Count(x => x.PhysicalHealth.Current == 0);
                    teamStatistic.TotalPhysicalDamageDone = bots4Arena.Select(x => x.Statistics.PhysicalDamageDone).Sum();
                    teamStatistic.TotalStaminaConsumed = bots4Arena.Select(x => x.Stamina.Maximum - x.Stamina.Current).Sum();
                    teamStatistics.Add(teamStatistic);
                });
                Debug.WriteLine($"GetTeamStatistics - {sw.ElapsedMilliseconds}ms");
                return teamStatistics;
            }
        }

        public async Task<List<BotStatisticDto>> GetBotStatistics(String teamName, String teamPassword, String arenaName)
        {
            using (var sw = new SimpleStopwatch())
            {
                var botStatistics = new List<BotStatisticDto>();
                var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name.ToUpper() == teamName.ToUpper());
                if (team == null || !Crypt.EnhancedVerify(teamPassword, team.Password)) return null;
                var arena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name.ToUpper() == arenaName.ToUpper());
                if (arena == null) return null;

                var bots = await _dbContext.Bots.Where(x => x.Deployments.Any(d => d.ArenaId == arena.Id))
                    .Include(x => x.Statistics).Include(x => x.PhysicalHealth).Include(x => x.Stamina)
                    .Include(x => x.Location).Include(x => x.LastAttackLocation).ToListAsync();

                bots.ForEach(bot =>
                {
                    var botStatistic = _botMapper.Map(bot);
                    botStatistic.BotId = bot.Id;
                    botStatistic.BotName = bot.Name;
                    botStatistic.ArenaId = arena.Id;
                    botStatistic.ArenaName = arena.Name;
                    botStatistic.TotalPhysicalDamageDone = bot.Statistics.PhysicalDamageDone;
                    botStatistic.TotalNumberOfKills = bot.Statistics.Kills;
                    botStatistic.BotLife = DateTime.UtcNow - bot.Statistics.TimeOfBirth;
                    botStatistics.Add(botStatistic);
                });
                Debug.WriteLine($"GetBotStatistics - {sw.ElapsedMilliseconds}ms");
                return botStatistics;
            }
        }
    }
}