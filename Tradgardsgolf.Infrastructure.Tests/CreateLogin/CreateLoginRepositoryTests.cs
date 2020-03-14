using System;
using System.Linq;
using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.EntityFrameworkCore;
using ArrangeDependencies.Autofac.Extensions;
using Autofac;
using Moq;
using NUnit.Framework;
using Tradgardsgolf.Core.Infrastructure.Login;
using Tradgardsgolf.Core.Services.Crypto;
using Tradgardsgolf.Core.Services.SystemClock;
using Tradgardsgolf.Infrastructure;
using Tradgardsgolf.Infrastructure.Context;
using Tradgardsgolf.Infrastructure.CreateLogin;

namespace Tradgardsgolf.Tests.Infrastructure.CreateLogin
{
    [TestFixture]
    public static class CreateLoginRepositoryTests
    {
        [TestFixture]
        internal class CreateLogin
        {
            [Test]
            public void ShouldCreateAPlayerEntity()
            {
                var encrypt = "****";
                var currentDateTime = DateTime.Now;

                var arrange = Arrange.Dependencies<ICreateLoginRepository, CreateLoginRepository>(config =>
                {
                    config.UseMock<ISystemClockService>(mock => mock.Setup(x => x.CurrentDateTime()).Returns(currentDateTime));
                    config.UseMock<ICryptoService>(mock => mock.Setup(x => x.Encrypt(It.IsAny<string>())).Returns(encrypt));
                
                    config.UseDbContext<TradgardsgolfContext>();
                });

                var repsoitory = arrange.Resolve<ICreateLoginRepository>();
                var dto = new StubCreateLoginDto();

                repsoitory.CreateLogin(dto);

                var db = arrange.Resolve<TradgardsgolfContext>();
                var result = db.Player.FirstOrDefault();

                Assert.IsNotNull(result);
                Assert.AreEqual(dto.Email, result.Email);
                Assert.AreEqual(dto.Password, result.Password);
                Assert.AreEqual(currentDateTime, result.Created);
            }
        }

        [TestFixture]
        internal class EmailExists
        {
            [Test]
            public void ShouldReturnFalseWhenEmailDontExists()
            {
                var arrange = Arrange.Dependencies<ICreateLoginRepository, CreateLoginRepository> (
                    dependencies => {
                        dependencies.UseDbContext<TradgardsgolfContext>();
                    });
            
                var repsoitory = arrange.Resolve<ICreateLoginRepository>();

                Assert.IsFalse(repsoitory.EmailExists("example@example.com"));
            }

            [Test]
            public void ShouldReturTrueWhenEmailAllreadyExists()
            {
                Player player = null;

                var arrange = Arrange.Dependencies<ICreateLoginRepository, CreateLoginRepository>(
                    dependencies => {
                        dependencies.UseEntity<Player, TradgardsgolfContext>((player) => player.SetEmail("example@example.com"), out player);
                    });
            
                var repsoitory = arrange.Resolve<ICreateLoginRepository>();

                Assert.IsTrue(repsoitory.EmailExists(player.Email));
            }

            [Test]
            public void ShouldReturTrueWhenEmailAllreadyExistsWithDiffrentCase()
            {
                Player player = null;

                var arrange = Arrange.Dependencies<ICreateLoginRepository, CreateLoginRepository>(
                    dependencies => {
                        dependencies.UseEntity<Player, TradgardsgolfContext>((player) => player.SetEmail("example@example.com"), out player);
                    });
            
                var repsoitory = arrange.Resolve<ICreateLoginRepository>();

                Assert.IsTrue(repsoitory.EmailExists(player.Email.ToUpper()));
            }
        }
        
        
    }
}
