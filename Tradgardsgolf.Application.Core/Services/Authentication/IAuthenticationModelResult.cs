using Tradgardsgolf.Core.Enums;

namespace Tradgardsgolf.Core.Services.Authentication
{
    public interface IAuthenticationModelResult
    {
        AuthenticationStatus Status { get; }
        int? Id { get; }
        string Email { get; }
        string Name { get; }
        string Key { get; }
    }
}