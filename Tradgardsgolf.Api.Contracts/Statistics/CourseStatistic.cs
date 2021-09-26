using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics
{
    public class CourseStatistic
    {
        public IEnumerable<RoundModel> Rounds { get; set; }
    }
}