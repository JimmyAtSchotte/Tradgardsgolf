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

public class UpdateCourseLocationHandler : IRequestHandler<UpdateCourseLocationCommand, CourseResponse>
{
    private readonly IResponseFactory<CourseResponse, Core.Entities.Course> _responseFactory;
    private readonly IRepository<Core.Entities.Course> _courseRepository;
    private readonly IAuthenticationService _authenticationService;
    
    public UpdateCourseLocationHandler(IResponseFactory<CourseResponse, Core.Entities.Course> responseFactory, IRepository<Core.Entities.Course> courseRepository, IAuthenticationService authenticationService)
    {
        _responseFactory = responseFactory;
        _courseRepository = courseRepository;
        _authenticationService = authenticationService;
    }


    public async Task<CourseResponse> Handle(UpdateCourseLocationCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(request.Id), cancellationToken);
        var user = _authenticationService.RequireAuthenticatedUser();
        
        if (user?.UserId != course.OwnerGuid)
            throw new ForbiddenException();
        
        course.Longitude = request.Longitude;
        course.Latitude = request.Latitude;
        
        await _courseRepository.UpdateAsync(course, cancellationToken);
        
        return _responseFactory.Create(course);
    }
}