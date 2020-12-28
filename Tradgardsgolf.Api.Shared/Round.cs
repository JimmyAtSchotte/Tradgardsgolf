using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Api.Shared
{
    public class Round
    {
        public DateTime Date { get; set; }
        public IEnumerable<RoundScore> Scores { get; set; }
    }
}