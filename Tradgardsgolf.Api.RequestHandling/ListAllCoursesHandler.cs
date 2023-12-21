using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling
{
    public class ListAllCoursesHandler(IRepository<Course> repository) : IRequestHandler<ListAllCoursesCommand, IEnumerable<CourseResponse>>
    {
        public async Task<IEnumerable<CourseResponse>> Handle(ListAllCoursesCommand request, CancellationToken cancellationToken)
        {
            var courses = await repository.ListAsync();

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

