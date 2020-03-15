using Tradgardsgolf.Core.Email;
using Tradgardsgolf.Core.Infrastructure.Login;
using Tradgardsgolf.Core.Services.CreateLogin;
using Tradgardsgolf.Core.Services.Crypto;
using Tradgardsgolf.SharedKernel.Encryption;

namespace Tradgardsgolf.Services.CreateLogin
{
    public class CreateLoginDto : ICreateLoginDto
    {
        private readonly ICreateLoginModel _model;

        public EmailString Email => new EmailString(_model.Email);
        public EncryptedString Password => new EncryptedString(_model.Password);

        public CreateLoginDto(ICreateLoginModel model)
        {
            _model = model;
        }
    }
}