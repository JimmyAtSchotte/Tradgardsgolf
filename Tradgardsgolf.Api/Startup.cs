using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Tradgardsgolf.Api.RequestHandling;
using Tradgardsgolf.Core.Config;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Infrastructure.Database;
using Tradgardsgolf.Infrastructure.Files;

namespace Tradgardsgolf.Api
{
    public class Startup(IConfiguration configuration)
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Tradgardsgolf.Api", Version = "v1"});
            });

            services.AddMediatR(mediatr =>
            {
                mediatr.RegisterServicesFromAssembly(typeof(IRequestHandlingNamespaceMarker).Assembly);
            });
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            
            services.AddLogging();
            
            services.AddOptions<AllowPlayDistance>().Bind(configuration.GetSection("AllowPlayDistance"));
            services.AddOptions<AzureStorageOptions>().Bind(configuration.GetSection("AzureStorage"));
            
            services.AddDbContext<TradgardsgolfContext>(builder =>
            {
                var connectionString = configuration.GetConnectionString("Database");

                if (string.IsNullOrEmpty(connectionString))
                    builder.UseInMemoryDatabase("Tradgardsgolf");
                else
                    builder.UseSqlServer(configuration.GetConnectionString("Database"));
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var assemblies = new[] {
                Assembly.Load("Tradgardsgolf.Application.Core"),
                Assembly.Load("Tradgardsgolf.Application.Infrastructure"),
                Assembly.Load("Tradgardsgolf.Api.RequestHandling"), 
            };

            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tradgardsgolf.Api v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}