using BotRetreat2017.Contracts;
using BotRetreat2017.Mappers.Interfaces;
using BotRetreat2017.Model;
using Microsoft.Extensions.DependencyInjection;

namespace BotRetreat2017.Mappers
{
    public static class MappersServiceCollectionExtensions
    {
        public static void AddBotRetreatMappers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMapper<Arena, ArenaDto>, ArenaMapper>();
            serviceCollection.AddTransient<IMapper<Arena, ArenaListDto>, ArenaListMapper>();
            serviceCollection.AddTransient<IMapper<Team, TeamDto>, TeamMapper>();
            serviceCollection.AddTransient<IMapper<Team, TeamRegistrationDto>, TeamRegistrationMapper>();
            serviceCollection.AddTransient<IMapper<Bot, BotDto>, BotMapper>();
            serviceCollection.AddTransient<IMapper<Team, TeamStatisticDto>, TeamStatisticMapper>();
            serviceCollection.AddTransient<IMapper<Bot, BotStatisticDto>, BotStatisticMapper>();
            serviceCollection.AddTransient<IMapper<History, HistoryDto>, HistoryMapper>();
        }
    }
}