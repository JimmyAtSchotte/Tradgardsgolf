using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Core.Interfaces.Validators
{
    public interface IEmailValidator
    {
        bool IsValidEmail(string email);
    }
}
