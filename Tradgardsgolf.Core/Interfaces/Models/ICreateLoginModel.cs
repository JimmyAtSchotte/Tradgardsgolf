using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Core.Interfaces.Models
{
    public interface ICreateLoginModel
    {
        string Email { get; }
        string Password { get; }
    }
}
