using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Api.Shared
{
    public class RoundModel
    {
        public DateTime Date { get; set; }
        public IEnumerable<RoundScoreModel> Scores { get; set; }
    }
}