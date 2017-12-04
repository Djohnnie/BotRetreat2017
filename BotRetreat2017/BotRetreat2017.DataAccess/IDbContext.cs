using System;
using System.Threading;
using System.Threading.Tasks;

namespace BotRetreat2017.DataAccess
{
    public interface IDbContext : IDisposable
    {
        Int32 SaveChanges();

        Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        void InitializeDatabase();
    }
}