using Microsoft.Extensions.DependencyInjection;
using System;

namespace Tradgardsgolf.ApiClient
{
    public static class TradgradsgolfApiClientServiceExtension
    {
        public static IServiceCollection AddTradgradsgolfApiClient(this IServiceCollection services, Action<TradgradsgolfApiClientOptions> options)
        {
            services.AddSingleton<TradgradsgolfApiClient, TradgradsgolfApiClient>((provider) => new TradgradsgolfApiClient(options));

            return services;
        }

    }
}
