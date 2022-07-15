using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Tasks
{
    public class AddTournamentRoundScore : IRequestHandler<AddTournamentRoundScoreCommand>
    {
        private readonly IRepository<Tradgardsgolf.Core.Entities.TournamentRound> _repository;

        public AddTournamentRoundScore(IRepository<Tradgardsgolf.Core.Entities.TournamentRound> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddTournamentRoundScoreCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(new TournamentRound()
            {
                TournamentId = request.TournamentId,
                RoundId = request.RoundId
            });
            
            return await Unit.Task;
        }
    }
}