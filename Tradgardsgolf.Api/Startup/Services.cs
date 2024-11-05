using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tradgardsgolf.Api.Authentication;
using Tradgardsgolf.Api.CosmosRUTracking;
using Tradgardsgolf.Api.RequestHandling;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Config;
using Tradgardsgolf.Infrastructure.Database;
using Tradgardsgolf.Infrastructure.Files;

namespace Tradgardsgolf.Api.Startup;

public static class Services
{
    public static void ConfigureServices(this WebApplicationBuilder builder, IConfigurationRoot configuration)
    {
        builder.Services.AddControllers(options => { options.Filters.Add<ImageReferenceFilter>(); });

        builder.Services.AddMediatR(mediatr =>
        {
            mediatr.RegisterServicesFromAssembly(typeof(IRequestHandlingNamespaceMarker).Assembly);
        });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        builder.Services.AddLogging(options => { options.AddConsole(); });
        builder.Services.AddApplicationInsightsTelemetry();

        builder.Services.AddOptions<AllowPlayDistance>().Bind(configuration.GetSection("AllowPlayDistance"));
        builder.Services.AddOptions<AzureMapsSubscriptionKey>().Bind(configuration.GetSection("AzureMapsSubscriptionKey"));
        builder.Services.AddOptions<AzureStorageOptions>().Bind(configuration.GetSection("AzureStorage"));

        builder.Services.AddCosmosRuTracking();

        builder.Services.AddDbContext<TradgardsgolfContext>((services, dbContextOptionsBuilder) =>
        {
            var connectionString = configuration.GetConnectionString("Database");
            dbContextOptionsBuilder.UseCosmos(connectionString, "tradgardsgolf-db");
            dbContextOptionsBuilder.EnableCosmosRuTracking(services);
        });

        builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
    }
}