using System;
using System.Windows.Threading;
using BotRetreat2017.Wpf.Framework.Services.Interfaces;

namespace BotRetreat2017.Wpf.Framework.Services
{
    public class TimerToken : ITimerToken
    {
        private readonly DispatcherTimer _timer;

        public TimerToken(TimeSpan interval, Action action)
        {
            _timer = new DispatcherTimer { Interval = interval };
            _timer.Tick += (sender, e) => { action(); };
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}