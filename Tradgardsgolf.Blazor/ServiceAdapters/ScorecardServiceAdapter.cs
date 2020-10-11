using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Data;
using Tradgardsgolf.Core.Services.Scorecard;

namespace Tradgardsgolf.Blazor.ServiceAdapters
{
    public interface IScorecardServiceAdapter
    {
        void Add(Course course, IEnumerable<PlayerScore> playerScores);
    }

    public class ScorecardServiceAdapter : IScorecardServiceAdapter
    {
        private readonly IScorecardService _scorecardService;

        public ScorecardServiceAdapter(IScorecardService scorecardService)
        {
            _scorecardService = scorecardService;
        }

        public void Add(Course course, IEnumerable<PlayerScore> playerScores)
        {
            _scorecardService.Add(new ScorecardModel(course, playerScores));
        }
    }

    public class ScorecardModel : IScorecardModel
    {
        private readonly Course _course;
        private readonly IEnumerable<PlayerScore> _playerScores;

        public int CourseId => _course.Id;
        public IEnumerable<IPlayerScoreModel> PlayerScores => _playerScores.Select(x => new PlayerScoreModel(x));

        public ScorecardModel(Course course, IEnumerable<PlayerScore> playerScores)
        {
            _course = course;
            _playerScores = playerScores;
        }


    }

    public class PlayerScoreModel : IPlayerScoreModel
    {
        private readonly PlayerScore _playerScore;

        public string Name => _playerScore.Player.Name;
        public int[] Scores => _playerScore.Scores.OrderBy(x => x.Hole).Select(x => x.Score.GetValueOrDefault(0)).ToArray();

        public PlayerScoreModel(PlayerScore playerScore)
        {
            _playerScore = playerScore;
        }
    }
}
