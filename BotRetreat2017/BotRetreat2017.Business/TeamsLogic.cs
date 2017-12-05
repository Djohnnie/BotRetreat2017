using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Business.Base;
using BotRetreat2017.Business.Exceptions;
using BotRetreat2017.Business.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.DataAccess;
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

        public async Task<TeamDto> GetTeam(String name, String password)
        {
            var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name == name);
            if (team != null && Crypt.EnhancedVerify(password, team.Password))
            {
                return _teamMapper.Map(team);
            }
            return null;
        }

        public async Task<TeamDto> CreateTeam(TeamRegistrationDto team)
        {
            Team existingTeam = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name.ToUpper() == team.Name.ToUpper());
            if (existingTeam != null)
            {
                throw new BusinessException($"A team with name '{team.Name}' already exists.");
            }
            Arena existingArena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name.ToUpper() == team.Name.ToUpper());
            if (existingArena != null)
            {
                throw new BusinessException($"An arena with name '{team.Name}' already exists.");
            }

            Team teamToCreate = _teamRegistrationMapper.Map(team);
            teamToCreate.Password = Crypt.HashPassword(team.Password, 10, enhancedEntropy: true);
            teamToCreate.PersonalArena = new Arena
            {
                Active = true,
                DeploymentRestriction = TimeSpan.Zero,
                MaximumPoints = 1000,
                Width = 10,
                Height = 10,
                Name = team.Name,
                LastRefreshDateTime = DateTime.UtcNow,
                Private = true
            };
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