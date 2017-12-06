using System;
using System.Threading.Tasks;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Business.Interfaces
{
    public interface IGameLogic : ILogic
    {
        Task<GameDto> GetGameForArena(String arenaName);
    }
}