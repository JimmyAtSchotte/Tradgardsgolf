using System;
using Tradgardsgolf.Core.Infrastructure.Player;

namespace Tradgardsgolf.Core.Infrastructure.Course
{
    public  interface ICourseDtoResult
    {        
        int Id { get;  }
        String Name { get;  }
        int Holes { get; }
        double Longitude { get;  }
        double Latitude { get;  }
        int CreatedById { get; }
        IPlayerDtoResult CreatedBy { get; }
        DateTime Created { get;  }
    }
}
