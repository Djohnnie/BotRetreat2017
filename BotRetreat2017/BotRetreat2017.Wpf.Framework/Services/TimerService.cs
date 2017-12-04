using System;
using System.Collections.Generic;
using BotRetreat2017.Wpf.Framework.Services.Interfaces;

namespace BotRetreat2017.Wpf.Framework.Services
{
    public class TimerService : ITimerService
    {
        private readonly List<TimerToken> _timerTokens = new List<TimerToken>();

        public ITimerToken Start(TimeSpan interval, Action action)
        {
            var timerToken = new TimerToken(interval, action);
            _timerTokens.Add(timerToken);
            timerToken.Start();
            return timerToken;
        }

        public void Dispose()
        {
            _timerTokens.ForEach(timerToken => timerToken.Stop());
        }
    }
}