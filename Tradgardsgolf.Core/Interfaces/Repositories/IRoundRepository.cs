using System.Collections.Generic;

namespace Tradgardsgolf.Core.Interfaces
{
    public interface IRoundRepository 
    {

        IEnumerable<object> ListAllByCourse(object course);
    }
}
