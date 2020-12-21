using System.Linq;
using System.Net;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications
{
    public class PlayerPlayedOnCourse : Specification<Player>
    {
        public PlayerPlayedOnCourse(int courseId)
        {
            Query.Where(x => x.RoundScores.Any(score => score.Round.Course.Id == courseId));
            Query.Include(x => x.RoundScores)
                .ThenInclude(x => x.Round)
                .ThenInclude(x => x.Course);
        }
    }
}