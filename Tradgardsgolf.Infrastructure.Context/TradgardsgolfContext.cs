using Microsoft.EntityFrameworkCore;

namespace Tradgardsgolf.Infrastructure.Context
{
    public class TradgardsgolfContext : DbContext
    {
        public DbSet<Course> Course { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Round> Round { get; set; }
        public DbSet<RoundScore> RoundScore { get; set; }


        public TradgardsgolfContext(DbContextOptions<TradgardsgolfContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         
        }
    }
}
