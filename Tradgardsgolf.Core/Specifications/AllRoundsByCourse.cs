using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications
{
    public class AllRoundsByCourse : Specification<Round>
    {
        public AllRoundsByCourse(int courseId)
        {
            Query.Where(x => x.CourseId == courseId);
            Query.Include(x => x.RoundScores)
                .ThenInclude(x => x.Player);
        }
    }
}