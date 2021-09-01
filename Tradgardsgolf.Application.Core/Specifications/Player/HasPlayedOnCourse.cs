using System.Linq;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Player
{
    public class HasPlayedOnCourse : Specification<Entities.Player>
    {
        public HasPlayedOnCourse(int courseId)
        {
            Query.Where(x => x.RoundScores.Any(score => score.Round.Course.Id == courseId));
            Query.Include(x => x.RoundScores)
                .ThenInclude(x => x.Round)
                .ThenInclude(x => x.Course);
        }
    }
}