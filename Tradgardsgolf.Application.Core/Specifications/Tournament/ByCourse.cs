using System;
using System.Linq;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Tournament;

public class TournamentsOnCourse : Specification<Entities.Tournament>
{
    public TournamentsOnCourse(Guid courseId, DateTime date)
    {
        Query.Where(tournament =>
            tournament.TournamentCourseDates.Any(courseDate =>
                courseDate.CourseId == courseId && courseDate.Date == date));
    }
}