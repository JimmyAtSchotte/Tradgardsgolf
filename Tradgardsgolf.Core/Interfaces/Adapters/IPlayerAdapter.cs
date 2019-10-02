using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Core.Interfaces.Adapters
{
    public interface IPlayerAdapter
    {
        int Id { get; }
        string Email { get; }
        string Password { get;  }
        string Key { get;  }
        string Name { get; }
        DateTime Created { get; }

    }
}
