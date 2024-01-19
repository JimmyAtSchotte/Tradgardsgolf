using System;
using System.Security.Claims;

namespace Tradgardsgolf.BlazorWasm;

public static class ClaimsPrincipleExtensions
{
    public static bool TryGetUserId(this ClaimsPrincipal user, out Guid userId)
    {
        userId = Guid.Empty;
        
        return Guid.TryParse(user.FindFirst(u => u.Type.Contains("oid"))?.Value, out userId);
    }
}