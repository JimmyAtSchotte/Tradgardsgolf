using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Tournament;

public class TournamentResultResponse
{
    public IEnumerable<PlayerTournamentScore> PlayerTournamentScores { get; set; } = new List<PlayerTournamentScore>();
    public string Name { get; set; } = string.Empty;
}