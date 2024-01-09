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
            var apiUrl = builder.Configuration.GetValue<string>("API_URL");

            if (string.IsNullOrEmpty(apiUrl))
                throw new Exception("API url is not configured");
            
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddSingleton(services => new HttpClient
            {
                BaseAddress = new Uri(apiUrl)
            });

            builder.Services
                .AddHttpClient("ApiDispatcher", client => client.BaseAddress = new Uri(apiUrl))
                .AddPolicyHandler(GetRetryPolicy());

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredModal();
            builder.Services.AddScoped<LocationService>();
            builder.Services.AddScoped<IApiDispatcher, ApiDispatcher>();
            
            builder.Services
                .AddBlazorise( options =>
                {
                    options.Immediate = true;
                } )
                .AddMaterialProviders()
                .AddMaterialIcons();
            
            var host = builder.Build();

            await host.RunAsync();
        }
    }
}