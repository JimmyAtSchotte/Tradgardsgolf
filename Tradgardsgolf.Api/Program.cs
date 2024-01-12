using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tradgardsgolf.Api.Startup;

namespace Tradgardsgolf.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .AddAzureAppConfiguration()
                .Build();

            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddConfiguration(configuration);
            
            builder.ConfigureSerilog();
            builder.ConfigureAutofac();
            builder.ConfigureServices(configuration);

            var app = builder.Build();
            app.ConfigureApplicationPipeline();
            
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            
            try
            {
                stopwatch.Stop();
                logger.LogInformation("Starting up application. Host was built in {time}", stopwatch.Elapsed);
                
                await app.SetupDatabase();
                await app.RunAsync();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Application start-up failed");
            } 
        }
    }
}