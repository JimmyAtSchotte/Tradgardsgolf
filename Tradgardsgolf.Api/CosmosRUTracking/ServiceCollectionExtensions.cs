using Microsoft.Extensions.DependencyInjection;

namespace Tradgardsgolf.Api.CosmosRUTracking;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCosmosRuTracking(this IServiceCollection services)
    {
        services.AddSingleton<CosmosRUTracker>();
        services.AddHttpContextAccessor();

        return services;
    }
    
}