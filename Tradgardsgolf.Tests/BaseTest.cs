using Autofac;
using Moq;
using System;
using System.Linq;

namespace Tradgardsgolf.Tests
{
    public abstract class BaseTest<TInterFace, TImplementation>
    {
        protected TInterFace GetService(Action<ContainerBuilder> action = null)
        {
            var containerBuilder = new ContainerBuilder();
            action?.Invoke(containerBuilder);
            containerBuilder.RegisterType<TImplementation>().AsImplementedInterfaces();

            var constructors = typeof(TImplementation).GetConstructors();
            var parameters = constructors.SelectMany(x => x.GetParameters()).Where(x => x.ParameterType.IsInterface).Distinct();
                    
            foreach(var parameter in parameters)
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
