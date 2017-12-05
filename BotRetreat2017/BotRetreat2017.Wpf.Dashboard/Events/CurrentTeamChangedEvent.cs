using BotRetreat2017.Contracts;

namespace BotRetreat2017.Wpf.Dashboard.Events
{
    public class CurrentTeamChangedEvent
    {
        public TeamDto Team { get; }

        public CurrentTeamChangedEvent(TeamDto team)
        {
            Team = team;
        }
    }
}