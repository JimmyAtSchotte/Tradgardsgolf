using AspNetMonsters.Blazor.Geolocation;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using Tradgardsgolf.Blazor.State;
using Tradgardsgolf.Infrastructure.Context;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Tradgardsgolf.Blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddProtectedBrowserStorage();

            services.AddLogging();

            services.AddScoped<LocationService>();
            
            
            services.AddDbContext<TradgardsgolfContext>(builder =>
            {
                var connectionString = Environment.GetEnvironmentVariable("DATABASE") ??
                                       throw new NullReferenceException(
                                           "Enviroment varaible for database cannot be null");
                
                builder.UseMySql(connectionString, new MySqlServerVersion(new Version(5, 7, 32)));

                //builder.UseInMemoryDatabase("Memory");
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var assemblies = new[] {
                Assembly.Load("Tradgardsgolf.Core.Infrastructure"),
                Assembly.Load("Tradgardsgolf.Core.Services"),
                Assembly.Load("Tradgardsgolf.Core"),
                Assembly.Load("Tradgardsgolf.Infrastructure"),
                Assembly.Load("Tradgardsgolf.Services"),
                Assembly.Load("Tradgardsgolf.Blazor"),
            };

            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
            builder.Register(context => new ScorecardState(context.Resolve<IStorage>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
