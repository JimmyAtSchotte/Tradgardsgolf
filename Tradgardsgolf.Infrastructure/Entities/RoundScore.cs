using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tradgardsgolf.Infrastructure.EntityBuilder;
using Tradgardsgolf.Infrastructure.SharedKernel;

namespace Tradgardsgolf.Infrastructure.Entities
{
    public class RoundScore : BaseEntity<RoundScore>
    {
        [Column("intRoundId")]
        public int RoundId { get; internal set; }
        public Round Round { get; internal set; }

        [Column("intHole")]
        public int Hole { get; internal set; }

        [Column("intPlayerId")]
        public int PlayerId { get; internal set; }
        public Player Player { get; internal set; }

        [Column("intScore")]
        public int Score { get; internal set; }

        private RoundScore()
        {
        }

        public static RoundScore Create(Action<RoundScoreBuilder> options)
        {
            var roundScore = new RoundScore();
            roundScore.SetOptions(options);

            return roundScore;
        }
    }
}
