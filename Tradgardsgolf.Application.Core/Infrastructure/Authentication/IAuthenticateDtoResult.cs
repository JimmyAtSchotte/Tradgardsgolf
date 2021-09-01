using Tradgardsgolf.Core.Enums;

namespace Tradgardsgolf.Core.Infrastructure.Authentication
{
    public interface IAuthenticateDtoResult
    {
        AuthenticationStatus Status { get; }
        int? Id { get; }
        string Email { get; }
        string Name { get; }
        string Key { get; }
    }
}