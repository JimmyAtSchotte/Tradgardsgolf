using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Infrastructure.Database;

namespace Tradgardsgolf.Api;

public static class SetupDatabaseExtensions
{
    private static readonly Random Random = new Random();
        
    public static async Task SetupDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        
        await using var context = scope.ServiceProvider.GetService<TradgardsgolfContext>();
        
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>(); 
        var seedDatabase = config.GetValue<bool>("SeedDatabase");

        if (seedDatabase)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        
            await context.SeedData();
            return;
        }
        
        await context.Database.EnsureCreatedAsync();
    }

    private static async Task SeedData(this TradgardsgolfContext context)
    {
        var kumhof = Course.Create(Guid.Empty, p =>
        {
            p.Holes = 6;
            p.Name = "Kumhof (IN MEMORY)";
            p.Latitude = 59.331181;
            p.Longitude = 18.040736;
        });
            
        var tornehof = Course.Create(Guid.Empty, p =>
        {
            p.Holes = 6;
            p.Name = "Törnehof (IN MEMORY)";
            p.Latitude = 59.5751198688695;
            p.Longitude = 17.120523690349092;
        });
        
        var berlin = Course.Create(Guid.Empty, p =>
        {
            p.Holes = 6;
            p.Name = "Berlin (IN MEMORY)";
            p.Latitude = 52.520007;
            p.Longitude = 13.404954;
        });
            
        context.Add(kumhof);
        context.Add(tornehof);
        context.Add(berlin);

        for (int r = 0; r < 300; r++)
            AddScorecard(context, kumhof);
        
        for (int r = 0; r < 60; r++)
            AddScorecard(context, tornehof);
        
        for (int r = 0; r < 25; r++)
            AddScorecard(context, berlin);

        
        var tournament = new Tournament();
        
        tournament.TournamentCourseDates.Add(new TournamentCourseDate()
        {
            Course = kumhof,
            Date = DateTime.Today
        });
        
        tournament.TournamentCourseDates.Add(new TournamentCourseDate()
        {
            Course = tornehof,
            Date = DateTime.Today
        });

        context.Add(tournament);
            
        await context.SaveChangesAsync();
    }

    private static void AddScorecard(this TradgardsgolfContext context, Course course)
    {
        var scorecard = Scorecard.Create(course);
        var players = new[] { "Jimmy", "Patrik", "Amanda", "Hanna" };
        
        foreach (var player in players)
            scorecard.AddPlayerScores(player,Enumerable.Repeat(0, course.Holes).Select(_ => GenerateRandomScore()).ToArray());
        
        context.Add(scorecard);
    }

    private static int GenerateRandomScore()
    {
        var score = Random.Next(0, 100);

        return score switch
        {
            >= 95 => 1,
            >= 85 => 2,
            >= 50 => 3,
            >= 30 => 4,
            >= 15 => 5,
            _ => 6
        };

    }
}