using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Core.Services.Scorecard
{
    public interface IScorecardService
    {
        void Add(IScorecardModel model);
    }

    public interface IScorecardModel
    {
        int CourseId { get; }
        IEnumerable<IPlayerScoreModel> PlayerScores { get; }
    }

    public interface IPlayerScoreModel
    {
        string Name { get; }
        int[] Scores { get; }
    }
}
