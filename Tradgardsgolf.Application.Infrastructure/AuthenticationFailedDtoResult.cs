using Tradgardsgolf.Core.Enums;
using Tradgardsgolf.Core.Infrastructure.Authentication;

namespace Tradgardsgolf.Infrastructure
{
    public class AuthenticationFailedDtoResult : IAuthenticateDtoResult
    {
        public AuthenticationStatus Status => AuthenticationStatus.Failed;

        public int? Id => default;
        public string Email => default;
        public string Name => default;

        public string Key => default;
    }
}
