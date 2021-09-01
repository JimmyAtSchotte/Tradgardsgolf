using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MediatR;
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

            services.AddMediatR(typeof(Startup));
            
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
                builder.UseNpgsql(GetPostgresConnectionString(), options =>
                {
                    options.MigrationsAssembly(typeof(Program).Assembly.GetName().ToString());
                });
                
            });
        }
        
        private string GetPostgresConnectionString() {
            // Get the connection string from the ENV variables
            var connectionUrl = Configuration.GetValue<string>("DATABASE_URL") ??
                                throw new NullReferenceException("Enviroment varaible for database cannot be null");

            // parse the connection string
            var databaseUri = new Uri(connectionUrl);

            var db = databaseUri.LocalPath.TrimStart('/');
            var userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

            var connectionStringBuilder = new StringBuilder();
            connectionStringBuilder.Append($"User ID={userInfo[0]};");
            connectionStringBuilder.Append($"Password={userInfo[1]};");
            connectionStringBuilder.Append($"Host={databaseUri.Host};");
            connectionStringBuilder.Append($"Port={databaseUri.Port};");
            connectionStringBuilder.Append($"Database={db};");

            if (databaseUri.Host != "127.0.0.1")
            {
                connectionStringBuilder.Append("Pooling=true;");
                connectionStringBuilder.Append("SSL Mode=Require;");
                connectionStringBuilder.Append("Trust Server Certificate=True;");
            }

            return connectionStringBuilder.ToString();
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var assemblies = new[] {
                Assembly.Load("Tradgardsgolf.Application.Core"),
                Assembly.Load("Tradgardsgolf.Application.Infrastructure"),
                Assembly.Load("Tradgardsgolf.Application.Services"),
                Assembly.Load("Tradgardsgolf.Api.RequestHandling"), 
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