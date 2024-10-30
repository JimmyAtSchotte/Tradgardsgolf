using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling.Course;

public class QueryAllCoursesHandler(
    IRepository repository,
    IResponseFactory<CourseResponse, Core.Entities.Course> courseResponseFactory)
    : IRequestHandler<QueryAllCourses, IEnumerable<CourseResponse>>
{
    public async Task<IEnumerable<CourseResponse>> Handle(QueryAllCourses request,
        CancellationToken cancellationToken)
    {
        var list = await repository.ListAsync<Core.Entities.Course>(cancellationToken);

        return list.Select(courseResponseFactory.Create).ToList();
    }
}