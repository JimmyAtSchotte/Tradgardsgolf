using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Players;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;
using HasPlayedOnCourse = Tradgardsgolf.Core.Specifications.HasPlayedOnCourse;

namespace Tradgardsgolf.Api.RequestHandling
{
    public class HasPlayedOnCourseHandler : IRequestHandler<HasPlayedOnCourseCommand, IEnumerable<PlayerResponse>>
    {
        private readonly IRepository<Player> _playerRepository;

        public HasPlayedOnCourseHandler(IRepository<Player> playerRepository)
        {
            _playerRepository = playerRepository;
        }
        
        public async Task<IEnumerable<PlayerResponse>> Handle(HasPlayedOnCourseCommand request, CancellationToken cancellationToken)
        {
            return (await _playerRepository.ListAsync(new HasPlayedOnCourse(request.CourseId)))
                    .Select(player => new PlayerResponse()
                    {
                        Name = player.Name
                    }).ToList();
        }
    }
}