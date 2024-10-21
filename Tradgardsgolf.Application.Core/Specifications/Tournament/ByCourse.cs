using System;
using System.Linq;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications.Tournament;

public class TournamentsOnCourse : Specification<Entities.Tournament>, IEquatable<TournamentsOnCourse>
{
    private readonly Guid _courseId;
    private readonly DateTime _date;

    public TournamentsOnCourse(Guid courseId, DateTime date)
    {
        _courseId = courseId;
        _date = date;
        
        Query.PostProcessingAction(tournaments => tournaments.Where(x => x.TournamentCourseDates.Any(courseDate =>
                     courseDate.CourseId == courseId && courseDate.Date == date)));
    }

    public bool Equals(TournamentsOnCourse other)
    {
        return _courseId == other._courseId && _date == other._date;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TournamentsOnCourse)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_courseId, _date);
    }
}