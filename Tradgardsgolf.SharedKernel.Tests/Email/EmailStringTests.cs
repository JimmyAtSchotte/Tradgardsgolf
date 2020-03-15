using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Email;

namespace Tradgardsgolf.SharedKernel.Tests.Email
{
    [TestFixture]
    public class EmailStringTests
    {

        [TestCase("example@sxample.com")]
        public void ShouldBeValidEmail(string email)
        {
            var emailString = new EmailString(email);

            Assert.AreEqual(email, emailString.Value);
        }

        [TestCase("")]
        [TestCase("test")]
        [TestCase("@test.com")]
        public void ShouldBeInvalidEmail(string email)
        {
            var emailString = new EmailString(email);

            Assert.Throws<InvalidEmailException>(() => {
                var result = emailString.Value;
            });
        }
    }
}
