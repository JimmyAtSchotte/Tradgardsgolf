using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Tournament;

public class PlayerTournamentScore
{
    public string Name { get; set; }
    public IEnumerable<int> Results { get; set; }
    public int Total { get; set; }
}