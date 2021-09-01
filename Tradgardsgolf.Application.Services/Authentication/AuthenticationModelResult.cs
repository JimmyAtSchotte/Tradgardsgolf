using Tradgardsgolf.Core.Enums;
using Tradgardsgolf.Core.Infrastructure.Authentication;
using Tradgardsgolf.Core.Services.Authentication;

namespace Tradgardsgolf.Services.Authentication
{
    public class AuthenticationModelResult : IAuthenticationModelResult
    {
        private readonly IAuthenticateDtoResult _result;

        public AuthenticationStatus Status => _result.Status;
        public int? Id => _result.Id;
        public string Email => _result.Email;
        public string Name => _result.Name;
        public string Key => _result.Key;

        public AuthenticationModelResult(IAuthenticateDtoResult result)
        {
            _result = result;
        }
    }
}
