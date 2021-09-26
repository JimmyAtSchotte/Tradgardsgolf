using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Services.Player;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Services.Player
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        
        public async Task<IEnumerable<Core.Entities.Player>> ListPlayersThatHasPlayedOnCourseAsync(int courseId)
        {
            return await _playerRepository.ListAsync(new HasPlayedOnCourse(courseId));
        }
    }
}