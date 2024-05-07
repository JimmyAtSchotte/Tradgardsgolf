using System;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications
{
    public class ByTournament : Specification<Scorecard>
    {
        public ByTournament(Guid tournamentId)
        {
            Query.Where(x => x.TournamentId == tournamentId);
        }
    }
}