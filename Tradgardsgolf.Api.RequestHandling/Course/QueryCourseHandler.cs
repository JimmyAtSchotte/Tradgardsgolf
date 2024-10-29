using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Api.RequestHandling.Course;

public class QueryCourseHandler(
    IRepository<Core.Entities.Course> courses,
    IResponseFactory<CourseResponse, Core.Entities.Course> courseResponseFactory)
    : IRequestHandler<QueryCourse, CourseResponse>
{
    public async Task<CourseResponse> Handle(QueryCourse request, CancellationToken cancellationToken)
    {
        var course = await courses.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(request.Id), cancellationToken);

        return courseResponseFactory.Create(course);
    }
}