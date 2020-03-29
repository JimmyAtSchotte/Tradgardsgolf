using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using Tradgardsgolf.Core.Enums;
using Tradgardsgolf.Core.Infrastructure.Authentication;
using Tradgardsgolf.Infrastructure.Context;

namespace Tradgardsgolf.Infrastructure.Tests.Authentication
{
    [TestFixture]
    public class AuthenticationRepositoryTest
    {

        [Test]
        public void ShouldFailWhenNoPlayers()
        {
            var arrange = Arrange.Dependencies<IAuthenticationRepository, Infrastructure.Authentication.AuthenticationRepository>(dependencies => {
                dependencies.UseDbContext<TradgardsgolfContext>();
            });

            var authenticationRepository = arrange.Resolve<IAuthenticationRepository>();

            var result = authenticationRepository.CredentialsAuthentication(new StubCredentialsDto());

            Assert.AreEqual(AuthenticationStatus.Failed, result.Status);
        }

        [Test]
        public void ShouldSucceedWhenCorrectCreadentials()
        {
            var dto = new StubCredentialsDto();
            Player player = null;

            var arrange = Arrange.Dependencies<IAuthenticationRepository, Infrastructure.Authentication.AuthenticationRepository>(dependencies => {
                dependencies.UseDbContext<TradgardsgolfContext>();

                dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(x => {
                    x.Email = dto.Email;
                    x.Password = dto.Password.Value; ;
                }), out player);
            });

            var authenticationRepository = arrange.Resolve<IAuthenticationRepository>();

            var result = authenticationRepository.CredentialsAuthentication(dto);

            Assert.AreEqual(AuthenticationStatus.Success, result.Status);
            Assert.AreEqual(player.Id, result.Id);
            Assert.AreEqual(player.Name, result.Name);
            Assert.AreEqual(player.Email, result.Email);
            Assert.IsNotNull(result.Key);
        }

        [Test]
        public void ShouldSetNewKeyOnSuccessfullLogin()
        {
            var dto = new StubCredentialsDto();
            Player player = null;

            var arrange = Arrange.Dependencies<IAuthenticationRepository, Infrastructure.Authentication.AuthenticationRepository>(dependencies => {
                dependencies.UseDbContext<TradgardsgolfContext>();

                dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(x => {
                    x.Email = dto.Email;
                    x.Password = dto.Password.Value; ;
                    }), out player);
            });

            var authenticationRepository = arrange.Resolve<IAuthenticationRepository>();
            var db = arrange.Resolve<TradgardsgolfContext>();

            var result = authenticationRepository.CredentialsAuthentication(dto);
            var playerDb = db.Player.FirstOrDefault(x => x.Id == player.Id);

            Assert.AreEqual(playerDb.Key, result.Key);
        }
    }
}
