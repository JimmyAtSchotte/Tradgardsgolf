using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Core.Infrastructure.Scorecard
{
    public interface IScorecardRepository
    {
        void Add(IScorecardDto dto);
    }

    public interface IScorecardDto
    {
        int CourseId { get; }
        IEnumerable<IPlayerScoreDto> PlayerScores { get; }
    }

    public interface IPlayerScoreDto
    {
        int PlayerId { get; }
        int[] Scores { get; }
    }
}
