using System;
using System.Threading.Tasks;
using BotRetreat2017.Business.Interfaces;
using BotRetreat2017.Routes;
using BotRetreat2017.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BotRetreat2017.WebApi.Controllers
{
    [Route(RouteConstants.PREFIX)]
   public class StatisticsController : ApiController<IStatisticsLogic>
    {
        public StatisticsController(IStatisticsLogic statisticsLogicLogic) : base(statisticsLogicLogic) { }

        [HttpGet, Route(RouteConstants.GET_STATISTICS_TEAM)]
        public Task<IActionResult> GetTeamStatistics(String teamName, String teamPassword)
        {
            return Ok(l => l.GetTeamStatistics(teamName, teamPassword));
        }

        [HttpGet, Route(RouteConstants.GET_STATISTICS_BOT)]
        public Task<IActionResult> GetBotStatistics(String teamName, String teamPassword, String arenaName)
        {
            return Ok(l => l.GetBotStatistics(teamName, teamPassword, arenaName));
        }
    }
}