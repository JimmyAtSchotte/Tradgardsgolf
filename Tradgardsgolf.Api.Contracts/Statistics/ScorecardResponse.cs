using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics;

public record ScorecardResponse
{
    public DateTime Date { get; init; }
    public IEnumerable<HoleScoreResponse> Scores { get; init; }
}