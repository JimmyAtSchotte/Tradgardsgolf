using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001")
            });

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredModal();

            builder.Services.AddScoped<ICourseApiService, CourseApiService>();
            
            await builder.Build().RunAsync();
        }
    }
}