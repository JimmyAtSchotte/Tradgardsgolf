using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetMonsters.Blazor.Geolocation;
using AzureMapsControl.Components;
using AzureMapsControl.Components.Animations;
using AzureMapsControl.Components.Configuration;
using AzureMapsControl.Components.FullScreen;
using AzureMapsControl.Components.Geolocation;
using AzureMapsControl.Components.Indoor;
using AzureMapsControl.Components.Map;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazorise;
using Blazorise.Icons.Material;
using Blazorise.Material;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Polly;
using Polly.Extensions.Http;
using Tradgardsgolf.BlazorWasm.ApiServices;
using Tradgardsgolf.BlazorWasm.Options;
using Tradgardsgolf.Contracts.Settings;

namespace Tradgardsgolf.BlazorWasm;

public class Program
{
    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        
        var backend = builder.Configuration.GetSection("Backend").Get<Backend>();

        if (string.IsNullOrEmpty(backend.Url))
            throw new Exception("Backend url is not configured");

        builder.RootComponents.Add<App>("#app");

        builder.Services
            .AddHttpClient("ApiDispatcher", client => { client.BaseAddress = new Uri(backend.Url); })
            .AddHttpMessageHandler<AppendBearerAuthorizationMessageHandler>()
            .AddPolicyHandler(GetRetryPolicy());

        builder.Services.AddMsalAuthentication(options =>
        {
            builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
            options.ProviderOptions.LoginMode = "redirect";
            options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
            options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
            options.ProviderOptions.DefaultAccessTokenScopes.Add("https://tradgardsgolf.onmicrosoft.com/api/access");
        });

        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddBlazoredModal();
        builder.Services.AddScoped<LocationService>();
        builder.Services.AddScoped<AppendBearerAuthorizationMessageHandler>();
        builder.Services.AddSingleton<IApiDispatcher, ApiDispatcher>();
        builder.Services.AddOptions<Backend>().Bind(builder.Configuration.GetSection("Backend"));

        var azureMapsSubscriptionKey = await GetAzureMapsSubscriptionKey(builder.Services.BuildServiceProvider());
        
        builder.Services.AddAzureMapsControl(configuration =>
        {
            configuration.SubscriptionKey = azureMapsSubscriptionKey;
        });

        builder.Services
            .AddBlazorise(options => options.Immediate = true)
            .AddMaterialProviders()
            .AddMaterialIcons();

        var host = builder.Build();
        

        await host.RunAsync();
    }

    private static async Task<string> GetAzureMapsSubscriptionKey(IServiceProvider provider)
    {
        var dispatcher = provider.GetRequiredService<IApiDispatcher>();
        var mapsKeyResponse = await dispatcher.Dispatch(new AzureMapsSubscriptionKeyCommand());
        var azureMapsSubscriptionKey = mapsKeyResponse.Key;
        return azureMapsSubscriptionKey;
    }
}


