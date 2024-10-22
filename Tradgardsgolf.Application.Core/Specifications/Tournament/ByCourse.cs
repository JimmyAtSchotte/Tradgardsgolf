using System;
using System.Linq;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Specifications.Course;

namespace Tradgardsgolf.Core.Specifications.Tournament;

public class TournamentsOnCourse : SpecificationEquatable<Entities.Tournament, TournamentsOnCourse>
{
    public TournamentsOnCourse(Guid courseId, DateTime date) : base(courseId, date)
    {
        Query.PostProcessingAction(tournaments => tournaments.Where(x => x.TournamentCourseDates.Any(courseDate =>
                     courseDate.CourseId == courseId && courseDate.Date == date)));
    }
}