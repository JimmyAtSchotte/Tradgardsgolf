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
    public class GetTournamentScores : IRequestHandler<GetTournamentScoresCommand, IEnumerable<TournamentScore>>
    {
        private readonly IRepository<Tradgardsgolf.Core.Entities.Tournament> _repository;

        public GetTournamentScores(IRepository<Tradgardsgolf.Core.Entities.Tournament> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TournamentScore>> Handle(GetTournamentScoresCommand request, CancellationToken cancellationToken)
        {
            var tournament = await _repository.GetBySpecAsync(new TournamentScores(request.TournamentId));
            
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