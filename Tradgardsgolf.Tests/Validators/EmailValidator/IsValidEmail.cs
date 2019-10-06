using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Tests.Validators.EmailValidator
{
    [TestFixture]
    public class IsValidEmail 
    {
        [Test]
        public void ShouldBeValidEmail()
        {
            var emailValidator = new Core.Validators.EmailValidator();

            Assert.IsTrue(emailValidator.IsValidEmail("example@sxample.com"));
        }

        [TestCase("")]
        [TestCase("test")]
        [TestCase("@test.com")]
        public void ShouldBeInvalidEmail(string email)
        {
            var emailValidator = new Core.Validators.EmailValidator();

            Assert.IsFalse(emailValidator.IsValidEmail(email));
        }

    }
}
