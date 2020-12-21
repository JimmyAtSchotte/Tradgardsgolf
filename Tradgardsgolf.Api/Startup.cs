using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Tradgardsgolf.Infrastructure;

namespace Tradgardsgolf.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Tradgardsgolf.Api", Version = "v1"});
            });
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin() 
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            
            
            services.AddLogging();
            
            services.AddDbContext<TradgardsgolfContext>(builder =>
            {
                var connectionString = Environment.GetEnvironmentVariable("DATABASE") ??
                                       throw new NullReferenceException(
                                           "Enviroment varaible for database cannot be null");
                
                builder.UseMySql(connectionString, new MySqlServerVersion(new Version(5, 7, 32)));
            });
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var assemblies = new[] {
                Assembly.Load("Tradgardsgolf.Core"),
                Assembly.Load("Tradgardsgolf.Infrastructure"),
                Assembly.Load("Tradgardsgolf.Services")
            };

            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tradgardsgolf.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}