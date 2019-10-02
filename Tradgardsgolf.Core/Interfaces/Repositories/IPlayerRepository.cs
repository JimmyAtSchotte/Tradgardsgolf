using System.Collections.Generic;
using Tradgardsgolf.Core.Interfaces.Adapters;
using Tradgardsgolf.Core.Interfaces.Models;

namespace Tradgardsgolf.Core.Interfaces
{
    public interface IPlayerRepository 
    {
        bool CheackIfMailExists(string email);
        IPlayerAdapter GetByEmail(string email);
        IEnumerable<IPlayerAdapter> GetPlayersThatHasPlayedOnCourse(ICourseModel course);
    }
}
