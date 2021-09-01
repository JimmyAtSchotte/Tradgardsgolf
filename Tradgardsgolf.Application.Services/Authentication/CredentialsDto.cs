using Tradgardsgolf.Core.Encryption;
using Tradgardsgolf.Core.Infrastructure.Authentication;
using Tradgardsgolf.Core.Services.Authentication;

namespace Tradgardsgolf.Services.Authentication
{
    public class CredentialsDto : ICredentialsDto
    {
        private readonly ICredentialsModel _model;
        public string Email => _model.Email;
        public EncryptedString Password => new EncryptedString(_model.Password);
        public CredentialsDto(ICredentialsModel model)
        {
            _model = model;
        } 
    }
}
