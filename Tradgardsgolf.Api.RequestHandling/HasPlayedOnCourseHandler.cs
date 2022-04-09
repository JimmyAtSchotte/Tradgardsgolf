using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Players;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using HasPlayedOnCourse = Tradgardsgolf.Core.Specifications.HasPlayedOnCourse;

namespace Tradgardsgolf.Tasks
{
    public class HasPlayedOnCourseHandler : IRequestHandler<HasPlayedOnCourseCommand, IEnumerable<Player>>
    {
        private readonly IPlayerRepository _playerRepository;

        public HasPlayedOnCourseHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        
        public async Task<IEnumerable<Player>> Handle(HasPlayedOnCourseCommand request, CancellationToken cancellationToken)
        {
            return (await _playerRepository.ListAsync(new HasPlayedOnCourse(request.CourseId)))
                    .Select(player => new Player()
                    {
                        Name = player.Name
                    }).ToList();
        }
    }
}