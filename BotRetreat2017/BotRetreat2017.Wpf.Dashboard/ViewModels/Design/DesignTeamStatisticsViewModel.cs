using System.Security;
using BotRetreat2017.Wpf.Framework.Services.Design;
using Reactive.EventAggregator;
using BotRetreat2017.Client.Design;

namespace BotRetreat2017.Wpf.Dashboard.ViewModels.Design
{
    public class DesignTeamStatisticsViewModel : TeamStatisticsViewModel
    {
        public DesignTeamStatisticsViewModel()
            : base(new DesignTeamClient(), new DesignStatisticsClient(), new DesignTimerService(), new EventAggregator())
        {
            TeamName = "De Sjarels";
            TeamPassword = new SecureString();
            OnAcceptExistingTeam();
        }
    }
}