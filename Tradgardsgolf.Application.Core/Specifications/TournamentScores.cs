using System;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications
{
    public class TournamentScores : Specification<Tournament>
    {
        public TournamentScores(int tournamentId)
        {
            Query.Where(x => x.Id == tournamentId);
            Query.Include(x => x.TournamentRounds)
                .ThenInclude(x => x.Round)
                .ThenInclude(x => x.RoundScores)
                .ThenInclude(x => x.Player);
        }
    }
}