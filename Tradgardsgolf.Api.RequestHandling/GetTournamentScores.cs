using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Api.RequestHandling
{
    public class GetTournamentScores(IRepository<Tradgardsgolf.Core.Entities.Tournament> repository)
        : IRequestHandler<GetTournamentScoresCommand, IEnumerable<TournamentScore>>
    {
        public async Task<IEnumerable<TournamentScore>> Handle(GetTournamentScoresCommand request, CancellationToken cancellationToken)
        {
            var tournament = await repository.FirstOrDefaultAsync(new TournamentScores(request.TournamentId), cancellationToken);
            
            return tournament?.TournamentRounds.SelectMany(x => x.Round.RoundScores)
                .GroupBy(x => x.Player.Name)
                .Select(x => new TournamentScore()
                {
                    Name = x.Key,
                    Score = x.Sum(scores => scores.Score)
                }) ?? new List<TournamentScore>();
        }
    }
}