using System;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace Tradgardsgolf.Api.Startup;

public static class AzureAppConfiguration
{
    public static IConfigurationBuilder AddAzureAppConfiguration(this IConfigurationBuilder builder)
    {
        var preConfig = builder.Build();
        var nextBuilder = new ConfigurationBuilder().AddConfiguration(preConfig);
            
        var appConfigUrl = preConfig.GetValue<string>("APP_CONFIG_URL");
        var environment = preConfig.GetValue<string>("ASPNETCORE_ENVIRONMENT");

        if (!string.IsNullOrEmpty(appConfigUrl))
        {
            nextBuilder.AddAzureAppConfiguration(options =>
            {
                options
                    .Connect(new Uri(appConfigUrl), new DefaultAzureCredential())
                    .Select(KeyFilter.Any, LabelFilter.Null)
                    .Select(KeyFilter.Any, environment)
                    .UseFeatureFlags();
            });
        }

        return nextBuilder;
    }
}