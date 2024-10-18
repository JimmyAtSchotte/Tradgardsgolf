using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Scorecard;

public class ByCourse : Specification<Entities.Scorecard>, IEquatable<ByCourse>
{
    private readonly Guid _courseId;

    public ByCourse(Guid courseId)
    {
        _courseId = courseId;
        Query.Where(x => x.CourseId == courseId);
    }

    public bool Equals(ByCourse other)
    {
        return _courseId == other._courseId;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ByCourse)obj);
    }

    public override int GetHashCode()
    {
        return _courseId.GetHashCode();
    }
}