using System.Collections.Generic;

namespace Tradgardsgolf.Api.Shared
{
    public class CourseStatisticModel
    {
        public IEnumerable<RoundModel> Rounds { get; set; }
    }
}