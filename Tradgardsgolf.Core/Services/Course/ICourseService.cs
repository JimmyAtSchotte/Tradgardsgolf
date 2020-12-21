using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Core.Services.Course
{
    public interface ICourseService
    {
        IEnumerable<ICourseModelResult> ListAll();
        ICourseModelResult Add(ICourseAddModel model);
        IEnumerable<ICoursePlayerModelResult> Players(ICoursePlayerModel model);

    }

    public interface ICoursePlayerModelResult
    {
        string Name { get; }        
    }

    public interface ICoursePlayerModel
    {
        int Id { get; }
    }

    public interface ICourseModelResult
    {
        int Id { get; }
        string Name { get; }
        int Holes { get; }
        double Longitude { get; }
        double Latitude { get; }

        string Image { get; }
        ICourseCreatedByModelResult CreatedBy { get; }
        DateTime Created { get; }
    }

    public interface ICourseCreatedByModelResult
    {
        int Id { get; }
        string Name { get; }
    }

    public interface ICourseAddModel
    {
        string Name { get; }
        int Holes { get; }
        double Longitude { get; }
        double Latitude { get; }
        int CreatedBy { get; }
    }
}
