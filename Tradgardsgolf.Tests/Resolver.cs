using Autofac;
using Moq;
using System;
using System.Linq;
using System.Text;

namespace Tradgardsgolf.Tests
{
    public class Resolver
    {
        private readonly Action<ResolverBuilder> _config;
      
        public Resolver(Action<ResolverBuilder> config = null)
        {
            _config = config;
        }        

        public TInterFace Resolve<TInterFace, TImplementation>()
        {
            var resolverBuilder = new ResolverBuilder();
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

            return container.Resolve<TInterFace>();
        }  
    }
}
