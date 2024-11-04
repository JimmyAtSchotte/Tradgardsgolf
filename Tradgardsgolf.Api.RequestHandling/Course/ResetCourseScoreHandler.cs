using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Exceptions;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Api.RequestHandling.Course;

public class ResetCourseScoreHandler : IRequestHandler<ResetCourseScoreCommand, CourseResponse>
{
    private readonly IRepository _repository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IResponseFactory<CourseResponse, Core.Entities.Course> _courseResponseFactory;

    public ResetCourseScoreHandler(IRepository repository, IAuthenticationService authenticationService, IResponseFactory<CourseResponse, Core.Entities.Course> courseResponseFactory)
    {
        _repository = repository;
        _authenticationService = authenticationService;
        _courseResponseFactory = courseResponseFactory;
    }

    public async Task<CourseResponse> Handle(ResetCourseScoreCommand request, CancellationToken cancellationToken)
    {
        var authenticatedUser = _authenticationService.RequireAuthenticatedUser();
        var course = await _repository.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(request.CourseId), cancellationToken);

        if (course?.OwnerGuid != authenticatedUser.UserId)
            throw new ForbiddenException();

        course.ScoreReset = request.ResetDate;
        
        await _repository.UpdateAsync(course, cancellationToken);

        return _courseResponseFactory.Create(course);
    }
}