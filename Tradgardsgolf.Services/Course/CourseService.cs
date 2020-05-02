using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tradgardsgolf.Core.Infrastructure.Course;
using Tradgardsgolf.Core.Services.Course;

namespace Tradgardsgolf.Services.Course
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public ICourseModelResult Add(ICourseAddModel model)
        {
            var course = _courseRepository.Add(new CourseAddDto(model));

            return new CourseModelResult(course);
        }

        public IEnumerable<ICourseModelResult> ListAll()
        {
            return _courseRepository.ListAll().Select(x => new CourseModelResult(x));
        }
    }

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

        public CourseModelResult(ICourseDtoResult course)
        {
            _course = course;
            CreatedBy = new CourseCreatedByModelResult(course.CreatedBy);
        }
    }

    public class CourseCreatedByModelResult : ICourseCreatedByModelResult
    {
        private readonly ICourseCreatedByDtoResult _createdBy;



        public int Id => _createdBy.Id;
        public string Name => _createdBy.Name;

        public CourseCreatedByModelResult(ICourseCreatedByDtoResult createdBy)
        {
            _createdBy = createdBy;
        }
    }

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
