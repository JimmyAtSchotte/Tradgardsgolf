using System;

namespace Tradgardsgolf.Core.Authentication;

public interface IAuthenticatedUser
{
    bool TryGetAuthenticatedUserId(out Guid userId);
}