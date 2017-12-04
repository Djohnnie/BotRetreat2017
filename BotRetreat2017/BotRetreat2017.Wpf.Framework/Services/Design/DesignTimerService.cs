using System;
using System.Windows.Threading;
using BotRetreat2017.Wpf.Framework.Services.Interfaces;

namespace BotRetreat2017.Wpf.Framework.Services.Design
{
    public class DesignTimerService : ITimerService
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private Action _action;

        public ITimerToken Start(TimeSpan interval, Action action)
        {
            _action = action;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
            return new DesignTimerToken();
        }

        private void Timer_Tick(Object sender, EventArgs e)
        {
            _timer.Stop();
            _action();
        }

        public void Dispose()
        {
            _timer.Stop();
        }
    }
}