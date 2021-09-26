using System.Linq;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications
{
    public class CoursePlayer : Specification<Player>
    {
        public CoursePlayer(int courseId, string name)
        {
            Query.Where(x => x.RoundScores.Any(score => score.Round.Course.Id == courseId) && x.Name == name);
            Query.Include(x => x.RoundScores)
                .ThenInclude(x => x.Round)
                .ThenInclude(x => x.Course);
        }

        public static CoursePlayer Specification(int courseId, string name) => new CoursePlayer(courseId, name);
    }
}