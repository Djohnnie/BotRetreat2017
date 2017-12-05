using System;
using Unity;

namespace BotRetreat2017.Wpf.Dashboard.Unity
{
    public class UnityConfiguration
    {
        public static UnityContainer Container { get; } = new Lazy<UnityContainer>(() =>
        {
            var container = new UnityContainer();
            container.AddNewExtension<PresentationContainerExtension>();
            return container;
        }).Value;
    }
}