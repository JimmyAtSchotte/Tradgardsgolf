using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics
{
    public record RoundResponse
    {
        public DateTime Date { get; init; }
        public IEnumerable<RoundScoreResponse> Scores { get; init; }
    }
}