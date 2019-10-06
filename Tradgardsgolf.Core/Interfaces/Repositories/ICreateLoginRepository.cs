using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces.Models;

namespace Tradgardsgolf.Core.Interfaces.Repositories
{
    public interface ICreateLoginRepository
    {
        bool EmailExists(string email);
        void CreateLogin(ICreateLoginModel createLoginModel);
    }
}
