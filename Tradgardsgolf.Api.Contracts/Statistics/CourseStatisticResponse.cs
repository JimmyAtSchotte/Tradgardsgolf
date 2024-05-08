using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics;

public record CourseStatisticResponse
{
    public IEnumerable<ScorecardResponse> Scorecards { get; init; }
}