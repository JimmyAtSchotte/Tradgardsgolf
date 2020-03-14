using Tradgardsgolf.Core.Infrastructure.Authentication;
using Tradgardsgolf.Core.Services.Authentication;
using Tradgardsgolf.SharedKernel.Encryption;

namespace Tradgardsgolf.Authentication
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
