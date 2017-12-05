using System;

namespace BotRetreat2017.Wpf.Dashboard.Events
{
    public class IsBusyChangedEvent
    {
        public Boolean IsBusy { get; set; }

        public IsBusyChangedEvent(Boolean isBusy)
        {
            IsBusy = isBusy;
        }
    }
}