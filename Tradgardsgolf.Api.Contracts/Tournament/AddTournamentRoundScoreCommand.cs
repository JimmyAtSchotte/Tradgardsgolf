using MediatR;

namespace Tradgardsgolf.Contracts.Tournament
{
    public class AddTournamentRoundScoreCommand : IRequest
    {
        public int TournamentId { get; set; }
        public int RoundId { get; set; }
    }
}