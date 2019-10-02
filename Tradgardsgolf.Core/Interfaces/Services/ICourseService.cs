using System.Collections.Generic;

namespace Tradgardsgolf.Core.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<object> GetCoursesWithDistance(double latitude, double longitude);

        IEnumerable<object> GetHasPlayedOnCourses();
        IEnumerable<object> GetHasNotPlayedOnCourses();

        object Create(object createCourseModel);

        

        
    }
}
