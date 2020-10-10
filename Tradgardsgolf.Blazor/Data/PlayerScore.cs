using System.Collections.Generic;
using System.Linq;

namespace Tradgardsgolf.Blazor.Data
{
    public class PlayerScore
    {
        public Player Player { get; set; }

        public HoleScoreCollection Scores { get; set; }


        public PlayerScore()
        {

        }

        private PlayerScore(Player player, HoleScoreCollection scores)
        {
            Player = player;
            Scores = scores;
        }

        public static PlayerScore Create(string name, int holes)
        {
            var scores = new HoleScoreCollection();

            for (int hole = 1; hole <= holes; hole++)
                scores.Add(HoleScore.Create(hole));

            return new PlayerScore(new Player()
            {
                Name = name
            }, scores);
        }

        public int Total() => Scores.Select(x => x.Score.GetValueOrDefault(0)).Sum();
        public bool MissingScores() => Scores.Any(x => x.Score.HasValue == false);
    }
}
