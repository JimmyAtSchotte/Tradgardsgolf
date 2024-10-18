using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Course;

public class ById : Specification<Entities.Course> , IEquatable<ById>
{
    private readonly Guid _courseId;

    public ById(Guid courseId)
    {
        _courseId = courseId;
        Query.Where(x => x.Id == courseId);
    }

    public bool Equals(ById other)
    {
        return _courseId == other._courseId;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ById)obj);
    }

    public override int GetHashCode()
    {
        return _courseId.GetHashCode();
    }
}