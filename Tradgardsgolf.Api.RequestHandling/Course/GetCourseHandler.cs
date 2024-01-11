using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling.Course
{
    public class GetCourseHandler(
        IRepository<Core.Entities.Course> repository,
        IResponseFactory<CourseResponse, Core.Entities.Course> courseResponseFactory) 
        : IRequestHandler<GetCourseCommand, CourseResponse>
    {
        public async Task<CourseResponse> Handle(GetCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await repository.GetByIdAsync(request.Id, cancellationToken);

            return courseResponseFactory.Create(course);
        }
    }
}

