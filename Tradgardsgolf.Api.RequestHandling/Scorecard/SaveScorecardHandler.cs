using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Scorecard;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling.Scorecard;

public class SaveScorecardHandler(IRepository repository, IMediator mediator)
    : IRequestHandler<SaveScorecardCommand, ScorecardResponse>
{
    public async Task<ScorecardResponse> Handle(SaveScorecardCommand request, CancellationToken cancellationToken)
    {
        var scorecard = Core.Entities.Scorecard.Create(request.CourseId, request.Revision);

        foreach (var playerScore in request.PlayerScores)
            scorecard.AddPlayerScores(playerScore.Name, playerScore.HoleScores.ToArray());

        await repository.AddAsync(scorecard, cancellationToken);

        await mediator.Publish(new ScorecardSavedNotification(scorecard), cancellationToken);

        return new ScorecardResponse
        {
            Id = scorecard.Id,
            PlayerScores = request.PlayerScores
        };
    }
}