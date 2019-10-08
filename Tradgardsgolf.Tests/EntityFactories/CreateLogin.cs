using Autofac;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces;
using Tradgardsgolf.Core.Interfaces.Models;
using Tradgardsgolf.Core.Interfaces.Services;
using Tradgardsgolf.Core.Models;
using Tradgardsgolf.Infrastructure.Entities;
using Tradgardsgolf.Infrastructure.EntityFactories;
using Tradgardsgolf.Infrastructure.Interfaces;
using Tradgardsgolf.Infrastructure.Strategies;

namespace Tradgardsgolf.Tests.EntityFactories
{
    [TestFixture]
    public class CreateLogin
    {
        [Test]
        public void ShouldCreateInstanceOfCreateLoginFactory()
        {
            var resolver = new Resolver(config =>
            {
                config.UseDependencies(c =>
                {
                    var assembly = typeof(IEntityFactoryStrategy<>).Assembly;
                    c.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEntityFactoryStrategy<>));
                    c.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEntityFactoryFactory<>));
                    c.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEntityFactoryProvider<>));
                });
            });


            var entityStrategy = resolver.Resolve<IEntityFactoryStrategy<Player>, EntityFactoryStrategy<Player>>();
            var factory = entityStrategy.Create<ICreateLoginModel>();

            Assert.IsInstanceOf<Infrastructure.EntityFactories.CreateLogin>(factory);
        }

        [Test]
        public void ShouldCreateNewPlayer()
        {
            var encrypt = "****";
            var currentDateTime = DateTime.Now;

            var resolver = new Resolver(config =>
            {
                config.UseMock<ISystemClockService>(mock => mock.Setup(x => x.CurrentDateTime()).Returns(currentDateTime));

                config.UseMock<ICryptoService>(mock => mock.Setup(x => x.Encrypt(It.IsAny<string>())).Returns(encrypt));

                config.UseDependencies(c =>
                {
                    var assembly = typeof(IEntityFactoryStrategy<>).Assembly;
                    c.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEntityFactoryStrategy<>));
                    c.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEntityFactoryFactory<>));
                    c.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEntityFactoryProvider<>));
                });
            });

            var entityStrategy = resolver.Resolve<IEntityFactoryStrategy<Player>, EntityFactoryStrategy<Player>>();

            ICreateLoginModel model = new CreateLoginModel()
            {
                Email = "example@example.com",
                Password = "Password"
            };

            var factory = entityStrategy.Create<ICreateLoginModel>();
            var result = factory.Create(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(model.Email, result.Email);
            Assert.AreEqual(encrypt, result.Password);
            Assert.AreEqual(currentDateTime, result.Created);
        }
    }
}
