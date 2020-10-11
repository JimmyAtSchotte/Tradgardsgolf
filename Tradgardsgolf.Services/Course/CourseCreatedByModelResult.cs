using Tradgardsgolf.Core.Infrastructure.Course;
using Tradgardsgolf.Core.Services.Course;

namespace Tradgardsgolf.Services.Course
{
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
}
