using BotRetreat2017.Client.Unity;
using BotRetreat2017.Wpf.Dashboard.ViewModels;
using BotRetreat2017.Wpf.Framework.Services;
using BotRetreat2017.Wpf.Framework.Services.Interfaces;
using Reactive.EventAggregator;
using Unity;
using Unity.Extension;
using Unity.Lifetime;

namespace BotRetreat2017.Wpf.Dashboard.Unity
{
    public class PresentationContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.AddNewExtension<ClientContainerExtension>();
            Container.RegisterType<ITimerService, TimerService>(new TransientLifetimeManager());
            Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFileExplorerService, FileExplorerService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFileService, FileService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ICacheService, CacheService>(new SingletonLifetimeManager());
            Container.RegisterType<MainViewModel>();
            Container.RegisterType<TeamStatisticsViewModel>();
            Container.RegisterType<BotStatisticsViewModel>();
            Container.RegisterType<BotDeploymentViewModel>();
        }
    }
}