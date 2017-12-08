using BotRetreat2017.Model;
using Microsoft.EntityFrameworkCore;

namespace BotRetreat2017.DataAccess
{
    public class BotRetreatHistoryDbContext : DbContext, IBotRetreatHistoryDbContext
    {
        public DbSet<History> History { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }

        public void InitializeDatabase()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<History>().ToTable("HISTORY");
            modelBuilder.Entity<History>().HasKey(x => x.Id).ForSqlServerIsClustered(clustered: false);
            modelBuilder.Entity<History>().HasIndex(x => x.SysId).IsUnique().ForSqlServerIsClustered();
            modelBuilder.Entity<History>().Property(x => x.SysId).ValueGeneratedOnAdd();
        }
    }
}