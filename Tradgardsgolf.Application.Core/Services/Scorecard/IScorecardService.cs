using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Core.Services.Scorecard
{
    public interface IScorecardService
    {
        void Add(IAddScorecardCommand command);
    }

    public interface IAddScorecardCommand
    {
        int CourseId { get; }
        IEnumerable<IPlayerScoreCommand> PlayerScores { get; }
    }

    public interface IPlayerScoreCommand
    {
        string Name { get; }
        int[] Scores { get; }
    }
}
