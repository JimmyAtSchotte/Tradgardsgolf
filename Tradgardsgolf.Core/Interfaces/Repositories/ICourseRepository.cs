using System.Collections.Generic;
using Tradgardsgolf.Core.Interfaces.Adapters;
using Tradgardsgolf.Core.Interfaces.Models;

namespace Tradgardsgolf.Core.Interfaces
{
    public interface ICourseRepository 
    {
        IEnumerable<ICourseAdapter> CreatedByPlayer(IPlayerModel player);
        IEnumerable<ICourseAdapter> HasPlayedOnCourses(IPlayerModel player);
        IEnumerable<ICourseAdapter> HasNotPlayedOnCourses(IPlayerModel player);
        IEnumerable<ICourseAdapter> ListAllWithHasPlayedCheck(IPlayerModel player);
    }
}
