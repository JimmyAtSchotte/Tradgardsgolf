using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Data;
using Tradgardsgolf.Core.Services.Course;

namespace Tradgardsgolf.Blazor.Pages
{
    public interface ICourseServiceAdapter
    {
        Task<IEnumerable<Course>> ListAll();
        Task<IEnumerable<Player>> Players(Course course);
    }

    public class CourseServiceAdapter : ICourseServiceAdapter
    {
        private readonly ICourseService _courseService;

        public CourseServiceAdapter(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IEnumerable<Course>> ListAll()
        {
            return await Task.Run(() => _courseService.ListAll().Select(x => Course.Create(x)));
        }

        public async Task<IEnumerable<Player>> Players(Course course)
        {
            return await Task.Run(() => _courseService.Players(new CoursePlayerModel(course)).Select(x => new Player()
            {
                Name = x.Name
            }));
        }
    }

    public class CoursePlayerModel : ICoursePlayerModel
    {
        private readonly Course _course;

        public int Id => _course.Id;

        public CoursePlayerModel(Course course)
        {
            _course = course;
        }
    }

}
