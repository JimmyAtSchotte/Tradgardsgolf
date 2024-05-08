using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Scorecard;

public class ByTournament : Specification<Entities.Scorecard>
{
    public ByTournament(Guid tournamentId)
    {
        Query.Where(x => x.TournamentId == tournamentId);
    }
}