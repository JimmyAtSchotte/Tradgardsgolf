using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using MediatR;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Exceptions;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Course;

public class UpdatePlayerNameHandler : IRequestHandler<UpdatePlayerNameCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IAuthenticationService _authenticationService;
    
    public UpdatePlayerNameHandler(IRepository repository, IAuthenticationService authenticationService)
    {
        _repository = repository;
        _authenticationService = authenticationService;
    }

    public async Task<Unit> Handle(UpdatePlayerNameCommand request, CancellationToken cancellationToken)
    {
        var user = _authenticationService.RequireAuthenticatedUser();
        var course = await _repository.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(request.CourseId), cancellationToken);

        if (user.UserId != course.OwnerGuid)
            throw new ForbiddenException();
        
        var scorecards = await _repository.ListAsync(Specs.Scorecard.ByCourse(course.Id), cancellationToken);
        var updatedScorecards = scorecards.Where(s => s.ReplaceName(request.OldName, request.NewName)).ToArray();
        
        if(updatedScorecards.Any())
            await _repository.UpdateRangeAsync(updatedScorecards, cancellationToken);
   
        return Unit.Value;
    }
}