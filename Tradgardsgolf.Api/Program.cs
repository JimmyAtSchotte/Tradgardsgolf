using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Serilog;
using Tradgardsgolf.Api.Startup;

namespace Tradgardsgolf.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ConfigureSerilog(configuration)
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddConfiguration(configuration);

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(
            jwtBearerOptions => builder.Configuration.Bind("AzureAdB2C", jwtBearerOptions),
            identityOptions => builder.Configuration.Bind("AzureAdB2C", identityOptions));

        builder.Host.UseSerilog(Log.Logger);
        builder.ConfigureAutofac();
        builder.ConfigureSwaggerGen(configuration);
        builder.ConfigureExceptions();

        builder.ConfigureServices(configuration);

        var app = builder.Build();
        app.ConfigureApplicationPipeline(configuration);

        try
        {
            stopwatch.Stop();
            Log.Information("Starting up application. Host was built in {time}", stopwatch.Elapsed);

            await app.SetupDatabase();
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.Information("Shutting down application. Bye!");
            await Log.CloseAndFlushAsync();
        }
    }
}