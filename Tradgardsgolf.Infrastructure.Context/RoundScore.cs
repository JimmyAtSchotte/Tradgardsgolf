using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Infrastructure.Context
{
    public class RoundScore : BaseEntity<RoundScore>
    {
        [Column("intRoundId")]
        public int RoundId { get; set; }
        public Round Round { get; set; }


        [Column("intHole")]
        public int Hole { get; set; }

        [Column("intPlayerId")]
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        [Column("intScore")]
        public int Score { get; set; }
    }
}
