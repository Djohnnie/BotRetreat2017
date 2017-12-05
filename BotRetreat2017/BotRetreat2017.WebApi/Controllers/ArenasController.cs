﻿using System;
using System.Threading.Tasks;
using BotRetreat2017.Business.Interfaces;
using BotRetreat2017.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using BotRetreat2017.Routes;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.WebApi.Controllers
{
    [Route(RouteConstants.PREFIX)]
    public class ArenasController : ApiController<IArenaLogic>
    {
        public ArenasController(IArenaLogic arenaLogic) : base(arenaLogic) { }

        [HttpGet, Route(RouteConstants.GET_ARENAS)]
        public Task<IActionResult> GetAllArenas()
        {
            return Ok(l => l.GetAllArenas());
        }

        [HttpGet, Route(RouteConstants.GET_ARENAS_LIST)]
        public Task<IActionResult> GetArenasList()
        {
            return Ok(l => l.GetArenasList());
        }

        [HttpGet, Route(RouteConstants.GET_AVAILABLE_ARENAS)]
        public Task<IActionResult> GetAvailableArenas()
        {
            return Ok(l => l.GetAvailableArenas());
        }

        [HttpGet, Route(RouteConstants.GET_TEAM_ARENA)]
        public Task<IActionResult> GetTeamArena(String teamName, String teamPassword)
        {
            return Ok(l => l.GetTeamArena(teamName, teamPassword));
        }

        [HttpGet, Route(RouteConstants.GET_TEAM_ARENAS)]
        public Task<IActionResult> GetTeamArenas(String teamName, String teamPassword)
        {
            return Ok(l => l.GetTeamArenas(teamName, teamPassword));
        }

        [HttpPost, Route(RouteConstants.POST_ARENA)]
        public Task<IActionResult> Post(ArenaDto arena)
        {
            return Ok(l => l.CreateArena(arena));
        }

        [HttpPut, Route(RouteConstants.PUT_ARENA)]
        public Task<IActionResult> Put(ArenaDto arena)
        {
            return Ok(l => l.EditArena(arena));
        }

        [HttpDelete, Route(RouteConstants.DELETE_ARENA)]
        public Task<IActionResult> Delete(Guid id)
        {
            return Ok(l => l.RemoveArena(id));
        }
    }
}