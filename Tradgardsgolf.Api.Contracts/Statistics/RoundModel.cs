using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics
{
    public record RoundModel
    {
        public DateTime Date { get; init; }
        public IEnumerable<RoundScoreModel> Scores { get; init; }
    }
}