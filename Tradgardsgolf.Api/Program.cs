using System;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Tradgardsgolf.Core.Entities;

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
                await host.SetupDatabase();
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

     


        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging(logger => logger.AddConsole())
                .UseSerilog(Log.Logger)
                .ConfigureAppConfiguration((context, config) => {
                    
                    
                    config.AddEnvironmentVariables();
                    config.AddCommandLine(args);


                    var appConfigUrl = config.Build().GetValue<string>("APP_CONFIG_URL");

                    if (!string.IsNullOrEmpty(appConfigUrl))
                    {
                        config.AddAzureAppConfiguration(options =>
                        {
                            options
                                .Connect(new Uri(appConfigUrl), new DefaultAzureCredential())
                                .Select(KeyFilter.Any, LabelFilter.Null)
                                .Select(KeyFilter.Any, context.HostingEnvironment.EnvironmentName)
                                .UseFeatureFlags();
                        });
                    }
                    
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}