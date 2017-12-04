using System;

namespace BotRetreat2017.Wpf.Framework.Services.Interfaces
{
    public interface ITimerService : IDisposable
    {
        ITimerToken Start(TimeSpan interval, Action action);
    }
}