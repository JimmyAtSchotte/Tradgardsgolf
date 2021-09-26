using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics
{
    public class RoundModel
    {
        public DateTime Date { get; set; }
        public IEnumerable<RoundScoreModel> Scores { get; set; }
    }
}