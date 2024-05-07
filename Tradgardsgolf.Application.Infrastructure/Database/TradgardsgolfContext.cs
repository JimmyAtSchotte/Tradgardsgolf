using Microsoft.EntityFrameworkCore;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Infrastructure.Database
{
    public class TradgardsgolfContext(DbContextOptions<TradgardsgolfContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Scorecard> Scorecards { get; set; }
        public DbSet<Tournament> Tournament { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(builder =>
            {
                builder.ToContainer("Course");
                builder.HasPartitionKey(x => x.Id);
                builder.HasNoDiscriminator();
            });

            modelBuilder.Entity<Scorecard>(builder =>
            {
                builder.ToContainer("Scorecard");
                builder.HasPartitionKey(x => x.Id);
                builder.HasKey(x => x.Id);
                builder.HasNoDiscriminator();
            });
            
            modelBuilder.Entity<Tournament>(builder =>
            {
                builder.ToContainer("Tournament");
                builder.HasPartitionKey(x => x.Id);
                builder.HasKey(x => x.Id);
                builder.HasNoDiscriminator();
                builder.OwnsMany(x => x.TournamentCourseDates);
            });
            
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
