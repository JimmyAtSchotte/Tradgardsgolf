using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Tradgardsgolf.Api.Startup;

public static class Serlog
{
    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, services, logger) =>
        {
            logger.Enrich.FromLogContext();
            logger.WriteTo.Console();
            logger.WriteTo
                .ApplicationInsights(services.GetRequiredService<TelemetryConfiguration>(),
                    TelemetryConverter.Traces);
        });
    }
}