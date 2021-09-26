using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Scorecard
{
    public record SaveScorecard : IRequest<Scorecard>
    {
        public IEnumerable<PlayerScore> PlayerScores { get; init; }
        public int CourseId { get; init; }
    }
}