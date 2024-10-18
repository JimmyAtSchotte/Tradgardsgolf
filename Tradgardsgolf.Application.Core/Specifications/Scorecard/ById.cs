using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Scorecard;

public class ById : Specification<Entities.Scorecard>, IEquatable<ById>
{
    private readonly Guid _scorecardId;

    public ById(Guid scorecardId)
    {
        _scorecardId = scorecardId;
        Query.Where(x => x.Id == scorecardId);
    }

    public bool Equals(ById other)
    {
        return _scorecardId == other._scorecardId;
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
        return _scorecardId.GetHashCode();
    }
}