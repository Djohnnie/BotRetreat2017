using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Client.Interfaces;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Client.Design
{
    public class DesignArenaClient : BaseDesignClient, IArenaClient
    {
        public Task<List<ArenaDto>> GetAllArenas()
        {
            return Task.FromResult(new List<ArenaDto>
            {
                RandomArena("ArenaDto 1"),
                RandomArena("ArenaDto 2"),
                RandomArena("ArenaDto 3")
            });
        }

        public Task<List<ArenaListDto>> GetArenasList()
        {
            return Task.FromResult(new List<ArenaListDto>
            {
                RandomArenaList("ArenaDto 1"),
                RandomArenaList("ArenaDto 2"),
                RandomArenaList("ArenaDto 3")
            });
        }

        public Task<List<ArenaDto>> GetAvailableArenas()
        {
            return Task.FromResult(new List<ArenaDto>
            {
                RandomArena("ArenaDto 1"),
                RandomArena("ArenaDto 2"),
                RandomArena("ArenaDto 3")
            });
        }

        public Task<ArenaDto> GetTeamArena(String teamName, String teamPassword)
        {
            return Task.FromResult(RandomArena(teamName));
        }

        public Task<List<ArenaDto>> GetTeamArenas(String teamName, String teamPassword)
        {
            return Task.FromResult(new List<ArenaDto>
            {
                RandomArena("ArenaDto 1"),
                RandomArena("ArenaDto 2"),
                RandomArena("ArenaDto 3")
            });
        }

        public Task<ArenaDto> CreateArena(ArenaDto arena)
        {
            return Task.FromResult(arena);
        }

        public Task<ArenaDto> EditArena(ArenaDto arena)
        {
            return Task.FromResult(arena);
        }

        public Task RemoveArena(Guid arenaId)
        {
            return Task.CompletedTask;
        }

        private ArenaListDto RandomArenaList(String arenaName)
        {
            return new ArenaListDto
            {
                Name = arenaName
            };
        }

        private ArenaDto RandomArena(String arenaName)
        {
            return new ArenaDto
            {
                Name = arenaName,
                Active = true,
                Width = RandomByte(),
                Height = RandomByte(),
                LastRefreshDateTime = DateTime.UtcNow,
                Private = false,
                DeploymentRestriction = RandomTimeSpan(),
                LastDeploymentDateTime = RandomDateTime(),
                MaximumPoints = RandomInt16(),
            };
        }
    }
}