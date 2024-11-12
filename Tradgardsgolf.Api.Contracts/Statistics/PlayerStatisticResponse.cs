using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics;

public class PlayerStatisticResponse
{
    public string Name { get; init; }
    public double AverageScore { get; init; }
    public int TimesPlayed { get; init; }
    public IEnumerable<HoleStatisticResponse> HoleStatistics { get; init; }

    public BestScoreResponse BestScore { get; init; }
}