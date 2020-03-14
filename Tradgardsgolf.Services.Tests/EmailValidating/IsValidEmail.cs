using NUnit.Framework;

namespace Tradgardsgolf.Tests.EmailValidating
{
    [TestFixture]
    public class IsValidEmail 
    {
        [Test]
        public void ShouldBeValidEmail()
        {
            var emailValidator = new Tradgardsgolf.EmailValidating.EmailValidator();

            Assert.IsTrue(emailValidator.IsValidEmail("example@sxample.com"));
        }

        [TestCase("")]
        [TestCase("test")]
        [TestCase("@test.com")]
        public void ShouldBeInvalidEmail(string email)
        {
            var emailValidator = new Tradgardsgolf.EmailValidating.EmailValidator();

            Assert.IsFalse(emailValidator.IsValidEmail(email));
        }

    }
}
