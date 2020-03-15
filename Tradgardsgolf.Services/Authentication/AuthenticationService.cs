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

        public IAuthenticationModelResult AuthenticateWithCredentials(ICredentialsModel credentials)
        {
            var result = _authenticationRepository.AuthenticateWithCredentials(new CredentialsDto(credentials));

            return new AuthenticationModelResult(result);
        }
    }
}
