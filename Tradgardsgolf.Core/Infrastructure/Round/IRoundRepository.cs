using System.Collections.Generic;

namespace Tradgardsgolf.Core.Infrastructure.Round
{
    public interface IRoundRepository 
    {
        IEnumerable<object> ListAllByCourse(int courseId);
    }
}
