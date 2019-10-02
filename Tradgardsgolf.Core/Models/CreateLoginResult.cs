using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces.Models;
using Tradgardsgolf.Core.SharedKernel.Enums;

namespace Tradgardsgolf.Core
{
    public class CreateLoginResult : ICreateLoginResult
    {
        private readonly CreateLoginStatus _result;

        public CreateLoginStatus Status => _result;

        public CreateLoginResult(CreateLoginStatus result)
        {
            _result = result;
        }
    }
}
