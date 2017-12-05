using Reactive.EventAggregator;

namespace BotRetreat2017.Wpf.Dashboard.ViewModels.Design
{
    public class DesignMainViewModel : MainViewModel
    {
        public DesignMainViewModel() : base(new EventAggregator()) { }
    }
}