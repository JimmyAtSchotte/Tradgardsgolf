using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Tradgardsgolf.Core.Auth;

namespace Tradgardsgolf.Api;

public class AuthenticatedUser(IHttpContextAccessor httpContextAccessor) : IAuthenticatedUser
{
    public bool TryGetAuthenticatedUserId(out Guid userId)
    {
        return httpContextAccessor.HttpContext.User.TryGetUserId(out userId);
    }
}

public static class ClaimsPrincipleExtensions
{
    private static readonly string[] UserIdClaimAliases = new[]
    {
        "oid", "objectidentifier"
    };
    
    public static bool TryGetUserId(this ClaimsPrincipal user, out Guid userId)
    {
        userId = Guid.Empty;
        
        return Guid.TryParse(user.FindFirst(u => UserIdClaimAliases.Any(alias => u.Type.Contains(alias)))?.Value, out userId);
    }
}