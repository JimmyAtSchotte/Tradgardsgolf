using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics
{
    public record CourseStatistic
    {
        public IEnumerable<RoundModel> Rounds { get; init; }
    }
}