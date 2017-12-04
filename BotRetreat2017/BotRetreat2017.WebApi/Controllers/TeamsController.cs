﻿using System;
using System.Threading.Tasks;
using BotRetreat2017.Business.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BotRetreat2017.WebApi.Controllers
{
    public class TeamsController : ApiController<ITeamsLogic>
    {
        public TeamsController(ITeamsLogic teamsLogic) : base(teamsLogic) { }

        [HttpGet, Route("teams/all")]
        public Task<IActionResult> GetAllTeams()
        {
            return Ok(l => l.GetAllTeams());
        }

        [HttpGet, Route("teams")]
        public Task<IActionResult> GetTeam(String name, String password)
        {
            return Ok(l => l.GetTeam(name, password));
        }

        [HttpPost, Route("teams")]
        public Task<IActionResult> CreateTeam([FromBody]TeamRegistrationDto team)
        {
            return Ok(l => l.CreateTeam(team));
        }

        [HttpPut, Route("teams/{teamId}")]
        public Task<IActionResult> EditTeam(Guid teamId, String password, [FromBody]TeamRegistrationDto team)
        {
            return Ok(l => l.EditTeam(teamId, password, team));
        }

        [HttpDelete, Route("teams/{teamId}")]
        public Task<IActionResult> RemoveTeam(Guid teamId, String password)
        {
            return Ok(l => l.RemoveTeam(teamId, password));
        }
    }
}