using Tradgardsgolf.Core.Infrastructure.Authentication;
using Tradgardsgolf.Core.Services.Authentication;

namespace Tradgardsgolf.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
          

        public IAuthenticationModelResult CredentialsAuthentication(ICredentialsModel credentials)
        {
            var result = _authenticationRepository.CredentialsAuthentication(new CredentialsDto(credentials));

            return new AuthenticationModelResult(result);
        }

        public IAuthenticationModelResult TokenAuthentication(ITokenAuthenticationModel model)
        {
            var result = _authenticationRepository.KeyAuthentication(new KeyAuthenticationDto(model));

            return new AuthenticationModelResult(result);
        }
    }
}
