using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Core.Interfaces.Adapters
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
