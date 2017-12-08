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
    public class BotsController : ApiController<IBotsLogic>
    {
        public BotsController(IBotsLogic botsLogic) : base(botsLogic) { }

        [HttpGet, Route(RouteConstants.GET_BOTS)]
        public Task<IActionResult> GetAllBots()
        {
            return Ok(l => l.GetAllBots());
        }

        [HttpPost, Route(RouteConstants.POST_BOT)]
        public Task<IActionResult> Post([FromBody]BotDto bot)
        {
            return Ok(l => l.CreateBot(bot));
        }
    }
}