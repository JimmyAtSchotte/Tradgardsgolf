using System.Linq;
using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.EntityFrameworkCore;
using NUnit.Framework;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure.Login;

namespace Tradgardsgolf.Infrastructure.Tests.CreateLogin
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
                var arrange = Arrange.Dependencies<ICreateLoginRepository, CreateLoginRepository>(config =>
                {
                    config.UseDbContext<TradgardsgolfContext>();
                });

                var repsoitory = arrange.Resolve<ICreateLoginRepository>();
                var dto = new StubCreateLoginDto();

                repsoitory.CreateLogin(dto);

                var db = arrange.Resolve<TradgardsgolfContext>();
                var result = db.Player.FirstOrDefault();

                Assert.IsNotNull(result);
                Assert.AreEqual(dto.Email.Value, result.Email);
                Assert.AreEqual(dto.Password.Value, result.Password);
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
                        dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(x => x.Email = "example@example.com"), out player);
                    });
            
                var repsoitory = arrange.Resolve<ICreateLoginRepository>();

                Assert.IsTrue(repsoitory.EmailExists(player.Email));
            }

            [Test]
            public void ShouldReturnTrueWhenEmailAlreadyExistsWithDifferentCase()
            {
                Player player = null;

                var arrange = Arrange.Dependencies<ICreateLoginRepository, CreateLoginRepository>(
                    dependencies => {
                        dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(x => x.Email = "example@example.com"), out player);
                    });
            
                var repsoitory = arrange.Resolve<ICreateLoginRepository>();

                Assert.IsTrue(repsoitory.EmailExists(player.Email.ToUpper()));
            }
        }
        
        
    }
}
