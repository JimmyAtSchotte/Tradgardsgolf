using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Enums;
using Tradgardsgolf.Core.Infrastructure.Authentication;
using Tradgardsgolf.Core.Services.Authentication;
using Tradgardsgolf.Services.Authentication;

namespace Tradgardsgolf.Tests.Authentication
{
    [TestFixture]
    public class AuthenticationServiceTests
    {

        [Test]
        public void ShouldHaveSuccessStatusWhenLoginSucceeds()
        {
            var arrange = Arrange.Dependencies<IAuthenticationService, AuthenticationService>(
                dependencies =>
                {
                    dependencies.UseMock<IAuthenticationRepository>(mock =>
                    {
                        mock.Setup(x => x.AuthenticateWithCredentials(It.IsAny<ICredentialsDto>()))
                            .Returns(new StubAuthenticateDtoResult(AuthenticationStatus.Success));                     
                    });
                });

            var authenticationService = arrange.Resolve<IAuthenticationService>();

            var result = authenticationService.AuthenticateWithCredentials(new StubCredentialsModel());

            Assert.AreEqual(AuthenticationStatus.Success, result.Status);
        }
    }
}
