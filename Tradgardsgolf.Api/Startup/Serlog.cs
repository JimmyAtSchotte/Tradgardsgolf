using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Tradgardsgolf.Api.Startup;

public static class Serlog
{
    public static LoggerConfiguration ConfigureSerilog(this LoggerConfiguration logger,
        IConfigurationRoot configuration)
    {
        logger.MinimumLevel.Information();
        logger.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
        logger.Enrich.FromLogContext();

        logger.WriteTo.Console();

        var instrumentationKey = configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");

        if (!string.IsNullOrEmpty(instrumentationKey))
            logger.WriteTo.ApplicationInsights(new TelemetryConfiguration
            {
                InstrumentationKey = instrumentationKey
            }, TelemetryConverter.Traces);

        return logger;
    }
}