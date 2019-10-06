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
    public class CreateLogin : BaseTest<ICreateLoginService, Core.Services.CreateLoginService>
    {      
        [Test]
        public void StatusShouldBeInvalidEmail()
        {          
            var createLoginService = GetService(container =>
            {
                container.Register(c =>
                 {
                     var mock = new Mock<IEmailValidator>();
                     mock.Setup(x => x.IsValidEmail(It.IsAny<string>())).Returns(false);
                     return mock.Object;
                 });
            });

            var result = createLoginService.CreateLogin(new CreateLoginModel()
            {
                Email = "invalid email"
            });

            Assert.AreEqual(CreateLoginStatus.InvalidEmail, result.Status);
        }

        [Test]
        public void StatusShouldBeEmailAllreadyExists()
        {
            var createLoginService = GetService(container => {
                container.Register(c =>
                {
                    var mock = new Mock<IEmailValidator>();
                    mock.Setup(x => x.IsValidEmail(It.IsAny<string>())).Returns(true);
                    return mock.Object;
                });

                container.Register(c =>
                {
                    var mock = new Mock<ICreateLoginRepository>();
                    mock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(true);
                    return mock.Object;
                });            
            });

            var result = createLoginService.CreateLogin(new CreateLoginModel() {
                Email = "example@example.com"
            });

            Assert.AreEqual(CreateLoginStatus.EmailAllreadyExists, result.Status);
        }

        [Test]
        public void StatusShouldBeSuccess()
        {
            var createLoginService = GetService(container => {
                container.Register(c =>
                {
                    var mock = new Mock<IEmailValidator>();
                    mock.Setup(x => x.IsValidEmail(It.IsAny<string>())).Returns(true);
                    return mock.Object;
                });

                container.Register(c =>
                {
                    var mock = new Mock<ICreateLoginRepository>();
                    mock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(false);
                    return mock.Object;
                });
            });

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
            createLoginRepositoryMock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(false);

            var createLoginService = GetService(container => {
                container.Register(c =>
                {
                    var mock = new Mock<IEmailValidator>();
                    mock.Setup(x => x.IsValidEmail(It.IsAny<string>())).Returns(true);
                    return mock.Object;
                });

                container.Register(c => createLoginRepositoryMock.Object);
            });

            var result = createLoginService.CreateLogin(new CreateLoginModel()
            {
                Email = "example@example.com"
            });

            createLoginRepositoryMock.Verify(x => x.CreateLogin(It.IsAny<ICreateLoginModel>()), Times.Once());
        }

    }
}
