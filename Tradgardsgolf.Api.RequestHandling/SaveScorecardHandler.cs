using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Scorecard;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.UnitOfWork;

namespace Tradgardsgolf.Tasks
{
    public class SaveScorecardHandler : IRequestHandler<SaveScorecardCommand, ScorecardResponse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IPlayerRepository _playerRepository;

        public SaveScorecardHandler(ICourseRepository courseRepository, IPlayerRepository playerRepository)
        {
            _courseRepository = courseRepository;
            _playerRepository = playerRepository;
        }


        public async Task<ScorecardResponse> Handle(SaveScorecardCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            var round = course.CreateRound();
            
            var createScoreCard = new RoundUnitOfWork(round);
            
            foreach (var score in request.PlayerScores)
            {
                var player = await GetOrCreatePlayer(request, score);

                foreach (var holeScore in score.HoleScores)
                    createScoreCard.AddScore(player, holeScore);
            }

            await _courseRepository.UpdateAsync(course);

            return new ScorecardResponse()
            {
                Id = round.Id,
                CourseId = course.Id,
                PlayerScores = request.PlayerScores
            };
        }

        private async Task<Player> GetOrCreatePlayer(SaveScorecardCommand request, PlayerScore score)
        {
            var player = await _playerRepository.GetBySpecAsync(CoursePlayer.Specification(request.CourseId, score.Name)) ??
                         await _playerRepository.AddAsync(Player.Create(x => x.Name = score.Name));
            
            return player;
        }
    }
}

