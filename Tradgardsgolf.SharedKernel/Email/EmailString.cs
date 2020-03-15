using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Core.Email
{

    public class EmailString
    {
        private readonly string _email;
        private readonly IEmailValidator _emailValidator;

        public string Value => GetValue();
        
        public EmailString(string email, IEmailValidator emailValidator = null)
        {
            _email = email;
            _emailValidator = emailValidator ?? EmailValidation.Default;
        }

        private string GetValue()
        {
            if (!_emailValidator.IsValid(_email))
                throw new InvalidEmailException(_email);

            return _email;

        }
    }
}
