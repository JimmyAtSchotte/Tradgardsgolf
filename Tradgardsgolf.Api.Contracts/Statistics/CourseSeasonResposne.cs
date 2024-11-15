using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics;

public class CourseSeasonResposne
{
    public string Season { get; init; } = string.Empty;
    public Dictionary<string, List<int>> Players { get; init; } = new();
}