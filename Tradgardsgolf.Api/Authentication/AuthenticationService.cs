using Microsoft.AspNetCore.Http;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Exceptions;

namespace Tradgardsgolf.Api.Authentication;

public class AuthenticationService(IHttpContextAccessor httpContextAccessor) : IAuthenticationService
{
    public AuthenticatedUser RequireAuthenticatedUser()
    {
        var context = httpContextAccessor.HttpContext;

        if (context is null)
            throw new UnauthorizedException();

        if (!context.User.TryGetUserId(out var userId))
            throw new UnauthorizedException();

        return new AuthenticatedUser
        {
            UserId = userId
        };
    }
}