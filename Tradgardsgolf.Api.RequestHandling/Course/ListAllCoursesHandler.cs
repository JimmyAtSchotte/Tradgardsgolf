using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling.Course
{
    public class ListAllCoursesHandler(IRepository<Core.Entities.Course> repository) : IRequestHandler<ListAllCoursesCommand, IEnumerable<CourseResponse>>
    {
        public async Task<IEnumerable<CourseResponse>> Handle(ListAllCoursesCommand request, CancellationToken cancellationToken)
        {
            var courses = await repository.ListAsync(cancellationToken);

            return courses.Select(course => new CourseResponse()
            {
                Created = course.Created,
                Holes = course.Holes,
                Id = course.Id,
                ImageReference = ImageReference.Create(course.Image),
                Latitude = course.Latitude,
                Longitude = course.Longitude,
                Name = course.Name,
                ScoreReset = course.ScoreReset
            }).ToList();
        }
    }
}

