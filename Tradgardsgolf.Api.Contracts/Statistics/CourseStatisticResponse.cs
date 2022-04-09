using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Statistics
{
    public record CourseStatisticResponse
    {
        public IEnumerable<RoundResponse> Rounds { get; init; }
    }
}