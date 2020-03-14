using EmailAddressLibrary;
using Tradgardsgolf.Core.Services.EmailValidating;

namespace Tradgardsgolf.EmailValidating
{
    public class EmailValidator : IEmailValidator
    {
        public bool IsValidEmail(string email)
        {
            return EmailAddressValidator.JStedfast(email);
        }
    }
}
