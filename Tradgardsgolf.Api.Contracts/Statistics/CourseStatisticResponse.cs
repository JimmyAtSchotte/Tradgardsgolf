using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics;

public record CourseStatisticResponse
{
    public IEnumerable<CourseSeasonResposne> Seasons { get; init; }
    public IEnumerable<PlayerStatisticResponse> PlayerStatistics { get; set; }
}