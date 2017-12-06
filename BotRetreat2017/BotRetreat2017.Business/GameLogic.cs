using System;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat2017.Business.Base;
using BotRetreat2017.Business.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.DataAccess;
using BotRetreat2017.Mappers.Interfaces;
using BotRetreat2017.Model;
using Microsoft.EntityFrameworkCore;

namespace BotRetreat2017.Business
{
    public class GameLogic : Logic<IBotRetreatDbContext>, IGameLogic
    {
        private readonly IMapper<Arena, ArenaDto> _arenaMapper;
        private readonly IMapper<Bot, BotDto> _botMapper;
        private readonly IMapper<History, HistoryDto> _historyMapper;

        public GameLogic(IBotRetreatDbContext dbContext, IMapper<Arena, ArenaDto> arenaMapper, IMapper<Bot, BotDto> botMapper, IMapper<History, HistoryDto> historyMapper) : base(dbContext)
        {
            _arenaMapper = arenaMapper;
            _botMapper = botMapper;
            _historyMapper = historyMapper;
        }

        public async Task<GameDto> GetGameForArena(String arenaName)
        {
            var arena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == arenaName);
            var bots = await _dbContext.Deployments.Where(x => x.Arena.Name == arenaName).Select(x => x.Bot)
                .Include(x => x.Statistics).Include(x => x.PhysicalHealth).Include(x => x.Stamina)
                .Include(x => x.Location).Include(x => x.LastAttackLocation).Include(x => x.Deployments).ThenInclude(x => x.Team).ToListAsync();
            bots = bots.Where(x => !x.Statistics.TimeOfDeath.HasValue || (DateTime.UtcNow - x.Statistics.TimeOfDeath.Value).TotalMinutes < 2).ToList();
            //var history = await _dbContext.History.Where(x => x.Arena.Name == arenaName).OrderByDescending(x => x.DateTime).ToListAsync();
            bots.ForEach(x =>
            {
                x.Script = String.Empty;
                x.Name = $"{x.Name} ({x.Deployments.Single().Team.Name})";
            });
            return new GameDto
            {
                Arena = _arenaMapper.Map(arena),
                Bots = _botMapper.Map(bots),
                //History = _historyMapper.Map(history)
            };
        }
    }
}