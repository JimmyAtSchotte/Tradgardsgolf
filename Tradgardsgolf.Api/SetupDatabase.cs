using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Tradgardsgolf.Infrastructure.Database;

namespace Tradgardsgolf.Api;

public static class SetupDatabaseExtensions
{
    public static async Task SetupDatabase(this WebApplication host)
    {
        using var scope = host.Services.CreateScope();

        await using var context = scope.ServiceProvider.GetService<TradgardsgolfContext>();

        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var seedDatabase = config.GetValue<bool>("SeedDatabase") && host.Environment.IsDevelopment();

        if (seedDatabase)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            Log.Information("Starting the database setup.");
            
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            await DataSeeder.Create(context).SeedDataAsync();
            stopwatch.Stop();  
            
            Log.Information("Setup the database took {time}", stopwatch.Elapsed);
            
            return;
        }

        await context.Database.EnsureCreatedAsync();
    }
}