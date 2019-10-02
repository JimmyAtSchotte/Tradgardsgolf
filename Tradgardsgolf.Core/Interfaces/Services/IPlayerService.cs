using System.Collections.Generic;

namespace Tradgardsgolf.Core.Interfaces
{
    public interface IPlayerService
    {
        object Create(string input, out string password, bool emailIsRequierd = false);

        IEnumerable<object> GetPlayersThatHasPlayedOnCourse(int courseId);
    }
}
