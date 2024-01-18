using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        if (context.Database.IsInMemory())
        {
            await context.SeedData();
            return;
        }

        await context.Database.MigrateAsync();
    }

    private static async Task SeedData(this TradgardsgolfContext context)
    {
        var jimmy = Player.Create(p => p.Name = "Jimmy");
        var patrik = Player.Create(p => p.Name = "Patrik");
            
        context.Add(jimmy);
        context.Add(patrik);
            
        var kumhof = Course.Create( Guid.Parse("61a351bb-4860-445c-a541-6106a5de40d3"), p =>
        {
            p.Holes = 6;
            p.Name = "Kumhof (IN MEMORY)";
            p.Latitude = 59.331181;
            p.Longitude = 18.040736;
            p.Image = "1_638404748907561795.jpg";
        });
            
        var trornehof = Course.Create(new Guid(), p =>
        {
            p.Holes = 6;
            p.Name = "Törnehof (IN MEMORY)";
        });
        
        var berlin = Course.Create(new Guid(), p =>
        {
            p.Holes = 6;
            p.Name = "Berlin (IN MEMORY)";
            p.Latitude = 52.520007;
            p.Longitude = 13.404954;
        });
            
        context.Add(kumhof);
        context.Add(trornehof);
        context.Add(berlin);

        for (int r = 0; r < 25; r++)
            AddRound(context, kumhof, jimmy, patrik);
            
        await context.SaveChangesAsync();
    }

    private static void AddRound(this TradgardsgolfContext context, Course course, params Player[] players)
    {
        var round = Round.Create(course);

        for (int hole = 1; hole <= course.Holes; hole++)
        {
            foreach (var player in players)
                round.CreateRoundScore(player, hole, GenerateRandomScore());
        }

        context.Add(round);
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