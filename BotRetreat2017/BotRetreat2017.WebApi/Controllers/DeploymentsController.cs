using System;
using System.Threading.Tasks;
using BotRetreat2017.Business.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.Routes;
using BotRetreat2017.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BotRetreat2017.WebApi.Controllers
{
    [Route(RouteConstants.PREFIX)]
    public class DeploymentsController : ApiController<IDeploymentLogic>
    {
        public DeploymentsController(IDeploymentLogic deploymentsLogic) : base(deploymentsLogic) { }

        [HttpPost, Route(RouteConstants.POST_DEPLOYMENT)]
        public Task<IActionResult> Post([FromBody]DeploymentDto deployment)
        {
            return Ok(l => l.Deploy(deployment));
        }

        [HttpGet, Route(RouteConstants.POST_DEPLOYMENT_AVAILABLE)]
        public Task<IActionResult> Get(String teamName, String arenaName)
        {
            return Ok(l => l.Available(teamName, arenaName));
        }
    }
}