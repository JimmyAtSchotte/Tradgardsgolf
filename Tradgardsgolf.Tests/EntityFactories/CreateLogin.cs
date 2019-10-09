using Autofac;
using Moq;
using NUnit.Framework;
using System;
using Tradgardsgolf.Core.Interfaces;
using Tradgardsgolf.Core.Interfaces.Models;
using Tradgardsgolf.Core.Interfaces.Services;
using Tradgardsgolf.Core.Models;
using Tradgardsgolf.Infrastructure.Entities;
using Tradgardsgolf.Infrastructure.EntityFactories;
using Tradgardsgolf.Infrastructure.Interfaces;

namespace Tradgardsgolf.Tests.EntityFactories
{
    [TestFixture]
    public class CreateLogin
    {
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
                    var assembly = typeof(IEntityFactory<,>).Assembly;
                    c.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEntityFactory<,>));
                });
            });

            var factory = resolver.Resolve<IEntityFactory<Player, ICreateLoginModel>, CreateLoginFactory>();

            ICreateLoginModel model = new CreateLoginModel()
            {
                Email = "example@example.com",
                Password = "Password"
            };
           
            var result = factory.Create(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(model.Email, result.Email);
            Assert.AreEqual(encrypt, result.Password);
            Assert.AreEqual(currentDateTime, result.Created);
        }
    }
}
