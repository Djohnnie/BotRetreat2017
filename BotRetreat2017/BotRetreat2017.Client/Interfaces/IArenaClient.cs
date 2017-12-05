using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Client.Interfaces
{
    public interface IArenaClient
    {

        Task<List<ArenaDto>> GetAllArenas();

        Task<List<ArenaListDto>> GetArenasList();

        Task<List<ArenaDto>> GetAvailableArenas();

        Task<ArenaDto> GetTeamArena(String teamName, String teamPassword);

        Task<List<ArenaDto>> GetTeamArenas(String teamName, String teamPassword);

        Task<ArenaDto> CreateArena(ArenaDto arena);

        Task<ArenaDto> EditArena(ArenaDto arena);

        Task RemoveArena(Guid arenaId);

    }
}