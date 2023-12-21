using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Scorecard;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Api.RequestHandling
{
    public class SaveScorecardHandler(IRepository<Course> courseRepository, IRepository<Player> playerRepository)
        : IRequestHandler<SaveScorecardCommand, ScorecardResponse>
    {
        public async Task<ScorecardResponse> Handle(SaveScorecardCommand request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetByIdAsync(request.CourseId);
            var round = course.CreateRound();

            foreach (var score in request.PlayerScores)
            {
                var player = await GetOrCreatePlayer(request, score);

                foreach (var holeScore in score.HoleScores)
                    round.AddScore(player, holeScore);
            }

            await courseRepository.UpdateAsync(course);

            return new ScorecardResponse()
            {
                Id = round.Id,
                CourseId = course.Id,
                PlayerScores = request.PlayerScores
            };
        }

        private async Task<Player> GetOrCreatePlayer(SaveScorecardCommand request, PlayerScore score)
        {
            var player = await playerRepository.GetBySpecAsync(CoursePlayer.Specification(request.CourseId, score.Name)) ??
                         await playerRepository.AddAsync(Player.Create(x => x.Name = score.Name));
            
            return player;
        }
    }
}

