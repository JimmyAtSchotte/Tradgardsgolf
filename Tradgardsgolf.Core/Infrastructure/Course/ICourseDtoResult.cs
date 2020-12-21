using System;
using Tradgardsgolf.Core.Infrastructure.Player;

namespace Tradgardsgolf.Core.Infrastructure.Course
{
    public  interface ICourseDtoResult
    {        
        int Id { get;  }
        string Name { get;  }
        int Holes { get; }
        double Longitude { get;  }
        double Latitude { get;  }        
        ICourseCreatedByDtoResult CreatedBy { get; }
        DateTime Created { get;  }
    }

    public interface ICourseCreatedByDtoResult
    {
        int Id { get; }
        string Name { get; }
    }
}
