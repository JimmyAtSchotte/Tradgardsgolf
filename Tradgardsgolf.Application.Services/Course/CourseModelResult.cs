using System;
using Tradgardsgolf.Core.Services.Course;

namespace Tradgardsgolf.Services.Course
{
    public class CourseModelResult : ICourseModelResult
    {
        private readonly ICourseDtoResult _course;

        public int Id => _course.Id;
        public string Name => _course.Name;
        public int Holes => _course.Holes;
        public double Longitude => _course.Longitude;
        public double Latitude => _course.Latitude;
        public ICourseCreatedByModelResult CreatedBy { get; }
        public DateTime Created => _course.Created;
        
        public DateTime? ScoreReset => _course.ScoreReset;

        public string Image => "images/grass.jpg";

        public CourseModelResult(ICourseDtoResult course)
        {
            _course = course;
            CreatedBy = new CourseCreatedByModelResult(course.CreatedBy);
        }
    }
}
