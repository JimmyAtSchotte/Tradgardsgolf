using Microsoft.EntityFrameworkCore;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Infrastructure.Database;

public class TradgardsgolfContext(DbContextOptions<TradgardsgolfContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Scorecard> Scorecards { get; set; }
    public DbSet<Tournament> Tournament { get; set; }
    public DbSet<CourseSeason> CourseSeason { get; set; }
    public DbSet<PlayerStatistic> PlayerStatistic { get; set; }

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
            builder.HasPartitionKey(x => x.CourseId);
            builder.HasKey(x => x.Id);
            builder.HasNoDiscriminator();
        });

        modelBuilder.Entity<Tournament>(builder =>
        {
            builder.ToContainer("Tournament");
            builder.HasPartitionKey(x => x.Id);
            builder.HasKey(x => x.Id);
            builder.HasNoDiscriminator();
        });
        
        
        modelBuilder.Entity<CourseSeason>(builder =>
        {
            builder.ToContainer("CourseSeason");
            builder.HasPartitionKey(x => x.CourseId);
            builder.HasKey(x => x.Id);
            builder.HasNoDiscriminator();
        });
        
        modelBuilder.Entity<PlayerStatistic>(builder =>
        {
            builder.ToContainer("PlayerStatistic");
            builder.HasPartitionKey(x => x.CourseId);
            builder.HasKey(x => x.Id);
            builder.HasNoDiscriminator();
        });


        base.OnModelCreating(modelBuilder);
    }
}