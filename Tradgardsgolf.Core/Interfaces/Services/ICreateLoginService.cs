using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces.Models;

namespace Tradgardsgolf.Core.Interfaces.Services
{
    public interface ICreateLoginService
    {
        ICreateLoginResult CreateLogin(ICreateLoginModel createLogin);
    }
}
