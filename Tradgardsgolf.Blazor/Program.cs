using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Tradgardsgolf.Core.Services.Course;
using Tradgardsgolf.Core.Services.CreateLogin;
using Tradgardsgolf.Core.Services.Scorecard;

namespace Tradgardsgolf.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                               .Enrich.FromLogContext()
                               .WriteTo.Console()       
                               .CreateLogger();

            try
            {
                Log.Information("Starting up");
                var host = CreateHostBuilder(args).Build();

                host.Run();
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging(logger => logger.AddConsole())
                .UseSerilog(Log.Logger)
                .ConfigureAppConfiguration(config => {
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }   
}
