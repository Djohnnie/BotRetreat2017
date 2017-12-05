﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat2017.Business.Base;
using BotRetreat2017.Business.Interfaces;
using BotRetreat2017.Mappers.Interfaces;
using BotRetreat2017.Model;
using BotRetreat2017.Contracts;
using BotRetreat2017.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BotRetreat2017.Business
{
    public class ArenaLogic : Logic<IBotRetreatDbContext>, IArenaLogic
    {
        private readonly IMapper<Arena, ArenaDto> _arenaMapper;
        private readonly IMapper<Arena, ArenaListDto> _arenaListMapper;

        public ArenaLogic(IBotRetreatDbContext dbContext, IMapper<Arena, ArenaDto> arenaMapper, IMapper<Arena, ArenaListDto> arenaListMapper) : base(dbContext)
        {
            _arenaMapper = arenaMapper;
            _arenaListMapper = arenaListMapper;
        }

        public async Task<List<ArenaDto>> GetAllArenas()
        {
            return _arenaMapper.Map(await _dbContext.Arenas.ToListAsync());
        }

        public async Task<List<ArenaListDto>> GetArenasList()
        {
            return _arenaListMapper.Map(
                await _dbContext.Arenas.ToListAsync());
        }

        public async Task<List<ArenaDto>> GetAvailableArenas()
        {
            return _arenaMapper.Map(
                await _dbContext.Arenas.Where(x => x.Active && !x.Private).ToListAsync());
        }

        public async Task<ArenaDto> GetTeamArena(String teamName, String teamPassword)
        {
            var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name == teamName && x.Password == teamPassword);
            if (team != null)
            {
                return _arenaMapper.Map(
                    await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == teamName));
            }
            return null;
        }

        public async Task<List<ArenaDto>> GetTeamArenas(String teamName, String teamPassword)
        {
            var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name == teamName && x.Password == teamPassword);
            if (team != null)
            {
                var arenas = _arenaMapper.Map(
                    await _dbContext.Arenas.Where(x => x.Active && (!x.Private || x.Name == teamName)).ToListAsync());
                foreach (var arena in arenas)
                {
                    var lastDeployment = await _dbContext.Deployments.Where(x => x.Arena.Id == arena.Id && x.Team.Id == team.Id)
                            .OrderByDescending(x => x.DeploymentDateTime)
                            .FirstOrDefaultAsync();
                    arena.LastDeploymentDateTime = lastDeployment?.DeploymentDateTime;
                }
                return arenas;
            }
            return null;
        }

        public async Task<ArenaDto> CreateArena(ArenaDto arena)
        {
            arena.Active = true;
            arena.LastRefreshDateTime = DateTime.UtcNow;
            var arenaEntity = _arenaMapper.Map(arena);
            _dbContext.Arenas.Add(arenaEntity);
            await _dbContext.SaveChangesAsync();
            return _arenaMapper.Map(
                await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Id == arenaEntity.Id));
        }

        public async Task<ArenaDto> EditArena(ArenaDto arena)
        {
            _dbContext.Arenas.Attach(_arenaMapper.Map(arena));
            await _dbContext.SaveChangesAsync();
            return _arenaMapper.Map(
                await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Id == arena.Id));
        }

        public async Task RemoveArena(Guid arenaId)
        {
            var existingArena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Id == arenaId);
            if (existingArena != null)
            {
                var bots = await _dbContext.Deployments.Where(x => x.Arena.Id == existingArena.Id).Select(x => x.Bot).ToListAsync();
                var deployments = await _dbContext.Deployments.Where(x => x.Arena.Id == existingArena.Id).ToListAsync();
                deployments.ForEach(deployment => _dbContext.Deployments.Remove(deployment));
                bots.ForEach(bot => _dbContext.Bots.Remove(bot));
                _dbContext.Arenas.Remove(existingArena);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}