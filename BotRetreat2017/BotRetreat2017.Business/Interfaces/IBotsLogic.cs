using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Business.Interfaces
{
    public interface IBotsLogic : ILogic
    {
        Task<List<BotDto>> GetAllBots();

        Task<BotDto> CreateBot(BotDto bot);

        Task<BotDto> EditBot(BotDto bot);

        Task RemoveBot(Guid botId);
    }
}