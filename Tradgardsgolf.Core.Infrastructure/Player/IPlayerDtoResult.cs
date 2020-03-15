using System;

namespace Tradgardsgolf.Core.Infrastructure.Player
{
    public interface IPlayerDtoResult
    {
        int Id { get; }
        string Email { get; }
        string Password { get;  }
        string Key { get;  }
        string Name { get; }
        DateTime Created { get; }

    }
}
