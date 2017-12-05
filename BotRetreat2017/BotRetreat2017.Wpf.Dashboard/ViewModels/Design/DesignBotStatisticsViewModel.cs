using BotRetreat2017.Client.Design;
using BotRetreat2017.Wpf.Framework.Services;
using BotRetreat2017.Wpf.Framework.Services.Design;
using Reactive.EventAggregator;

namespace BotRetreat2017.Wpf.Dashboard.ViewModels.Design
{
    public class DesignBotStatisticsViewModel : BotStatisticsViewModel
    {
        public DesignBotStatisticsViewModel() : base(new DesignArenaClient(), new DesignStatisticsClient(), new DesignTimerService(), new EventAggregator(), new CacheService())
        {
            CurrentTeam = new DesignTeamClient().GetTeam("De Sjarels", "").Result;
            SelectedArena = new DesignArenaClient().GetTeamArena("De Sjarels", "").Result;
        }
    }
}