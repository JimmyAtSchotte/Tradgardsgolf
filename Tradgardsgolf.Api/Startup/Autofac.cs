using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Infrastructure.Database;

namespace Tradgardsgolf.Api.Startup;

public static class Autofac
{
    public static void ConfigureAutofac(this WebApplicationBuilder builder)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                var assemblies = new[] {
                    Assembly.Load("Tradgardsgolf.Application.Core"),
                    Assembly.Load("Tradgardsgolf.Application.Infrastructure"),
                    Assembly.Load("Tradgardsgolf.Api.RequestHandling"), 
                    Assembly.Load("Tradgardsgolf.Api.ResponseFactory"), 
                };

                RegistrationExtensions.RegisterAssemblyTypes((ContainerBuilder)containerBuilder, assemblies).AsImplementedInterfaces();
                RegistrationExtensions.RegisterGeneric((ContainerBuilder)containerBuilder, typeof(Repository<>)).As(typeof(IRepository<>));
            });
    }
}