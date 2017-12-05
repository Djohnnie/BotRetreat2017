using System;
using BotRetreat2017.Wpf.Dashboard.Events;
using BotRetreat2017.Wpf.Framework;
using Reactive.EventAggregator;

namespace BotRetreat2017.Wpf.Dashboard.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Boolean _isBusy;

        public Boolean IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                this.NotifyPropertyChanged(x => x.IsBusy);
            }
        }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            SubscribeEvents(eventAggregator);
        }

        #region [ Helper Methods ]

        private void SubscribeEvents(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<IsBusyChangedEvent>().Subscribe(payload => IsBusy = payload.IsBusy);
        }

        #endregion
    }
}