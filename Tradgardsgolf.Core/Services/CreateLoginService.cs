using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces;
using Tradgardsgolf.Core.Interfaces.Models;
using Tradgardsgolf.Core.Interfaces.Services;
using Tradgardsgolf.Core.SharedKernel.Enums;

namespace Tradgardsgolf.Core.Services
{
    public class CreateLoginService : ICreateLoginService
    {
        private readonly ICryptoService _cryptoService;

        public CreateLoginService(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public ICreateLoginResult CreateLogin(ICreateLoginModel createLogin)
        {
            return new CreateLoginResult(CreateLoginStatus.InvalidEmail);
        }
    }
}
