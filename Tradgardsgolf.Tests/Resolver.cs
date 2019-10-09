using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tradgardsgolf.Tests
{
    public class Resolver
    {
        private readonly Action<ResolverBuilder> _config;
        private IContainer _container;

        public bool IsTradgardsgolfContextRegisterd { get; set; }

        public Resolver(Action<ResolverBuilder> config = null)
        {
            _config = config;
            IsTradgardsgolfContextRegisterd = false;
        }        

        private IContainer GetContainer()
        {
            if (_container == null)
            {
                var resolverBuilder = new ResolverBuilder(this);
                _config?.Invoke(resolverBuilder);
                _container = resolverBuilder.GetContainerBuilder().Build();
            }

            return _container;
        }

        public T Resolve<T>()
        {
           return GetContainer().Resolve<T>();
        }

        public TInterFace Resolve<TInterFace, TImplementation>()
        {
            var resolverBuilder = new ResolverBuilder(this);
            _config?.Invoke(resolverBuilder);
            var containerBuilder = resolverBuilder.GetContainerBuilder();

            containerBuilder.RegisterType<TImplementation>().AsImplementedInterfaces().IfNotRegistered(typeof(TInterFace));

            var constructors = typeof(TImplementation).GetConstructors();
            var parameters = constructors.SelectMany(x => x.GetParameters()).Where(x => x.ParameterType.IsInterface).Distinct();

            foreach (var parameter in parameters)
            {
                containerBuilder.Register((c) =>
                {
                    var mockType = typeof(Mock<>).MakeGenericType(parameter.ParameterType);
                    var mock = Activator.CreateInstance(mockType);
                    return ((Mock)mock).Object;
                })
                .As(parameter.ParameterType)
                .IfNotRegistered(parameter.ParameterType);
            }

            var container = containerBuilder.Build();

            _container = container;

            return container.Resolve<TInterFace>();
        }



        public Resolver UseEntity<T>(Action<T> entiySetup) where T : class
        {
            using (var db = GetContext())
            {
                var entity = Activator.CreateInstance<T>();
                entiySetup(entity);

                db.Set<T>().Add(entity);
                db.SaveChanges();
            }

            return this;
        }

        public Resolver UseEntity<T>(Action<T> entiySetup, out T result) where T : class
        {
            using(var db = GetContext())
            {
                var entity = Activator.CreateInstance<T>();
                entiySetup(entity);

                db.Set<T>().Add(entity);
                db.SaveChanges();

                result = entity;
            }          

            return this;
        }

        public DbContext GetContext()
        {
            return GetContainer().Resolve<Infrastructure.TradgardsgolfContext>();
        }
    }
}
