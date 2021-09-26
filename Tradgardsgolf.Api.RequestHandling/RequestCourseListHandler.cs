using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Players;
using Tradgardsgolf.Core.Services.Course;

namespace Tradgardsgolf.Tasks
{
    public class RequestCourseListHandler : IRequestHandler<RequestCourseList, IEnumerable<Course>>
    {
        private readonly ICourseService _courseService;

        public RequestCourseListHandler(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IEnumerable<Course>> Handle(RequestCourseList request, CancellationToken cancellationToken)
        {
            var courses = _courseService.ListAll();

            return courses.Select(x => new Course()
            {
                Created = x.Created,
                Holes = x.Holes,
                Id = x.Id,
                Image = x.Image,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Name = x.Name,
                ScoreReset = x.ScoreReset,
                CreatedBy = new Player()
                {
                    Id = x.CreatedBy.Id,
                    Name = x.Name
                }
            });
        }
    }
}

