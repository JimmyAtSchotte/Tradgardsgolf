using System.Linq;

namespace Tradgardsgolf.Core.Entities
{
    public static class Round_AddScore
    {
        
        public static void AddScore(this Round round, Player player, int score)
        {
            var hole = round.RoundScores
                .Where(x => x.Player == player)
                .Select(x => x.Hole)
                .DefaultIfEmpty()
                .Max() + 1;
            
            round.CreateRoundScore(player, hole, score);
        }
    }
}