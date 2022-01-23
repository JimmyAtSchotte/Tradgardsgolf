using System.Linq;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.UnitOfWork
{
    public class RoundUnitOfWork
    {
        private readonly Round _round;

        public RoundUnitOfWork(Round round)
        {
            _round = round;
        }

        public void AddScore(Player player, int score)
        {
            var hole = _round.RoundScores
                .Where(x => x.Player == player)
                .Select(x => x.Hole)
                .DefaultIfEmpty()
                .Max() + 1;
            
            _round.CreateRoundScore(player, hole, score);
        }

        public Round Build()
        {
            return _round;
        }
    }
}