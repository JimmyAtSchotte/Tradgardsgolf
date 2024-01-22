using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling.Course
{
    public class ClaimOwnershipHandler(IRepository<Core.Entities.Course> repository,
        IResponseFactory<CourseResponse, Core.Entities.Course> courseResponseFactory,
        IAuthenticatedUser authenticatedUser) 
        : IRequestHandler<ClaimOwnership, CourseResponse>
    {
        public async Task<CourseResponse> Handle(ClaimOwnership request, CancellationToken cancellationToken)
        {
            if (!authenticatedUser.TryGetAuthenticatedUserId(out var userId))
                throw new UnauthorizedException();
            
            var course = await repository.GetByIdAsync(request.Id, cancellationToken);
            
            if(course.OwnerGuid != Guid.Empty)
                return courseResponseFactory.Create(course);
            
            course.OwnerGuid = userId;
            
            await repository.UpdateAsync(course, cancellationToken);
            
            return courseResponseFactory.Create(course);
        }
    }
}

