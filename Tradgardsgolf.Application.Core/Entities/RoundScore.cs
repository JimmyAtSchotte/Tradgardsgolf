﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities
{
    [Table("roundscore")]

    public class RoundScore : BaseEntity<RoundScore>
    {        
        public int Id { get; set; }
        public int RoundId { get; private set; }
        public int Hole { get; private set; }
        public int PlayerId { get; private set; }
        public int Score { get; private set; }
        public Round Round { get; private set; }
        public Player Player { get; private set; }
        private RoundScore()
        {

        }

        private RoundScore(Round round, Player player, int hole, int score)
        {
            Player = player; ;
            PlayerId = player.Id;
            Round = round;
            RoundId = round.Id;
            Hole = hole;
            Score = score;
        }
        
        public static RoundScore Create(Round round, Player player, int hole, int score)
        {
            return new RoundScore(round, player, hole, score);
        }
    }
}
