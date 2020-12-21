using Tradgardsgolf.Core.Enums;
using Tradgardsgolf.Core.Infrastructure.Authentication;

namespace Tradgardsgolf.Infrastructure
{
    public class AuthenticationSucceedDtoResult : IAuthenticateDtoResult
    {
        public AuthenticationStatus Status => AuthenticationStatus.Success;

        public int? Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
    }
}
