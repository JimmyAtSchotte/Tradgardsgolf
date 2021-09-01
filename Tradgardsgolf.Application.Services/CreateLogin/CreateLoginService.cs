using Tradgardsgolf.Core.Enums;
using Tradgardsgolf.Core.Infrastructure.Login;
using Tradgardsgolf.Core.Services.CreateLogin;
namespace Tradgardsgolf.Services.CreateLogin
{
    public class CreateLoginService : ICreateLoginService
    {
        private readonly ICreateLoginRepository _createLoginRepository;

        public CreateLoginService(ICreateLoginRepository loginRepository)
        {
            _createLoginRepository = loginRepository;
        }
        
        public ICreateLoginResult CreateLogin(ICreateLoginModel createLogin)
        {
            if (_createLoginRepository.EmailExists(createLogin.Email))
                return new CreateLoginResult(CreateLoginStatus.EmailAlreadyExists);

            _createLoginRepository.CreateLogin(new CreateLoginDto(createLogin));

            return new CreateLoginResult(CreateLoginStatus.Success);
        }
    }
}
