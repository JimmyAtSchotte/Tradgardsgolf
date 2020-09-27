using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tradgardsgolf.Blazor.Data;
using Tradgardsgolf.Infrastructure.Context;

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

            services.AddDbContext<TradgardsgolfContext>(builder =>
            {
                builder.UseInMemoryDatabase("Memory");
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var assemblies = new[] {
                Assembly.Load("Tradgardsgolf.Core.Infrastructure"),
                Assembly.Load("Tradgardsgolf.Core.Services"),
                Assembly.Load("Tradgardsgolf.Core.Types"),
                Assembly.Load("Tradgardsgolf.Infrastructure"),
                Assembly.Load("Tradgardsgolf.Services"),
            };

            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
            builder.Register(context => new ScorecardState(context.Resolve<ProtectedSessionStorage>()));
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
