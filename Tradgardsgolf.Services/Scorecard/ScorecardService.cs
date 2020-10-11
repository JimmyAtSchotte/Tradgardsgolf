using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Infrastructure.Course;
using Tradgardsgolf.Core.Infrastructure.Player;
using Tradgardsgolf.Core.Infrastructure.Scorecard;
using Tradgardsgolf.Core.Services.Scorecard;

namespace Tradgardsgolf.Services.Scorecard
{
    public class ScorecardService : IScorecardService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IScorecardRepository _scorecardRepository;
        private readonly IPlayerRepository _playerRepository;

        public ScorecardService(ICourseRepository courseRepository, IScorecardRepository scorecardRepository, IPlayerRepository playerRepository)
        {
            _courseRepository = courseRepository;
            _scorecardRepository = scorecardRepository;
            _playerRepository = playerRepository;
        }

        public void Add(IScorecardModel model)
        {
            var scorecard = new ScorecardDto(model);

            foreach (var score in model.PlayerScores)
            {
                var player = _playerRepository.Find(new FindPlayerPlayedOnCourseDto(model.CourseId, score.Name)) ??
                             _playerRepository.Add(new AddPlayerDto(score.Name));

                scorecard.AddScore(new PlayerScoreDto(player.Id, score.Scores));
            }

            _scorecardRepository.Add(scorecard);
        }
    }

    public class AddPlayerDto : IAddPlayerDto
    {
        public string Name { get; }

        public AddPlayerDto( string name)
        {
            Name = name;
        }
    }

    public class FindPlayerPlayedOnCourseDto : IFindPlayerPlayedOnCourseDto
    {
        public int CourseId { get; }
        public string Name { get; }

        public FindPlayerPlayedOnCourseDto(int courseId, string name)
        {
            CourseId = courseId;
            Name = name;
        }
    }

    public class ScorecardDto : IScorecardDto
    {
        private readonly IScorecardModel _model;
        private readonly IList<IPlayerScoreDto> _playerScores;

        public int CourseId => _model.CourseId;
        public IEnumerable<IPlayerScoreDto> PlayerScores => _playerScores;

        public ScorecardDto(IScorecardModel model)
        {
            _model = model;
            _playerScores = new List<IPlayerScoreDto>();
        }

        public void AddScore(IPlayerScoreDto score) => _playerScores.Add(score);

    }

    public class PlayerScoreDto : IPlayerScoreDto
    {
        public int PlayerId { get; }
        public int[] Scores { get; }

        public PlayerScoreDto(int playerId, int[] scores)
        {
            PlayerId = playerId;
            Scores = scores;
        }
    }
}
