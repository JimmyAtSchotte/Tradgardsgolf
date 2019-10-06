using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces.Models;

namespace Tradgardsgolf.Core.Models
{
    public class CreateLoginModel : ICreateLoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
