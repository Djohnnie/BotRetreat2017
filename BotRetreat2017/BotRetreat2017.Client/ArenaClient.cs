using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Client.Base;
using BotRetreat2017.Client.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.Routes;

namespace BotRetreat2017.Client
{
    public class ArenaClient : ClientBase, IArenaClient
    {
        public Task<List<ArenaDto>> GetAllArenas()
        {
            return Get<List<ArenaDto>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_ARENAS}");
        }

        public Task<List<ArenaListDto>> GetArenasList()
        {
            return Get<List<ArenaListDto>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_ARENAS_LIST}");
        }

        public Task<List<ArenaDto>> GetAvailableArenas()
        {
            return Get<List<ArenaDto>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_AVAILABLE_ARENAS}");
        }

        public Task<ArenaDto> GetTeamArena(String teamName, String teamPassword)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(teamName)] = teamName,
                [nameof(teamPassword)] = teamPassword
            };
            return Get<ArenaDto>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_TEAM_ARENA}", parameters);
        }

        public Task<List<ArenaDto>> GetTeamArenas(String teamName, String teamPassword)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(teamName)] = teamName,
                [nameof(teamPassword)] = teamPassword
            };
            return Get<List<ArenaDto>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_TEAM_ARENAS}", parameters);
        }

        public Task<ArenaDto> CreateArena(ArenaDto arena)
        {
            return Post<ArenaDto, ArenaDto>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_ARENA}", arena);
        }

        public Task<ArenaDto> EditArena(ArenaDto arena)
        {
            return Put<ArenaDto, ArenaDto>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.PUT_ARENA}", arena);
        }

        public Task RemoveArena(Guid arenaId)
        {
            return Delete(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.DELETE_ARENA}", arenaId);
        }
    }
}