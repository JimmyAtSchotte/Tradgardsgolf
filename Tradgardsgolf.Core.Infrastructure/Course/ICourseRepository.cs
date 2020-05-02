using System.Collections.Generic;

namespace Tradgardsgolf.Core.Infrastructure.Course
{
    public interface ICourseRepository
    {
        IEnumerable<ICourseDtoResult> ListAll();

        ICourseDtoResult Add(ICourseAddDto dto);

    }

    public interface ICourseAddDto
    {
        string Name { get; }
        int Holes { get; }
        double Longitude { get; }
        double Latitude { get; }
        int CreatedBy { get; }
    }
}
