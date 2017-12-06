using System;
using System.Threading.Tasks;
using BotRetreat2017.Model;

namespace BotRetreat2017.Business.Interfaces
{
    public interface ILogLogic
    {
        Task LogMessage(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description = null);

        Task LogWarning(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description = null);

        Task LogError(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description = null);

        Task LogTiming(Arena arena, Deployment deployment, Bot bot, String description);

        Task SaveChanges();
    }
}