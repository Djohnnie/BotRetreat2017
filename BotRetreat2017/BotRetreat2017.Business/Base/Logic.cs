using BotRetreat2017.DataAccess;

namespace BotRetreat2017.Business.Base
{
    public abstract class Logic<TContext> where TContext : IDbContext
    {
        protected readonly TContext _dbContext;

        protected Logic(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.InitializeDatabase();
        }
    }
}