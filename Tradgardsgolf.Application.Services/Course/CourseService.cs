using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure.Course;
using Tradgardsgolf.Core.Infrastructure.Round;
using Tradgardsgolf.Core.Services.Course;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Services.Course
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IRoundRepository _roundRepository;

        public CourseService(ICourseRepository courseRepository, IRoundRepository roundRepository)
        {
            _courseRepository = courseRepository;
            _roundRepository = roundRepository;
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

        public IEnumerable<ICoursePlayerModelResult> Players(ICoursePlayerModel model)
        {
            return _courseRepository.Players(new CoursePlayerDto(model)).Select(x => new CoursePlayerModelResult(x));
        }

        public async Task<IEnumerable<Round>> ListAllRounds(int courseId)
        {
            return await _roundRepository.ListAsync(new AllRoundsByCourse(courseId));
        }
    }


    public class CoursePlayerDto : ICoursePlayerDto
    {
        private readonly ICoursePlayerModel _model;        
        public int Id => _model.Id;
        public CoursePlayerDto(ICoursePlayerModel model)
        {
            _model = model;
        }
    }

    public class CoursePlayerModelResult : ICoursePlayerModelResult
    {
        private readonly ICoursePlayerDtoResult _player;

        public string Name => _player.Name;

        public CoursePlayerModelResult(ICoursePlayerDtoResult player)
        {
            _player = player;
        }
    }
}
