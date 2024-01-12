using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Tradgardsgolf.Api.RequestHandling;
using Tradgardsgolf.Core.Config;
using Tradgardsgolf.Infrastructure.Database;
using Tradgardsgolf.Infrastructure.Files;

namespace Tradgardsgolf.Api.Startup;

public static class Services
{
    public static void ConfigureServices(this WebApplicationBuilder builder, IConfigurationRoot configuration)
    {
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ImageReferenceFilter>();
        });
            
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "Tradgardsgolf.Api", Version = "v1"});
        });

        builder.Services.AddMediatR(mediatr =>
        {
            mediatr.RegisterServicesFromAssembly(typeof(IRequestHandlingNamespaceMarker).Assembly);
        });
            
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });
            
        builder.Services.AddLogging();
            
        builder.Services.AddOptions<AllowPlayDistance>().Bind(configuration.GetSection("AllowPlayDistance"));
        builder.Services.AddOptions<AzureStorageOptions>().Bind(configuration.GetSection("AzureStorage"));
         
        builder.Services.AddDbContext<TradgardsgolfContext>(builder =>
        {
            var connectionString = configuration.GetConnectionString("Database");

            if (string.IsNullOrEmpty(connectionString))
                builder.UseInMemoryDatabase("Tradgardsgolf");
            else
                builder.UseSqlServer(configuration.GetConnectionString("Database"));
        });
            
        builder.Services.AddApplicationInsightsTelemetry(options =>
        {
            options.ConnectionString = configuration.GetValue<string>("APPLICATIONINSIGHTS_CONNECTION_STRING");
        });
    }
}