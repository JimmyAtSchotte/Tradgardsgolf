using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces.Models;
using Tradgardsgolf.Core.Interfaces.Services;
using Tradgardsgolf.Core.Services;
using Tradgardsgolf.Core.SharedKernel.Enums;

namespace Tradgardsgolf.Tests.Services.CreateLoginService
{

    [TestFixture]
    public class CreateLogin : BaseTest<ICreateLoginService, Core.Services.CreateLoginService>
    {      
        [Test]
        public void StatusShouldBeInvalidEmail()
        {          
            var createLoginService = GetService();

            var result = createLoginService.CreateLogin(Mock.Of<ICreateLoginModel>());

            Assert.AreEqual(CreateLoginStatus.InvalidEmail, result.Status);
        }

    }
}
