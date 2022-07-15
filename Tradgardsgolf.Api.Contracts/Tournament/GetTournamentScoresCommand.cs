using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Tournament
{
    public class GetTournamentScoresCommand : IRequest<IEnumerable<TournamentScore>>
    {
        public int TournamentId { get; set; }
    }
}