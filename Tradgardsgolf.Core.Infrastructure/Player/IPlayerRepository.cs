using System.Collections.Generic;

namespace Tradgardsgolf.Core.Infrastructure.Player
{
    public interface IPlayerRepository 
    {
        bool CheackIfMailExists(string email);
        IPlayerDtoResult GetByEmail(string email);
        IEnumerable<IPlayerDtoResult> GetPlayersThatHasPlayedOnCourse(int courseId);
    }
}
