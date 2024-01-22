using System;

namespace Tradgardsgolf.Core.Auth;

public record AuthenticatedUser
{
    public Guid UserId { get; set; }
}

public interface IAuthenticationService
{
    AuthenticatedUser RequireAuthenticatedUser();
}