using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics;

public record CourseStatisticResponse
{
    public IEnumerable<CourseSeasonResposne> Seasons { get; init; } = new List<CourseSeasonResposne>();
    public IEnumerable<PlayerStatisticResponse> PlayerStatistics { get; init; } = new List<PlayerStatisticResponse>();
}