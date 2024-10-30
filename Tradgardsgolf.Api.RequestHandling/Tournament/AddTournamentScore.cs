using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Tournament;

public class AddTournamentRoundScoreHandler(IRepository repository)
    : IRequestHandler<AddTournamentRoundScoreCommand>
{
    public async Task Handle(AddTournamentRoundScoreCommand request, CancellationToken cancellationToken)
    {
        var scorecard = await repository.FirstOrDefaultAsync(Specs.ById<Core.Entities.Scorecard>(request.ScorecardId), cancellationToken);
        scorecard.TournamentId = request.TournamentId;
        await repository.UpdateAsync(scorecard, cancellationToken);
    }
}