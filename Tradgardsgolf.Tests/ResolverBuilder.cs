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
        private readonly Resolver _resolver;
        private readonly List<Action<ContainerBuilder>> _dependencies;
        private bool tradgardsgolfContextRegisterd;
        public ResolverBuilder(Resolver resolver)
        {
            _dependencies = new List<Action<ContainerBuilder>>();
            _resolver = resolver;
        }

        private ResolverBuilder Extend(Action<ContainerBuilder> builder)
        {
            _dependencies.Add(builder);

            return this;
        }
              
        public ContainerBuilder GetContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            _dependencies.ForEach(x => x?.Invoke(containerBuilder));

            return containerBuilder;
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
