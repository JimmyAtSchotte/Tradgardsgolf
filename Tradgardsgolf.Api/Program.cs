using System;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Infrastructure.Database;

namespace Tradgardsgolf.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()       
                .CreateLogger();

            try
            {
                Log.Information("Starting up");
                var host = CreateHostBuilder(args).Build();

                using var scope = host.Services.CreateScope();
                await using var context = scope.ServiceProvider.GetService<TradgardsgolfContext>();

                if (context.Database.IsInMemory())
                    await SeedData(context);
                
                
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }      
        }

        private static async Task SeedData(TradgardsgolfContext context)
        {
            var jimmy = Player.Create(p => p.Name = "Jimmy");
            var patrik = Player.Create(p => p.Name = "Patrik");
            
            context.Add(jimmy);
            context.Add(patrik);
            
            var kumhof = Course.Create(jimmy, p =>
            {
                p.Holes = 6;
                p.Name = "Kumhof (IN MEMORY)";
                p.Latitude = 59.331181;
                p.Longitude = 18.040736;
            });
            
            var trornehof = Course.Create(patrik, p =>
            {
                p.Holes = 6;
                p.Name = "Törnehof (IN MEMORY)";
            });
            
            context.Add(kumhof);
            context.Add(trornehof);

            for (int r = 0; r < 25; r++)
            {
                var roundKumhof = Round.Create(kumhof);
                var random = new Random();
                
                for (int hole = 1; hole <= kumhof.Holes; hole++)
                {
                    roundKumhof.CreateRoundScore(jimmy, hole, random.Next(1, 6));
                    roundKumhof.CreateRoundScore(patrik, hole, random.Next(1, 6));
                }

                context.Add(roundKumhof);
            }
            
            await context.SaveChangesAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging(logger => logger.AddConsole())
                .UseSerilog(Log.Logger)
                .ConfigureAppConfiguration(config => {
                    config.AddEnvironmentVariables();
                    config.AddCommandLine(args);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}