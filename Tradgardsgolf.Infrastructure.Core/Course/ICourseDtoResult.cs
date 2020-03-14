using System;
using Tradgardsgolf.Core.Infrastructure.Player;

namespace Tradgardsgolf.Core.Infrastructure.Course
{
    public  interface ICourseAdapter
    {        
        int Id { get;  }
        String Name { get;  }
        int Holes { get; }
        double Longitude { get;  }
        double Latitude { get;  }
        int CreatedById { get; }
        IPlayerAdapter CreatedBy { get; }
        DateTime Created { get;  }
    }
}
