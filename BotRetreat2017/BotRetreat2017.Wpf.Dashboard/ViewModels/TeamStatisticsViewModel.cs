using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Security;
using BotRetreat2017.Wpf.Dashboard.Events;
using BotRetreat2017.Wpf.Dashboard.Helpers;
using BotRetreat2017.Wpf.Framework;
using BotRetreat2017.Wpf.Framework.Services.Interfaces;
using Reactive.EventAggregator;
using BotRetreat2017.Contracts;
using BotRetreat2017.Client.Interfaces;

namespace BotRetreat2017.Wpf.Dashboard.ViewModels
{
    public class TeamStatisticsViewModel : ViewModelBase
    {
        #region [ Private Members ]

        private readonly ITeamClient _teamClient;
        private readonly IStatisticsClient _statisticsClient;
        private readonly IEventAggregator _eventAggregator;
        private readonly ICacheService _cacheService;

        private TeamDto _currentTeam;
        private String _teamName;
        private SecureString _password;

        #endregion

        #region [ Public Properties ]

        public TeamDto CurrentTeam
        {
            get { return _currentTeam; }
            set
            {
                _currentTeam = value;
                _eventAggregator.Publish(new CurrentTeamChangedEvent(_currentTeam));
                this.NotifyPropertyChanged(x => x.CurrentTeam);
            }
        }

        public ObservableCollection<TeamStatisticDto> TeamStatistics { get; } = new ObservableCollection<TeamStatisticDto>();

        public String TeamName
        {
            get
            {
                return _teamName;
            }
            set
            {
                _teamName = value;
                AcceptExistingTeamCommand.RaiseCanExecuteChanged();
                CreateNewTeamCommand.RaiseCanExecuteChanged();
            }
        }

        public SecureString TeamPassword
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                AcceptExistingTeamCommand.RaiseCanExecuteChanged();
                CreateNewTeamCommand.RaiseCanExecuteChanged();
            }
        }

        public IRelayCommand AcceptExistingTeamCommand { get; }
        public IRelayCommand CreateNewTeamCommand { get; }

        #endregion

        #region [ Construction ]

        public TeamStatisticsViewModel(ITeamClient teamClient, IStatisticsClient statisticsClient, ITimerService timerService, IEventAggregator eventAggregator, ICacheService cacheService)
        {
            _teamClient = teamClient;
            _statisticsClient = statisticsClient;
            timerService.Start(TimeSpan.FromSeconds(10), OnRefresh);
            _eventAggregator = eventAggregator;
            _cacheService = cacheService;

            Func<Boolean> canExecute = () =>
            {
                var password = new NetworkCredential(string.Empty, TeamPassword).Password;
                return !String.IsNullOrWhiteSpace(TeamName) && !TeamName.Contains(" ") &&
                       !String.IsNullOrWhiteSpace(password) && !password.Contains(" ");
            };

            AcceptExistingTeamCommand = new RelayCommand(OnAcceptExistingTeam, canExecute);
            CreateNewTeamCommand = new RelayCommand(OnCreateNewTeam, canExecute);
        }

        #endregion

        #region [ Command Handlers ]

        protected async void OnAcceptExistingTeam()
        {
            using (new IsBusy(_eventAggregator))
            {
                await ExceptionHandling(async () =>
                {
                    var password = new NetworkCredential(string.Empty, TeamPassword).Password;
                    _cacheService.Store("PSWD", password);
                    CurrentTeam = await _teamClient.GetTeam(TeamName, password);
                });
            }
        }

        private async void OnCreateNewTeam()
        {
            using (new IsBusy(_eventAggregator))
            {
                await ExceptionHandling(async () =>
                {
                    var password = new NetworkCredential(string.Empty, TeamPassword).Password;
                    _cacheService.Store("PSWD", password);
                    var team = new TeamRegistrationDto { Name = TeamName, Password = password };
                    CurrentTeam = await _teamClient.CreateTeam(team);
                });
            }
        }

        private async void OnRefresh()
        {
            if (CurrentTeam != null)
            {
                await IgnoreExceptions(async () =>
                {
                    var teamStatistics = await _statisticsClient.GetTeamStatistics(CurrentTeam.Name, _cacheService.Load<String>("PSWD"));
                    TeamStatistics.Clear();
                    teamStatistics.ForEach(ts => TeamStatistics.Add(ts));
                });
            }
        }

        #endregion

    }
}