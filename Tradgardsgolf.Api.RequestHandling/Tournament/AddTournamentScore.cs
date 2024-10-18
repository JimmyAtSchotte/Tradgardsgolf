using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Tournament;

public class AddTournamentRoundScoreHandler(IRepository<Core.Entities.Scorecard> scorecards)
    : IRequestHandler<AddTournamentRoundScoreCommand>
{
    public async Task Handle(AddTournamentRoundScoreCommand request, CancellationToken cancellationToken)
    {
        var scorecard = await scorecards.FirstOrDefaultAsync(new ById(request.ScorecardId), cancellationToken);
        scorecard.TournamentId = request.TournamentId;
        await scorecards.UpdateAsync(scorecard, cancellationToken);
    }
}