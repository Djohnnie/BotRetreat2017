using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat2017.Business.Base;
using BotRetreat2017.Business.Interfaces;
using BotRetreat2017.DataAccess;
using BotRetreat2017.DataTransferObjects;
using BotRetreat2017.Model;
using Microsoft.EntityFrameworkCore;

namespace BotRetreat2017.Business
{
    public class TeamsLogic : Logic<IBotRetreatDbContext>, ITeamsLogic
    {
        public TeamsLogic(IBotRetreatDbContext dbContext) : base(dbContext) { }

        public async Task<List<TeamDto>> GetAllTeams()
        {
            await _dbContext.InitializeDatabase();
            List<Team> teams = await _dbContext.Teams.ToListAsync();
            return new List<TeamDto>();
        }

        public Task<TeamDto> CreateTeam(TeamRegistrationDto team)
        {
            throw new NotImplementedException();
        }

        public Task<TeamDto> EditTeam(Guid teamId, TeamRegistrationDto team)
        {
            throw new NotImplementedException();
        }

        public Task<TeamDto> GetTeam(String name, String password)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTeam(Guid teamId, String password)
        {
            throw new NotImplementedException();
        }
    }
}