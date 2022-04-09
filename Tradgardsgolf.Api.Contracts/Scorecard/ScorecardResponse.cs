using System.Collections.Generic;

namespace Tradgardsgolf.Contracts.Scorecard
{
    public record ScorecardResponse
    {
        public int Id { get; init; }
        public IEnumerable<PlayerScore> PlayerScores { get; init; }
        public int CourseId { get; init; }
    }
}