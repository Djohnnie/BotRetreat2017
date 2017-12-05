using System;
using BotRetreat2017.Wpf.Dashboard.Events;
using Reactive.EventAggregator;

namespace BotRetreat2017.Wpf.Dashboard.Helpers
{
    public class IsBusy : IDisposable
    {
        private readonly IEventAggregator _eventAggregator;

        public IsBusy(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Publish(new IsBusyChangedEvent(true));
        }

        public void Dispose()
        {
            _eventAggregator.Publish(new IsBusyChangedEvent(false));
        }
    }
}