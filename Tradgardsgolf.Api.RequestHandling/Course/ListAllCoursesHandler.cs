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
        IRepository<Core.Entities.Course> courses,
        IResponseFactory<CourseResponse, Core.Entities.Course> courseResponseFactory) 
        : IRequestHandler<ListAllCoursesCommand, IEnumerable<CourseResponse>>
    {
        public async Task<IEnumerable<CourseResponse>> Handle(ListAllCoursesCommand request, CancellationToken cancellationToken)
        {
            var list = await courses.ListAsync(cancellationToken);

            return list.Select(courseResponseFactory.Create).ToList();
        }
    }
}

