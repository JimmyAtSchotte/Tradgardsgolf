using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Core.Email
{
    public struct EmailValidation
    {
        public static IEmailValidator Default => new RegexEmailValidation();
        public static IEmailValidator None => new NoEmailValidation();
    }
}
