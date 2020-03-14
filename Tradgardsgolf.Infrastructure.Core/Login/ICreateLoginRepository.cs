using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Core.Infrastructure.Login
{
    public interface ICreateLoginRepository
    {
        bool EmailExists(string email);
        void CreateLogin(ICreateLoginDto createLoginModel);
    }
}
