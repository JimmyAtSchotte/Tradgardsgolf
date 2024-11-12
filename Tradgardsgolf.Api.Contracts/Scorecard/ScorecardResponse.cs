using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Scorecard;

public record ScorecardResponse
{
    public Guid Id { get; init; }
    public IEnumerable<PlayerScore> PlayerScores { get; init; }
}