using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Scorecard;

public record PlayerScore
{
    public string Name { get; init; } = string.Empty;
    public IEnumerable<int> HoleScores { get; init; } = new List<int>();
}