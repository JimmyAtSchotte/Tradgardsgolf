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
    public class GetCourseHandler(IRepository<Course> repository) : IRequestHandler<GetCourseCommand, CourseResponse>
    {
        public async Task<CourseResponse> Handle(GetCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await repository.GetByIdAsync(request.Id, cancellationToken);

            return new CourseResponse()
            {
                Created = course.Created,
                Holes = course.Holes,
                Id = course.Id,
                Image = course.Image,
                Latitude = course.Latitude,
                Longitude = course.Longitude,
                Name = course.Name,
                ScoreReset = course.ScoreReset
            };
        }
    }
}

