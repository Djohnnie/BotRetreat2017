using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Client.Base;
using BotRetreat2017.Client.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.Routes;

namespace BotRetreat2017.Client
{
    public class BotClient : ClientBase, IBotClient
    {
        public Task<List<BotDto>> GetAllBots()
        {
            return Get<List<BotDto>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_BOTS}");
        }

        public Task<BotDto> CreateBot(BotDto bot)
        {
            return Post<BotDto, BotDto>(
              $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_BOT}", bot);
        }

        public Task<BotDto> EditBot(BotDto bot)
        {
            return Put<BotDto, BotDto>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.PUT_BOT}", bot);
        }

        public Task RemoveBot(Guid botId)
        {
            return Delete(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.DELETE_BOT}", botId);
        }
    }
}