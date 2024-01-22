using System;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetMonsters.Blazor.Geolocation;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazorise;
using Blazorise.Icons.Material;
using Blazorise.Material;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Tradgardsgolf.BlazorWasm.ApiServices;
using Tradgardsgolf.BlazorWasm.Options;

namespace Tradgardsgolf.BlazorWasm
{
    public class Program
    {
        
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
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
                .AddHttpClient("ApiDispatcher", client =>
                {
                    client.BaseAddress = new Uri(backend.Url);
                })
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
            builder.Services.AddScoped<IApiDispatcher, ApiDispatcher>();
            builder.Services.AddOptions<Backend>().Bind(builder.Configuration.GetSection("Backend"));
            
            builder.Services
                .AddBlazorise( options => options.Immediate = true)
                .AddMaterialProviders()
                .AddMaterialIcons();
            
            var host = builder.Build();

            await host.RunAsync();
        }
    }
}