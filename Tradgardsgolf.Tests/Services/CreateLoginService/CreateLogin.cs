using Autofac;
using Moq;
using NUnit.Framework;
using Tradgardsgolf.Core.Interfaces.Models;
using Tradgardsgolf.Core.Interfaces.Repositories;
using Tradgardsgolf.Core.Interfaces.Services;
using Tradgardsgolf.Core.Interfaces.Validators;
using Tradgardsgolf.Core.Models;
using Tradgardsgolf.Core.SharedKernel.Enums;

namespace Tradgardsgolf.Tests.Services.CreateLoginService
{

    [TestFixture]
    public class CreateLogin
    {      
        [Test]
        public void StatusShouldBeInvalidEmail()
        {
            var resolver = new Resolver(config =>
            {
                config.UseMock<IEmailValidator>(mock =>
                {
                    mock.Setup(x => x.IsValidEmail(It.IsAny<string>())).Returns(false);
                });
            });

            var createLoginService = resolver.Resolve<ICreateLoginService, Core.Services.CreateLoginService>();

            var result = createLoginService.CreateLogin(new CreateLoginModel()
            {
                Email = "invalid email"
            });

            Assert.AreEqual(CreateLoginStatus.InvalidEmail, result.Status);
        }

        [Test]
        public void StatusShouldBeEmailAllreadyExists()
        {

            var resolver = new Resolver(config =>
            {
                config.UseMock<IEmailValidator>(mock =>
                {
                    mock.Setup(x => x.IsValidEmail(It.IsAny<string>())).Returns(true);
                });

                config.UseMock<ICreateLoginRepository>(mock =>
                {
                    mock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(true);
                });
            });

            var createLoginService = resolver.Resolve<ICreateLoginService, Core.Services.CreateLoginService>();

            var result = createLoginService.CreateLogin(new CreateLoginModel() {
                Email = "example@example.com"
            });

            Assert.AreEqual(CreateLoginStatus.EmailAllreadyExists, result.Status);
        }

        [Test]
        public void StatusShouldBeSuccess()
        {

            var resolver = new Resolver(config =>
            {
                config.UseMock<IEmailValidator>(mock =>
                {
                    mock.Setup(x => x.IsValidEmail(It.IsAny<string>())).Returns(true);
                });

                config.UseMock<ICreateLoginRepository>(mock =>
                {
                    mock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(false);
                });
            });

            var createLoginService = resolver.Resolve<ICreateLoginService, Core.Services.CreateLoginService>();

            var result = createLoginService.CreateLogin(new CreateLoginModel()
            {
                Email = "example@example.com"
            });

            Assert.AreEqual(CreateLoginStatus.Success, result.Status);
        }

        [Test]
        public void ShouldInvokeCreateLoginInCreateLoginRepository()
        {
            var createLoginRepositoryMock = new Mock<ICreateLoginRepository>();

            var resolver = new Resolver(config =>
            {
                config.UseMock<IEmailValidator>(mock =>
                {
                    mock.Setup(x => x.IsValidEmail(It.IsAny<string>())).Returns(true);
                });

                config.UseMock(mock =>
                {
                    mock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(false);
                }, out createLoginRepositoryMock);
            });

            var createLoginService = resolver.Resolve<ICreateLoginService, Core.Services.CreateLoginService>();

            var result = createLoginService.CreateLogin(new CreateLoginModel()
            {
                Email = "example@example.com"
            });

            createLoginRepositoryMock.Verify(x => x.CreateLogin(It.IsAny<ICreateLoginModel>()), Times.Once());
        }

    }
}
