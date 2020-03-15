using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Infrastructure.Context
{
    public class RoundScore : BaseEntity<RoundScore>
    {
        [Column("intRoundId")]
        public int RoundId { get; private set; }
        public Round Round { get; private set; }


        [Column("intHole")]
        public int Hole { get; private set; }

        [Column("intPlayerId")]
        public int PlayerId { get; private set; }
        public Player Player { get; private set; }

        [Column("intScore")]
        public int Score { get; private set; }
              
        private RoundScore()
        {

        }

        internal RoundScore(Round round, Player player, int hole, int score)
        {
            Player = player; ;
            PlayerId = player.Id;
            Round = round;
            RoundId = round.Id;
            Hole = hole;
            Score = score;
        }
    }
}
