using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Scorecard;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Course;

namespace Tradgardsgolf.Api.RequestHandling.Scorecard;

public class SaveScorecardHandler(IRepository<Core.Entities.Course> courses)
    : IRequestHandler<SaveScorecardCommand, ScorecardResponse>
{
    public async Task<ScorecardResponse> Handle(SaveScorecardCommand request, CancellationToken cancellationToken)
    {
        var course = await courses.FirstOrDefaultAsync(Specs.Course.ById(request.CourseId), cancellationToken);
        var scorecard = course.CreateScorecard();

        foreach (var playerScore in request.PlayerScores)
            scorecard.AddPlayerScores(playerScore.Name, playerScore.HoleScores.ToArray());

        await courses.UpdateAsync(course, cancellationToken);

        return new ScorecardResponse
        {
            Id = scorecard.Id,
            CourseId = course.Id,
            PlayerScores = request.PlayerScores
        };
    }
}