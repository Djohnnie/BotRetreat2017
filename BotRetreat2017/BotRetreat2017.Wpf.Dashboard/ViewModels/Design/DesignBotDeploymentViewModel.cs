using BotRetreat2017.Client.Design;
using BotRetreat2017.Wpf.Framework.Services;
using Reactive.EventAggregator;

namespace BotRetreat2017.Wpf.Dashboard.ViewModels.Design
{
    public class DesignBotDeploymentViewModel : BotDeploymentViewModel
    {
        public DesignBotDeploymentViewModel() : base(new DesignArenaClient(), null, null, null, new EventAggregator(), null, null, null, new CacheService())
        {
            CurrentTeam = new DesignTeamClient().GetTeam("De Sjarels", "").Result;
        }
    }
}