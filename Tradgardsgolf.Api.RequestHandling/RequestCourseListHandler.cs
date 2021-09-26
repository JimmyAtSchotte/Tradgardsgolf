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
    public class RequestCourseListHandler : IRequestHandler<RequestCourseList, IEnumerable<Course>>
    {
        private readonly ICourseRepository _courseRepository;

        public RequestCourseListHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Course>> Handle(RequestCourseList request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.ListAsync();

            return courses.Select(x => new Course()
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

