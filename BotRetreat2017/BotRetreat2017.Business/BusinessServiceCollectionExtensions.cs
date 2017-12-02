using BotRetreat2017.Business.Interfaces;
using BotRetreat2017.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace BotRetreat2017.Business
{
    public static class BusinessServiceCollectionExtensions
    {
        public static void AddBotRetreatBusiness(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITeamsLogic, TeamsLogic>();
            serviceCollection.AddBotRetreatDataAccess();
        }
    }
}