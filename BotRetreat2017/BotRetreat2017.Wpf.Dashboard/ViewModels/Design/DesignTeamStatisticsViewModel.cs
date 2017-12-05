using System.Security;
using BotRetreat2017.Wpf.Framework.Services.Design;
using Reactive.EventAggregator;
using BotRetreat2017.Client.Design;
using BotRetreat2017.Wpf.Framework.Services;

namespace BotRetreat2017.Wpf.Dashboard.ViewModels.Design
{
    public class DesignTeamStatisticsViewModel : TeamStatisticsViewModel
    {
        public DesignTeamStatisticsViewModel()
            : base(new DesignTeamClient(), new DesignStatisticsClient(), new DesignTimerService(), new EventAggregator(), new CacheService())
        {
            TeamName = "De Sjarels";
            TeamPassword = new SecureString();
            OnAcceptExistingTeam();
        }
    }
}