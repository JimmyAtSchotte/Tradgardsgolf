using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Tournament;

public class PlayerTournamentScore
{
    public string Name { get; init; }
    public IEnumerable<int> Results { get; init; } = new List<int>();
    public int Total { get; init; }
}