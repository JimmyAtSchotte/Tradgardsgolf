using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics;

public class CourseSeasonResposne
{
    public int Season { get; init; }
    public Dictionary<string, List<int>> Players { get; init; }
}