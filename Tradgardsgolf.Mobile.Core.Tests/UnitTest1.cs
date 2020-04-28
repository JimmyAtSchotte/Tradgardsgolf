using NUnit.Framework;
using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using Autofac;
using Tradgardsgolf.Mobile.Core.Interfaces;

namespace Tradgardsgolf.Mobile.Core.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var arrange = Arrange.Dependencies(dependencies =>
            {
                dependencies.UseContainerBuilder(builder => builder.Register(ctx => SessionStorage.Instance).SingleInstance().AsImplementedInterfaces());
            });

            var storage = arrange.Resolve<ISessionStorage>();

            Assert.IsNotNull(storage);
        }

        [Test]
        public void Test2()
        {
            var arrange = Arrange.Dependencies(dependencies =>
            {
                dependencies.UseContainerBuilder(builder => builder.Register(ctx => SessionStorage.Instance).SingleInstance().AsImplementedInterfaces());
            });

            var storage = arrange.Resolve<ISessionStorage>();

            Assert.IsNotNull(storage.Courses);
        }
    }
}