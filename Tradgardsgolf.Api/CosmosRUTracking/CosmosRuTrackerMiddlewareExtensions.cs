using Microsoft.AspNetCore.Builder;

namespace Tradgardsgolf.Api.CosmosRUTracking;

public static class CosmosRuTrackerMiddlewareExtensions
{
    public static void ConfigureApplicationPipeline(this WebApplication app)
    {
        app.UseMiddleware<CosmosRuTrackerMiddleware>();
    }
}