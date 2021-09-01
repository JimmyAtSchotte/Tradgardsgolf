using System.Collections.Generic;

namespace Tradgardsgolf.Core.Services.Statistics
{
    public interface IStatisticsService
    {
        IEnumerable<object> GetHighScoreList(int numberOfTopScores);
        IEnumerable<object> GetAvarageScoreList(int numberOfTopScores);
        IEnumerable<object> GetAvarageHoleScoreList();
        IEnumerable<object> GetSeasonTable(int numberOfRounds);
    }
}
