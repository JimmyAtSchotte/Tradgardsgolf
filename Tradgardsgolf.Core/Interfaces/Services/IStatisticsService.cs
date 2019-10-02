using System.Collections.Generic;

namespace Tradgardsgolf.Core.Interfaces
{
    public interface IStatisticsService
    {
        IEnumerable<object> GetHighScoreList(int numberOfTopScores);
        IEnumerable<object> GetAvarageScoreList(int numberOfTopScores);
        IEnumerable<object> GetAvarageHoleScoreList();
        IEnumerable<object> GetSeasonTable(int numberOfRounds);
    }
}
