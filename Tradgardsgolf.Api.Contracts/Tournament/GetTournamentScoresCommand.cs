using System;
using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Tournament
{
    public class GetTournamentScoresCommand : IRequest<IEnumerable<TournamentScore>>
    {
        public Guid TournamentId { get; set; }
    }
}