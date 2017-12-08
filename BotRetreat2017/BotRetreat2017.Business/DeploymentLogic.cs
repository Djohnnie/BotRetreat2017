using System;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat2017.Business.Base;
using BotRetreat2017.Business.Exceptions;
using BotRetreat2017.Business.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.DataAccess;
using BotRetreat2017.Model;
using Microsoft.EntityFrameworkCore;

namespace BotRetreat2017.Business
{
    public class DeploymentLogic : Logic<IBotRetreatDbContext>, IDeploymentLogic
    {
        private readonly ILogLogic _logLogic;

        public DeploymentLogic(IBotRetreatDbContext dbContext, ILogLogic logLogic) : base(dbContext)
        {
            _logLogic = logLogic;
        }

        public async Task<DeploymentDto> Deploy(DeploymentDto deployment)
        {
            var arena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == deployment.ArenaName);
            if (arena == null) { throw new BusinessException($"Arena '{deployment.ArenaName}' does not exist!"); }

            var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name == deployment.TeamName);
            if (team == null) { throw new BusinessException($"Team '{deployment.TeamName}' does not exist!"); }

            var bot = await _dbContext.Bots.SingleOrDefaultAsync(x => x.Name == deployment.BotName);
            if (bot == null) { throw new BusinessException($"Bot '{deployment.BotName}' does not exist!"); }

            if (bot.MaximumPhysicalHealth < 0 || bot.MaximumStamina < 0)
            {
                throw new BusinessException($"Number of assigned bot points is not valid!");
            }

            var assignedPoints = bot.MaximumStamina + bot.MaximumPhysicalHealth;
            if (assignedPoints > arena.MaximumPoints)
            {
                throw new BusinessException($"Number of assigned bot points ({assignedPoints}) is larger than maximum allowed ({arena.MaximumPoints})!");
            }

            if (bot.Script.Contains("CSharpCompilation"))
            {
                throw new BusinessException($"Script blocked!");
            }

            var lastDeployment = await _dbContext.Deployments
                .Where(x => x.Team.Id == team.Id)
                .OrderByDescending(x => x.DeploymentDateTime)
                .FirstOrDefaultAsync(x => x.Arena.Id == arena.Id);

            if (lastDeployment != null && !team.Predator)
            {
                var timeSinceLastDeployment = DateTime.UtcNow - lastDeployment.DeploymentDateTime;
                if (timeSinceLastDeployment < arena.DeploymentRestriction && !bot.Predator)
                {
                    throw new BusinessException($"Deployment restriction of {arena.DeploymentRestriction} applies!");
                }
            }

            var existingBots = await _dbContext.Deployments.Where(x => x.Arena.Id == arena.Id)
                    .Select(x => x.Bot)
                    .Where(x => x.CurrentPhysicalHealth > 0)
                    .Select(b => new { b.LocationX, b.LocationY }).ToListAsync();
            var randomGenerator = new Random();
            var locationFound = false;
            while (!locationFound)
            {
                bot.LocationX = (Int16)randomGenerator.Next(0, arena.Width);
                bot.LocationY = (Int16)randomGenerator.Next(0, arena.Height);
                bot.Orientation = (Orientation)randomGenerator.Next(0, 4);
                if (!existingBots.Any(l => l.LocationX == bot.LocationX && l.LocationY == bot.LocationY))
                {
                    locationFound = true;
                }
            }

            var deploymentEntity = new Deployment
            {
                Arena = arena,
                Team = team,
                Bot = bot,
                DeploymentDateTime = DateTime.UtcNow
            };
            _dbContext.Deployments.Add(deploymentEntity);
            await _dbContext.SaveChangesAsync();
            await _logLogic.LogMessage(arena, deploymentEntity, bot, HistoryName.BotDeployed);
            return deployment;
        }

        public async Task<Boolean> Available(String teamName, String arenaName)
        {
            var arena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == arenaName);
            var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name == teamName);

            var lastDeployment = await _dbContext.Deployments
                .Where(x => x.Team.Name == teamName)
                .OrderByDescending(x => x.DeploymentDateTime)
                .FirstOrDefaultAsync(x => x.Arena.Name == arenaName);

            if (team.Predator || lastDeployment == null)
            {
                return true;
            }

            var timeSinceLastDeployment = DateTime.UtcNow - lastDeployment.DeploymentDateTime;
            if (timeSinceLastDeployment > arena.DeploymentRestriction)
            {
                return true;
            }

            return false;
        }
    }
}