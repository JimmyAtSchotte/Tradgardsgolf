using Autofac;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tradgardsgolf.Core.Interfaces;
using Tradgardsgolf.Core.Interfaces.Repositories;
using Tradgardsgolf.Core.Interfaces.Services;
using Tradgardsgolf.Core.Models;
using Tradgardsgolf.Infrastructure;
using Tradgardsgolf.Infrastructure.Interfaces;

namespace Tradgardsgolf.Tests.Repositories.CreateLoginRepository
{
    [TestFixture]
    public class CreateLogin
    {
        [Test]
        public void ShouldReturTrueWhenEmailAllreadyExistsWithDiffrentCase()
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
                config.UseTradgardsgolfContext();
            });

            var repsoitory = resolver.Resolve<ICreateLoginRepository, Infrastructure.Repositories.CreateLoginRepository>();
            var model = new CreateLoginModel()
            {
                Email = "example@example.com",
                Password = "Password"
            };

            repsoitory.CreateLogin(model);

            var db = resolver.Resolve<TradgardsgolfContext>();
            var result = db.Player.FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.AreEqual(model.Email, result.Email);
            Assert.AreEqual(encrypt, result.Password);
            Assert.AreEqual(currentDateTime, result.Created);
        }

    }
}
