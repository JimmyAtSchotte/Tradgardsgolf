using System.Collections.Generic;

namespace Tradgardsgolf.Core.Infrastructure.Course
{
    public interface ICourseRepository
    {
        IEnumerable<ICourseDtoResult> CreatedByPlayer(int playerId);
        IEnumerable<ICourseDtoResult> HasPlayedOnCourses(int playerId);
        IEnumerable<ICourseDtoResult> HasNotPlayedOnCourses(int playerId);
        IEnumerable<ICourseDtoResult> ListAllWithHasPlayedCheck(int playerId);
    }
}
