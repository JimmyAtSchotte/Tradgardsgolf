using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Core.Interfaces.Models
{
    public interface ICredentialsModel
    {
        string Email { get; }
        string Password { get; }
    }
}
