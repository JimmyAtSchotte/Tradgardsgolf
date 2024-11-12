using System;

namespace Tradgardsgolf.Contracts.Statistics;

public class BestScoreResponse
{
    public int Score { get; init; }
    public DateTime Date { get; init; }
}