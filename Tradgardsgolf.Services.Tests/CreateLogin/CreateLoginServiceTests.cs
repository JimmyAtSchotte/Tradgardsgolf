using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using Moq;
using NUnit.Framework;
using Tradgardsgolf.Core.Enums;
using Tradgardsgolf.Core.Infrastructure.Login;
using Tradgardsgolf.Core.Services.CreateLogin;
using Tradgardsgolf.Core.Services.EmailValidating;

namespace Tradgardsgolf.Tests.CreateLogin
{
    [TestFixture]
    public class CreateLoginServiceTests
    {      
        [Test]
        public void StatusShouldBeInvalidEmail()
        {
            var arrange = Arrange.Dependencies<ICreateLoginService, Services.CreateLogin.CreateLoginService>(
                dependencies =>
                {
                    dependencies.UseMock<IEmailValidator>(mock =>
                    {
                        mock.Setup(x => x.IsValidEmail(It.IsAny<string>())).Returns(false);
                    });
             });
           
            var createLoginService = arrange.Resolve<ICreateLoginService>();
            var result = createLoginService.CreateLogin(new StubCreateLoginModel());

            Assert.AreEqual(CreateLoginStatus.InvalidEmail, result.Status);
        }

        [Test]
        public void StatusShouldBeEmailAlreadyExists()
        {
            var arrange = Arrange.Dependencies<ICreateLoginService, Services.CreateLogin.CreateLoginService>(
            dependencies => {
                dependencies.UseMock<IEmailValidator>(mock =>
                {
                    mock.Setup(x => x.IsValidEmail(It.IsAny<string>())).Returns(true);
                });

                dependencies.UseMock<ICreateLoginRepository>(mock =>
                {
                    mock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(true);
                });
            });
            
            var createLoginService = arrange.Resolve<ICreateLoginService>();
            var result = createLoginService.CreateLogin(new StubCreateLoginModel());

            Assert.AreEqual(CreateLoginStatus.EmailAlreadyExists, result.Status);
        }

        [Test]
        public void StatusShouldBeSuccess()
        {
            var arrange = Arrange.Dependencies<ICreateLoginService, Services.CreateLogin.CreateLoginService>(
            dependencies => {
                dependencies.UseMock<IEmailValidator>(mock =>
                {
                    mock.Setup(x => x.IsValidEmail(It.IsAny<string>())).Returns(true);
                });

                dependencies.UseMock<ICreateLoginRepository>(mock =>
                {
                    mock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(false);
                });
            });

            var createLoginService = arrange.Resolve<ICreateLoginService>();
            var result = createLoginService.CreateLogin(new StubCreateLoginModel());

            Assert.AreEqual(CreateLoginStatus.Success, result.Status);
        }

        [Test]
        public void ShouldInvokeCreateLoginInCreateLoginRepository()
        {
            var createLoginRepositoryMock = new Mock<ICreateLoginRepository>();

            var arrange = Arrange.Dependencies<ICreateLoginService, Services.CreateLogin.CreateLoginService>(
           dependencies => {
               dependencies.UseMock<IEmailValidator>(mock =>
               {
                   mock.Setup(x => x.IsValidEmail(It.IsAny<string>())).Returns(true);
               });

               dependencies.UseMock<ICreateLoginRepository>(mock =>
               {
                   mock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(false);
               }, out createLoginRepositoryMock);
           });

            
            var createLoginService = arrange.Resolve<ICreateLoginService>();
            var result = createLoginService.CreateLogin(new StubCreateLoginModel());

            createLoginRepositoryMock.Verify(x => x.CreateLogin(It.IsAny<ICreateLoginDto>()), Times.Once());
        }

    }
}
