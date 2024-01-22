using System;

namespace Tradgardsgolf.Core.Auth;

public interface IAuthenticatedUser
{
    bool TryGetAuthenticatedUserId(out Guid userId);
}