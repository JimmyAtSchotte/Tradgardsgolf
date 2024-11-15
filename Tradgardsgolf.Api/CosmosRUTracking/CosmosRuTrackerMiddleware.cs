using System;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Tradgardsgolf.Api.CosmosRUTracking;

public class CosmosRuTrackerMiddleware
{
    private readonly RequestDelegate _next;
    public CosmosRuTrackerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, CosmosRUTracker ruTrackingService, ILogger<CosmosRuTrackerMiddleware> logger)
    {
        // Execute the next piece of middleware in the pipeline.
        await _next(context);

        try {

            foreach (var ruUsage in ruTrackingService.TotalCharge(context.TraceIdentifier))
                logger.LogInformation("RU Charge for request {uri} [{dateTime}]: {ru} RUs", context.Request.GetUri().ToString(), ruUsage.DateTime.ToString("yyyy-MM-dd HH:mm:ss"), ruUsage.Ru);
        }
        catch (Exception e)
        {
            logger.LogError(e, "{message}", e.Message);
        }
        
    }
}