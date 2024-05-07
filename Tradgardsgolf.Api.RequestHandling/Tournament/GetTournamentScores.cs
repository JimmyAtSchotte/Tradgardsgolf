using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Tournament
{
    public class GetTournamentScores(IRepository<Tradgardsgolf.Core.Entities.Scorecard> scorecards)
        : IRequestHandler<GetTournamentScoresCommand, IEnumerable<TournamentScore>>
    {
        public async Task<IEnumerable<TournamentScore>> Handle(GetTournamentScoresCommand request, CancellationToken cancellationToken)
        {
            var tournament = await scorecards.ListAsync(new ByTournament(request.TournamentId), cancellationToken);
            
            return tournament.SelectMany(x => x.Scores)
                .GroupBy(x => x.Key)
                .Select(x => new TournamentScore()
                {
                    Name = x.Key,
                    Score = x.Sum(scores => scores.Value.Sum(score => score))
                }) ?? new List<TournamentScore>();
        }
    }
}