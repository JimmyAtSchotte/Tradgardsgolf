using Tradgardsgolf.Core.Infrastructure.Course;
using Tradgardsgolf.Core.Services.Course;

namespace Tradgardsgolf.Services.Course
{
    public class CourseAddDto : ICourseAddDto
    {
        private readonly ICourseAddModel _course;

        public string Name => _course.Name;
        public int Holes => _course.Holes;
        public double Longitude => _course.Longitude;
        public double Latitude => _course.Latitude;
        public int CreatedBy => _course.CreatedBy;

        public CourseAddDto(ICourseAddModel course)
        {
            _course = course;
        }
    }
}
