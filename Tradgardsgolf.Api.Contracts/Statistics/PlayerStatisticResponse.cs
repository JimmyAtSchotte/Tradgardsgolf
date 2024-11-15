using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics;

public class PlayerStatisticResponse
{
    public string Name { get; init; } = string.Empty;
    public double AverageScore { get; init; }
    public int TimesPlayed { get; init; }
    public IEnumerable<HoleStatisticResponse> HoleStatistics { get; init; } = new List<HoleStatisticResponse>();
    public BestScoreResponse? BestScore { get; init; }
}