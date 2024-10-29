using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Scorecard;


public static partial class SpecificationSetExtensions
{
    public static ISpecification<Entities.Scorecard> ByTournament(this SpecificationSet<Entities.Scorecard> set,
        Guid tournamentId)
        => new ByTournament(tournamentId);
}

internal sealed class ByTournament : SpecificationEquatable<Entities.Scorecard, ByTournament>
{
    public ByTournament(Guid tournamentId) : base(tournamentId)
    {
        Query.Where(x => x.TournamentId == tournamentId);
    }
}