using System;
using System.Linq;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications
{
    public class TournamentsOnCourse : Specification<Tournament>
    {
        public TournamentsOnCourse(Guid courseId, DateTime date)
        {
            Query.Where(x => x.TournamentCourseDates.Any(c => c.CourseId == courseId && c.Date == date));
        }
    }
}