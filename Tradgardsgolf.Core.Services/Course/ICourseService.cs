using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Core.Services.Course
{
    public interface ICourseService
    {
        IEnumerable<ICourseModelResult> ListAll();
        ICourseModelResult Add(ICourseAddModel model);

    }

    public interface ICourseModelResult
    {
        int Id { get; }
        string Name { get; }
        int Holes { get; }
        double Longitude { get; }
        double Latitude { get; }
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
