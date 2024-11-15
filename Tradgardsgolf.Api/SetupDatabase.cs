using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tradgardsgolf.Infrastructure.Database;

namespace Tradgardsgolf.Api;

public static class SetupDatabaseExtensions
{
    private static readonly Random Random = new();

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

            await DataSeeder.Create(context).SeedDataAsync();
            
            return;
        }

        await context.Database.EnsureCreatedAsync();
    }
}