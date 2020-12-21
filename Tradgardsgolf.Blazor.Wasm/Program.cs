using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Tradgardsgolf.Blazor.Wasm.ServiceAdapters;
using Tradgardsgolf.Blazor.Wasm.State;

namespace Tradgardsgolf.Blazor.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001")
            });

            builder.Services.AddScoped<ICourseServiceAdapter, CourseServiceAdapter>();
            builder.Services.AddScoped<IScorecardServiceAdapter, ScorecardServiceAdapter>();
            builder.Services.AddScoped<IStorage, Storage>();
            builder.Services.AddScoped<ScorecardState>();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            
            await builder.Build().RunAsync();
        }
    }
}