namespace Tradgardsgolf.Core.Auth;

public interface IAuthenticationService
{
    AuthenticatedUser RequireAuthenticatedUser();
}