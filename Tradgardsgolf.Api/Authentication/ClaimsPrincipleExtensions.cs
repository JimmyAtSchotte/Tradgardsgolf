using System;
using System.Linq;
using System.Security.Claims;

namespace Tradgardsgolf.Api.Authentication;

public static class ClaimsPrincipleExtensions
{
    private static readonly string[] UserIdClaimAliases = new[]
    {
        "oid", "objectidentifier"
    };
    
    public static bool TryGetUserId(this ClaimsPrincipal user, out Guid userId)
    {
        userId = Guid.Empty;
        
        return Guid.TryParse(user.FindFirst(u => UserIdClaimAliases.Any(alias => u.Type.Contains((string)alias)))?.Value, out userId);
    }
}