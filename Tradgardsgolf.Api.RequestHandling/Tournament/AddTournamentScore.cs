using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling.Tournament
{
    public class AddTournamentRoundScore(IRepository<Core.Entities.Scorecard> scorecards)
        : IRequestHandler<AddTournamentRoundScoreCommand>
    {
        public async Task Handle(AddTournamentRoundScoreCommand request, CancellationToken cancellationToken)
        {
            var scorecard = await scorecards.GetByIdAsync(request.ScorecardId, cancellationToken);
            scorecard.TournamentId = request.TournamentId;
            await scorecards.UpdateAsync(scorecard, cancellationToken);
        }
    }
}