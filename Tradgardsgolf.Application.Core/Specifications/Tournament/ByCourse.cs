using System;
using System.Linq;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications.Tournament;

public class TournamentsOnCourse : Specification<Entities.Tournament>
{
    public TournamentsOnCourse(Guid courseId, DateTime date)
    {
        Query.PostProcessingAction(tournaments => tournaments.Where(x => x.TournamentCourseDates.Any(courseDate =>
                     courseDate.CourseId == courseId && courseDate.Date == date)));
    }
}