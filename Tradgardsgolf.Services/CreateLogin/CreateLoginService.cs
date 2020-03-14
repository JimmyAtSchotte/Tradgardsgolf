using Tradgardsgolf.Core.Infrastructure.Login;
using Tradgardsgolf.Core.Services.CreateLogin;
using Tradgardsgolf.Core.Services.Crypto;
using Tradgardsgolf.Core.Services.EmailValidating;
using Tradgardsgolf.Core.SharedKernel.Enums;

namespace Tradgardsgolf.CreateLogin
{
    public class CreateLoginService : ICreateLoginService
    {
        private readonly IEmailValidator _emailValidator;
        private readonly ICryptoService _cryptoService;
        private readonly ICreateLoginRepository _createLoginRepository;

        public CreateLoginService(IEmailValidator emailValidator, ICryptoService cryptoService, ICreateLoginRepository loginRepository)
        {
            _emailValidator = emailValidator;
            _cryptoService = cryptoService;
            _createLoginRepository = loginRepository;
        }
        
        public ICreateLoginResult CreateLogin(ICreateLoginModel createLogin)
        {
            if(!_emailValidator.IsValidEmail(createLogin.Email))
                return new CreateLoginResult(CreateLoginStatus.InvalidEmail);

            if (_createLoginRepository.EmailExists(createLogin.Email))
                return new CreateLoginResult(CreateLoginStatus.EmailAlreadyExists);

            _createLoginRepository.CreateLogin(new CreateLoginDto(createLogin, _cryptoService));

            return new CreateLoginResult(CreateLoginStatus.Success);
        }
    }
}
