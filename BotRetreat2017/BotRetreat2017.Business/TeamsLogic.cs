using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Business.Base;
using BotRetreat2017.Business.Exceptions;
using BotRetreat2017.Business.Interfaces;
using BotRetreat2017.DataAccess;
using BotRetreat2017.DataTransferObjects;
using BotRetreat2017.Mappers.Interfaces;
using BotRetreat2017.Model;
using Microsoft.EntityFrameworkCore;
using Crypt = BCrypt.Net.BCrypt;

namespace BotRetreat2017.Business
{
    public class TeamsLogic : Logic<IBotRetreatDbContext>, ITeamsLogic
    {
        private readonly IMapper<Team, TeamDto> _teamMapper;
        private readonly IMapper<Team, TeamRegistrationDto> _teamRegistrationMapper;

        public TeamsLogic(
            IBotRetreatDbContext dbContext,
            IMapper<Team, TeamDto> teamMapper,
            IMapper<Team, TeamRegistrationDto> teamRegistrationMapper) : base(dbContext)
        {
            _teamMapper = teamMapper;
            _teamRegistrationMapper = teamRegistrationMapper;
        }

        public async Task<List<TeamDto>> GetAllTeams()
        {
            List<Team> teams = await _dbContext.Teams.ToListAsync();
            return _teamMapper.Map(teams);
        }

        public Task<TeamDto> GetTeam(String name, String password)
        {
            throw new NotImplementedException();
        }

        public async Task<TeamDto> CreateTeam(TeamRegistrationDto team)
        {
            Team teamToCreate = _teamRegistrationMapper.Map(team);
            teamToCreate.Password = Crypt.HashPassword(team.Password, 10, enhancedEntropy: true);
            await _dbContext.Teams.AddAsync(teamToCreate);
            await _dbContext.SaveChangesAsync();
            return _teamMapper.Map(teamToCreate);
        }

        public async Task<TeamDto> EditTeam(Guid teamId, String password, TeamRegistrationDto team)
        {
            Team teamToUpdate = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Id == teamId);
            if (teamToUpdate == null) { throw new BusinessException(""); }
            if (!Crypt.EnhancedVerify(password, teamToUpdate.Password)) { throw new BusinessException(""); }
            teamToUpdate.Name = team.Name;
            teamToUpdate.Password = Crypt.HashPassword(team.Password, 10, enhancedEntropy: true);
            await _dbContext.SaveChangesAsync();
            return _teamMapper.Map(teamToUpdate);
        }

        public async Task RemoveTeam(Guid teamId, String password)
        {
            Team teamToRemove = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Id == teamId);
            if (teamToRemove == null) { throw new BusinessException(""); }
            if (!Crypt.EnhancedVerify(password, teamToRemove.Password)) { throw new BusinessException(""); }
            _dbContext.Teams.Remove(teamToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }
}