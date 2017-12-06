using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Business.Interfaces
{
    public interface IHistoryLogic : ILogic
    {
        Task<List<HistoryDto>> GetHistoryByArenaId(Guid arenaId, DateTime? fromDateTime = null, DateTime? untilDateTime = null);

        Task<List<HistoryDto>> GetHistoryByBotId(Guid botId, DateTime? fromDateTime = null, DateTime? untilDateTime = null);
    }
}