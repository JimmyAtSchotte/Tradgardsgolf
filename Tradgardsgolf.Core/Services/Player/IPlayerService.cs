using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tradgardsgolf.Core.Services.Player
{
    public interface IPlayerService
    {
        Task<IEnumerable<Entities.Player>> ListPlayersThatHasPlayedOnCourseAsync(int courseId);
    }
}
