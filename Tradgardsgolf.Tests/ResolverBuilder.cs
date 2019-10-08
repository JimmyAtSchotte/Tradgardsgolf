using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Tests
{
    public class ResolverBuilder
    {
        private readonly List<Action<ContainerBuilder>> _dependencies;
        private bool tradgardsgolfContextRegisterd;
        private readonly string _resolverId;

        public ResolverBuilder()
        {
            _dependencies = new List<Action<ContainerBuilder>>();
            _resolverId = Guid.NewGuid().ToString();
        }

        private ResolverBuilder Extend(Action<ContainerBuilder> builder)
        {
            _dependencies.Add(builder);

            return this;
        }

        private IContainer Container()
        {
            var containerBuilder = new ContainerBuilder();
            _dependencies.ForEach(x => x?.Invoke(containerBuilder));

            return containerBuilder.Build();
        }

        public ContainerBuilder GetContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            _dependencies.ForEach(x => x?.Invoke(containerBuilder));

            return containerBuilder;
        }

   
        public ResolverBuilder UseEntity<T>(Action<T> entiySetup) where T : class
        {
            UseTradgardsgolfContext();

            var db = Container().Resolve<Infrastructure.TradgardsgolfContext>();
            var entity = Activator.CreateInstance<T>();
            entiySetup(entity);

            db.Set<T>().Add(entity);
            db.SaveChanges();

            return this;
        }

        public ResolverBuilder UseEntity<T>(Action<T> entiySetup, out T result) where T : class
        {
            UseTradgardsgolfContext();

            var db = Container().Resolve<Infrastructure.TradgardsgolfContext>();
            var entity = Activator.CreateInstance<T>();
            entiySetup(entity);
            db.Set<T>().Add(entity);
            db.SaveChanges();

            result = entity;

            return this;
        }

        public ResolverBuilder UseMock<T>(Action<Mock<T>> mock) where T : class
        {
            var mockBuilder = new Mock<T>();
            mock(mockBuilder);

            Extend(builder => builder.Register(c => mockBuilder.Object));

            return this;
        }

        public ResolverBuilder UseMock<T>(Action<Mock<T>> mock, out Mock<T> result) where T : class
        {
            var mockBuilder = new Mock<T>();
            mock(mockBuilder);
            result = mockBuilder;

            Extend(builder => builder.Register(c => mockBuilder.Object));           

            return this;
        }

        public ResolverBuilder UseDependencies(Action<ContainerBuilder> builder)
        {
            Extend(builder);

            return this;
        }

        public void UseTradgardsgolfContext()
        {
            if (tradgardsgolfContextRegisterd)
                return;

            Extend(builder => {
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddDbContext<Infrastructure.TradgardsgolfContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
                builder.Populate(serviceCollection);
            });

            tradgardsgolfContextRegisterd = true;
        }

    }
}
