using Tradgardsgolf.Core.Enums;
using Tradgardsgolf.Core.Infrastructure.Login;
using Tradgardsgolf.Core.Services.CreateLogin;
using Tradgardsgolf.Core.Services.Crypto;
using Tradgardsgolf.Core.Services.EmailValidating;

namespace Tradgardsgolf.Services.CreateLogin
{
    public class CreateLoginService : ICreateLoginService
    {
        private readonly IEmailValidator _emailValidator;
        private readonly ICreateLoginRepository _createLoginRepository;

        public CreateLoginService(IEmailValidator emailValidator,  ICreateLoginRepository loginRepository)
        {
            _emailValidator = emailValidator;
            _createLoginRepository = loginRepository;
        }
        
        public ICreateLoginResult CreateLogin(ICreateLoginModel createLogin)
        {
            if(!_emailValidator.IsValidEmail(createLogin.Email))
                return new CreateLoginResult(CreateLoginStatus.InvalidEmail);

            if (_createLoginRepository.EmailExists(createLogin.Email))
                return new CreateLoginResult(CreateLoginStatus.EmailAlreadyExists);

            _createLoginRepository.CreateLogin(new CreateLoginDto(createLogin));

            return new CreateLoginResult(CreateLoginStatus.Success);
        }
    }
}
