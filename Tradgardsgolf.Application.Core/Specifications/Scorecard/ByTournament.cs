using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Scorecard;

public class ByTournament : Specification<Entities.Scorecard>, IEquatable<ByTournament>
{
    private readonly Guid _tournamentId;

    public ByTournament(Guid tournamentId)
    {
        _tournamentId = tournamentId;
        Query.Where(x => x.TournamentId == tournamentId);
    }

    public bool Equals(ByTournament other)
    {
        return _tournamentId == other._tournamentId;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ByTournament)obj);
    }

    public override int GetHashCode()
    {
        return _tournamentId.GetHashCode();
    }
}