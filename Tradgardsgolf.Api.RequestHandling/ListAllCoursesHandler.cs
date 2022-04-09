using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Players;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Tasks
{
    public class ListAllCoursesHandler : IRequestHandler<ListAllCoursesCommand, IEnumerable<CourseResponse>>
    {
        private readonly ICourseRepository _courseRepository;

        public ListAllCoursesHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseResponse>> Handle(ListAllCoursesCommand request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.ListAsync();

            return courses.Select(x => new CourseResponse()
            {
                Created = x.Created,
                Holes = x.Holes,
                Id = x.Id,
                Image = "images/grass.jpg",
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Name = x.Name,
                ScoreReset = x.ScoreReset
            });
        }
    }
}

