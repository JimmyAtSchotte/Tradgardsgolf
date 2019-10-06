using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces;
using Tradgardsgolf.Core.Interfaces.Models;
using Tradgardsgolf.Core.Interfaces.Repositories;
using Tradgardsgolf.Core.Interfaces.Services;
using Tradgardsgolf.Core.Interfaces.Validators;
using Tradgardsgolf.Core.SharedKernel.Enums;

namespace Tradgardsgolf.Core.Services
{
    public class CreateLoginService : ICreateLoginService
    {
        private readonly IEmailValidator _emailValidator;
        private readonly ICreateLoginRepository _createLoginRepository;

        public CreateLoginService(IEmailValidator emailValidator, ICreateLoginRepository createLoginRepository)
        {
            _emailValidator = emailValidator;
            _createLoginRepository = createLoginRepository;
        }

        public ICreateLoginResult CreateLogin(ICreateLoginModel createLogin)
        {
            if(!_emailValidator.IsValidEmail(createLogin.Email))
                return new CreateLoginResult(CreateLoginStatus.InvalidEmail);

            if (_createLoginRepository.EmailExists(createLogin.Email))
                return new CreateLoginResult(CreateLoginStatus.EmailAllreadyExists);

            _createLoginRepository.CreateLogin(createLogin);

            return new CreateLoginResult(CreateLoginStatus.Success);
        }
    }
}
