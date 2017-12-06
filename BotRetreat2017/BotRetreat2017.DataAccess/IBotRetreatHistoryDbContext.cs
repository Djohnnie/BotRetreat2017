using BotRetreat2017.Model;
using Microsoft.EntityFrameworkCore;

namespace BotRetreat2017.DataAccess
{
    public interface IBotRetreatHistoryDbContext : IDbContext
    {
        DbSet<History> History { get; set; }
    }
}