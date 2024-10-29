using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Scorecard;

public class ByTournament : SpecificationEquatable<Entities.Scorecard, ByTournament>
{
    public ByTournament(Guid tournamentId) : base(tournamentId)
    {
        Query.Where(x => x.TournamentId == tournamentId);
    }
}