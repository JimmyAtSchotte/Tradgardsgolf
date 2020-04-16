using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Tradgardsgolf.ApiClient.Models;
using Tradgardsgolf.Mobile.DataStore;

namespace Tradgardsgolf.Mobile.Tests
{
    public class StartupTest
    {

        [Test]
        public void GetAppPageStrategy()
        {
            Startup.CreateApplication().GetService<IAppPageStrategy>();
        }

        [Test]
        public void GetAppPageFactories()
        {
            Startup.CreateApplication().GetServices<IAppPageFactory>();
        }

        [Test]
        public void GetApiRepository()
        {
            Startup.CreateApplication().GetService<IApiRepository>();
        }

        [Test]
        public void GetContext()
        {
            Startup.CreateApplication().GetService<IContext>();
        }
    }


    public class ContextTests
    {


       
        [Test]
        public async Task ShouldInvokeAuthenticationSucceded()
        {
            var arrange = Arrange.Dependencies<IContext, Context>(dependencies =>
            {
                dependencies.UseMock<IApiRepository>(mock => mock.Setup(x => x.AuthenticateAsync(It.IsAny<CredentialsModel>())).Returns(Task.FromResult(true)));
            });

            var authenticationSuccededEventInvoked = false;
            var context = arrange.Resolve<IContext>();
            context.AuthenticationSucceded += async () => await Task.Run(async () => authenticationSuccededEventInvoked = true);

            await context.Authentication.AuthenticateAsync("email", "password");
            
            Assert.IsTrue(authenticationSuccededEventInvoked);
        }

        [Test]
        public async Task ShouldInvokeUnauthorized()
        {
            var arrange = Arrange.Dependencies<IContext, Context>(dependencies =>
            {
                dependencies.UseMock<IApiRepository>(mock => mock.Setup(x => x.IsAuthorizedAsync()).Throws<UnauthorizedException>());
            });

            var unauthorizedInvoked = false;
            var context = arrange.Resolve<IContext>();
            context.Unauthorized += async () => await Task.Run(async () => unauthorizedInvoked = true);

            await context.Authentication.IsAuthorizedAsync();

            Assert.IsTrue(unauthorizedInvoked);
        }
    }
}