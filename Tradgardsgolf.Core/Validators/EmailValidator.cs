using EmailAddressLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces.Validators;

namespace Tradgardsgolf.Core.Validators
{
    public class EmailValidator : IEmailValidator
    {
        public bool IsValidEmail(string email)
        {
            return EmailAddressValidator.JStedfast(email);
        }
    }
}
