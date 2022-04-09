using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Scorecard
{
    public record SaveScorecardCommand : IRequest<ScorecardResponse>
    {
        public IEnumerable<PlayerScore> PlayerScores { get; init; }
        public int CourseId { get; init; }
    }
}