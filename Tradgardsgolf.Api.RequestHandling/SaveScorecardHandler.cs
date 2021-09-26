using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Scorecard;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Player = Tradgardsgolf.Core.Entities.Player;

namespace Tradgardsgolf.Tasks
{
    public class SaveScorecardHandler : IRequestHandler<SaveScorecard, Scorecard>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IRoundRepository _roundRepository;
        private readonly IRoundScoreRepository _roundScoreRepository;

        public SaveScorecardHandler(ICourseRepository courseRepository, IPlayerRepository playerRepository, IRoundRepository roundRepository, IRoundScoreRepository roundScoreRepository)
        {
            _courseRepository = courseRepository;
            _playerRepository = playerRepository;
            _roundRepository = roundRepository;
            _roundScoreRepository = roundScoreRepository;
        }


        public async Task<Scorecard> Handle(SaveScorecard request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            var round = await _roundRepository.AddAsync(course.CreateRound());

            foreach (var score in request.PlayerScores)
            {
                var player = await _playerRepository.GetBySpecAsync(CoursePlayer.Specification(request.CourseId, score.Name)) ??
                             await _playerRepository.AddAsync(Player.Create(x => x.Name = score.Name));

                var hole = 1;
                foreach (var holeScore in score.HoleScores)
                {
                    await _roundScoreRepository.AddAsync(round.CreateRoundScore(player, hole, holeScore));
                    hole++;
                }
            }

            return new Scorecard()
            {
                Id = round.Id,
                CourseId = course.Id,
                PlayerScores = request.PlayerScores
            };
        }
    }
}

