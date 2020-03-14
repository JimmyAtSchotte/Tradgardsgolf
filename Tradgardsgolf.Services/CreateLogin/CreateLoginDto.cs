using Tradgardsgolf.Core.Infrastructure.Login;
using Tradgardsgolf.Core.Services.CreateLogin;
using Tradgardsgolf.Core.Services.Crypto;

namespace Tradgardsgolf.CreateLogin
{
    public class CreateLoginDto : ICreateLoginDto
    {
        private readonly ICreateLoginModel _model;
        private readonly ICryptoService _cryptoService;

        public string Email => _model.Email;
        public string Password => _cryptoService.Encrypt(_model.Password);

        public CreateLoginDto(ICreateLoginModel model, ICryptoService cryptoService)
        {
            _model = model;
            _cryptoService = cryptoService;
        }
    }
}