using BotRetreat2017.Client.Interfaces;
using Unity;
using Unity.Extension;
using Unity.Lifetime;

namespace BotRetreat2017.Client.Unity
{
    public class ClientContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<ITeamClient, TeamClient>(new PerResolveLifetimeManager());
            Container.RegisterType<IArenaClient, ArenaClient>(new PerResolveLifetimeManager());
            Container.RegisterType<IBotClient, BotClient>(new PerResolveLifetimeManager());
            Container.RegisterType<IDeploymentClient, DeploymentClient>(new PerResolveLifetimeManager());
            Container.RegisterType<IStatisticsClient, StatisticsClient>(new PerResolveLifetimeManager());
            Container.RegisterType<IScriptClient, ScriptClient>(new PerResolveLifetimeManager());
        }
    }
}