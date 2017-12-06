using System;
using System.Collections.Generic;
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
    public class HistoryLogic : Logic<IBotRetreatHistoryDbContext>, IHistoryLogic
    {
        private readonly IMapper<History, HistoryDto> _historyMapper;

        public HistoryLogic(IBotRetreatHistoryDbContext dbContext, IMapper<History, HistoryDto> historyMapper) : base(dbContext)
        {
            _historyMapper = historyMapper;
        }

        public async Task<List<HistoryDto>> GetHistoryByArenaId(Guid arenaId, DateTime? fromDateTime = null, DateTime? untilDateTime = null)
        {
            var historyQuery = _dbContext.History.Where(x => x.ArenaId == arenaId);
            historyQuery = WhereQuery(historyQuery, fromDateTime, untilDateTime);
            return _historyMapper.Map(await historyQuery.ToListAsync());
        }

        public async Task<List<HistoryDto>> GetHistoryByBotId(Guid botId, DateTime? fromDateTime = null, DateTime? untilDateTime = null)
        {
            var historyQuery = _dbContext.History.Where(x => x.BotId == botId);
            historyQuery = WhereQuery(historyQuery, fromDateTime, untilDateTime);
            return _historyMapper.Map(await historyQuery.ToListAsync());
        }

        private static IQueryable<History> WhereQuery(IQueryable<History> historyQuery, DateTime? fromDateTime, DateTime? untilDateTime)
        {
            if (fromDateTime.HasValue)
            {
                historyQuery = historyQuery.Where(x => x.DateTime >= fromDateTime.Value);
            }
            if (untilDateTime.HasValue)
            {
                historyQuery = historyQuery.Where(x => x.DateTime <= untilDateTime.Value);
            }
            return historyQuery;
        }
    }
}