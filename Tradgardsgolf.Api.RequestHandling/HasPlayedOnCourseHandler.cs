using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Players;
using Tradgardsgolf.Core.Infrastructure.Player;
using Tradgardsgolf.Core.Specifications;
using HasPlayedOnCourse = Tradgardsgolf.Core.Specifications.Player.HasPlayedOnCourse;

namespace Tradgardsgolf.Tasks
{
    public class HasPlayedOnCourseHandler : IRequestHandler<Contracts.Players.HasPlayedOnCourse, IEnumerable<Player>>
    {
        private readonly IPlayerRepository _playerRepository;

        public HasPlayedOnCourseHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        
        public async Task<IEnumerable<Player>> Handle(Contracts.Players.HasPlayedOnCourse request, CancellationToken cancellationToken)
        {
            return (await _playerRepository.ListAsync(new HasPlayedOnCourse(request.CourseId)))
                    .Select(player => new Player()
                    {
                        Name = player.Name
                    }).ToList();
        }
    }
}