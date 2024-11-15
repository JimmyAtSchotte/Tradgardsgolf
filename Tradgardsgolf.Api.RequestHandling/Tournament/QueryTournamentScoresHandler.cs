using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Tournament;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class QueryTournamentScoresHandler(IRepository repository)
    : IRequestHandler<QueryTournamentScores, IEnumerable<TournamentScore>>
{
    public async Task<IEnumerable<TournamentScore>> Handle(QueryTournamentScores request,
        CancellationToken cancellationToken)
    {
        var tournament = await repository.ListAsync(Specs.Scorecard.ByTournament(request.TournamentId), cancellationToken);

        return tournament.SelectMany(x => x.Scores)
            .GroupBy(x => x.Key)
            .Select(x => new TournamentScore
            {
                Name = x.Key,
                Score = x.Sum(scores => scores.Value.Sum(score => score))
            });
    }
}