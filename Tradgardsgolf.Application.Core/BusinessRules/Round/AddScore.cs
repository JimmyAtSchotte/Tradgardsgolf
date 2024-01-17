using System.Linq;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.BusinessRules.Round
{
    public  static partial class RoundExtensions
    {
        
        public static void AddScore(this Entities.Round round, Player player, int score)
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