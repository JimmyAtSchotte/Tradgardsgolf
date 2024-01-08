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
    public class UpdateCourseHandler(IRepository<Course> repository) : IRequestHandler<UpdateCourseCommand, CourseResponse>
    {
        public async Task<CourseResponse> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await repository.GetByIdAsync(request.Id, cancellationToken);
            course.Name = request.Name;
            course.Image = request.Image;
            await repository.UpdateAsync(course, cancellationToken);

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

