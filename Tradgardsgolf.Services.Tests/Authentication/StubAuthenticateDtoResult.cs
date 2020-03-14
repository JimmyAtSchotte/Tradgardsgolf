using Tradgardsgolf.Core.Enums;
using Tradgardsgolf.Core.Infrastructure.Authentication;

namespace Tradgardsgolf.Tests.Authentication
{
    public class StubAuthenticateDtoResult : IAuthenticateDtoResult
    {
        public AuthenticationStatus Status { get; }
        public int? Id { get; }
        public string Email { get; }
        public string Name { get; }
        public string Key { get; }

        public StubAuthenticateDtoResult(AuthenticationStatus status = AuthenticationStatus.Success, int? id = null, string email = null, string name = null, string key = null)
        {
            Status = status;
            Id = id;
            Email = email;
            Name = name;
            Key = key;
        }
    }
}
