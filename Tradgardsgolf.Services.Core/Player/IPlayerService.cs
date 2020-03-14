using System.Collections.Generic;

namespace Tradgardsgolf.Core.Services.Player
{
    public interface IPlayerService
    {
        object Create(string input, out string password, bool emailIsRequierd = false);

        IEnumerable<object> GetPlayersThatHasPlayedOnCourse(int courseId);
    }
}
