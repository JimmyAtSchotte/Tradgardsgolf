using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Tournament
{
    public class AddTournamentRoundScoreCommand : IRequest
    {
        public Guid TournamentId { get; set; }
        public Guid ScorecardId { get; set; }
    }
}