using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Scorecard;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Api.RequestHandling.Scorecard;

public class SaveScorecardHandler(IRepository repository)
    : IRequestHandler<SaveScorecardCommand, ScorecardResponse>
{
    public async Task<ScorecardResponse> Handle(SaveScorecardCommand request, CancellationToken cancellationToken)
    {
        var course = await repository.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(request.CourseId), cancellationToken);
        var scorecard = course.CreateScorecard();

        foreach (var playerScore in request.PlayerScores)
            scorecard.AddPlayerScores(playerScore.Name, playerScore.HoleScores.ToArray());

        await repository.UpdateAsync(course, cancellationToken);

        return new ScorecardResponse
        {
            Id = scorecard.Id,
            CourseId = course.Id,
            PlayerScores = request.PlayerScores
        };
    }
}