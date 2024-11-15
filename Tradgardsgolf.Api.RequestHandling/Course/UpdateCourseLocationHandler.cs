using System.Diagnostics.CodeAnalysis;
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

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class UpdateCourseLocationHandler : IRequestHandler<UpdateCourseLocationCommand, CourseResponse>
{
    private readonly IResponseFactory<CourseResponse, Core.Entities.Course> _responseFactory;
    private readonly IRepository _repository;
    private readonly IAuthenticationService _authenticationService;
    
    public UpdateCourseLocationHandler(IResponseFactory<CourseResponse, Core.Entities.Course> responseFactory, IRepository repository, IAuthenticationService authenticationService)
    {
        _responseFactory = responseFactory;
        _repository = repository;
        _authenticationService = authenticationService;
    }


    public async Task<CourseResponse> Handle(UpdateCourseLocationCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(request.Id), cancellationToken);
        var user = _authenticationService.RequireAuthenticatedUser();
        
        if (user?.UserId != course.OwnerGuid)
            throw new ForbiddenException();
        
        course.Longitude = request.Longitude;
        course.Latitude = request.Latitude;
        
        await _repository.UpdateAsync(course, cancellationToken);
        
        return _responseFactory.Create(course);
    }
}