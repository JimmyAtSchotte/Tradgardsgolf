using MediatR;

namespace Tradgardsgolf.Contracts.Tournament
{
    public class AddTournamentRoundScoreCommand : IRequest<Unit>
    {
        public int TournamentId { get; set; }
        public int RoundId { get; set; }
    }
}