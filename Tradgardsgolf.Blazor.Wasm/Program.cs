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
using Tradgardsgolf.Blazor.Wasm.ApiServices;

namespace Tradgardsgolf.Blazor.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            builder.Services.AddSingleton(services =>
            {
                var configuration = services.GetService<IConfiguration>();

                return new HttpClient
                {
                    BaseAddress = new Uri(configuration.GetValue<string>("API_URL"))
                };
            });

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