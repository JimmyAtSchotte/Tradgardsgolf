using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling.Course
{
    public class ListAllCoursesHandler(
        IRepository<Core.Entities.Course> repository,
        IResponseFactory<CourseResponse, Core.Entities.Course> courseResponseFactory) 
        : IRequestHandler<ListAllCoursesCommand, IEnumerable<CourseResponse>>
    {
        public async Task<IEnumerable<CourseResponse>> Handle(ListAllCoursesCommand request, CancellationToken cancellationToken)
        {
            var courses = await repository.ListAsync(cancellationToken);

            return courses.Select(courseResponseFactory.Create).ToList();
        }
    }
}

