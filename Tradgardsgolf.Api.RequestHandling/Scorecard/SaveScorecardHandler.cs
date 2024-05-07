using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Scorecard;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling.Scorecard
{
    public class SaveScorecardHandler(IRepository<Core.Entities.Course> courseRepository)
        : IRequestHandler<SaveScorecardCommand, ScorecardResponse>
    {
        public async Task<ScorecardResponse> Handle(SaveScorecardCommand request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
            var scorecard = course.CreateScorecard();

            foreach (var playerScore in request.PlayerScores)
                scorecard.AddPlayerScores(playerScore.Name, playerScore.HoleScores.ToArray());

            await courseRepository.UpdateAsync(course, cancellationToken);

            return new ScorecardResponse()
            {
                Id = scorecard.Id,
                CourseId = course.Id,
                PlayerScores = request.PlayerScores
            };
        }
    }
}

