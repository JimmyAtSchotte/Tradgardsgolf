using System;

namespace Tradgardsgolf.Core.Auth;

public record AuthenticatedUser
{
    public Guid UserId { get; init; }
}