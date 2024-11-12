using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics;

public record CourseStatisticResponse
{
    public IEnumerable<CourseSeasonResposne> Seasons { get; init; }
    public IEnumerable<PlayerStatisticResponse> PlayerStatistics { get; set; }
}

public class PlayerStatisticResponse
{
    public string Name { get; init; }
    public double AverageScore { get; init; }
    public int TimesPlayed { get; init; }
    public IEnumerable<HoleStatisticResponse> HoleStatistics { get; init; }

    public BestScoreResponse BestScore { get; init; }
}

public class HoleStatisticResponse
{
    public double AverageScore { get; init; }
    public int HoleInOnes { get; init; }
}

public class BestScoreResponse
{
    public int Score { get; init; }
    public DateTime Date { get; init; }
}



public class CourseSeasonResposne
{
    public int Season { get; init; }
    public Dictionary<string, List<int>> Players { get; init; }
}