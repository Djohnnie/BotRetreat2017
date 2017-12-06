using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BotRetreat2017.Wpf.Dashboard.Events;
using BotRetreat2017.Wpf.Framework;
using BotRetreat2017.Wpf.Framework.Services.Interfaces;
using Reactive.EventAggregator;
using BotRetreat2017.Contracts;
using BotRetreat2017.Client.Interfaces;

namespace BotRetreat2017.Wpf.Dashboard.ViewModels
{
    public class BotStatisticsViewModel : ViewModelBase
    {
        #region [ Private Members ]

        private readonly IArenaClient _arenaClient;
        private readonly IStatisticsClient _statisticsClient;
        private readonly ICacheService _cacheService;

        private TeamDto _currentTeam;
        private List<ArenaDto> _availableArenas;
        private ArenaDto _selectedArena;

        #endregion

        #region [ Public Properties ]

        public TeamDto CurrentTeam
        {
            get
            {
                return _currentTeam;
            }
            set
            {
                _currentTeam = value;
                this.NotifyPropertyChanged(x => x.CurrentTeam);
                FetchAvailableArenas();
            }
        }

        public List<ArenaDto> AvailableArenas
        {
            get { return _availableArenas; }
            set
            {
                _availableArenas = value;
                this.NotifyPropertyChanged(x => x.AvailableArenas);
            }
        }

        public ArenaDto SelectedArena
        {
            get { return _selectedArena; }
            set
            {
                _selectedArena = value;
                this.NotifyPropertyChanged(x => x.SelectedArena);
                OnRefresh();
            }
        }

        public ObservableCollection<BotStatisticDto> BotStatistics { get; } = new ObservableCollection<BotStatisticDto>();

        #endregion

        #region [ Construction ]

        public BotStatisticsViewModel(IArenaClient arenaClient, IStatisticsClient statisticsClient, ITimerService timerService, IEventAggregator eventAggregator, ICacheService cacheService)
        {
            _arenaClient = arenaClient;
            _statisticsClient = statisticsClient;
            _cacheService = cacheService;

            InitializeTimer(timerService);
            SubscribeEvents(eventAggregator);
        }

        #endregion

        #region [ Command Handlers ]

        private async void OnRefresh()
        {
            if (CurrentTeam != null && SelectedArena != null)
            {
                await IgnoreExceptions(async () =>
                {
                    var botStatistics = await _statisticsClient.GetBotStatistics(CurrentTeam.Name, _cacheService.Load<String>("PSWD"), SelectedArena.Name);
                    BotStatistics.Clear();
                    botStatistics.OrderBy(x => x.PhysicalHealth.Current == 0).ThenBy(x => x.BotName).ToList().ForEach(bs => BotStatistics.Add(bs));
                });
            }
        }

        #endregion

        #region [ Helper Methods ]

        private void InitializeTimer(ITimerService timerService)
        {
            timerService.Start(TimeSpan.FromSeconds(2), OnRefresh);
        }

        private void SubscribeEvents(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<CurrentTeamChangedEvent>().Subscribe(payload => CurrentTeam = payload.Team);
        }

        private async void FetchAvailableArenas()
        {
            AvailableArenas = await _arenaClient.GetTeamArenas(CurrentTeam.Name, _cacheService.Load<String>("PSWD"));
            SelectedArena = AvailableArenas.SingleOrDefault(x => x.Name == CurrentTeam.Name);
        }

        #endregion
    }
}