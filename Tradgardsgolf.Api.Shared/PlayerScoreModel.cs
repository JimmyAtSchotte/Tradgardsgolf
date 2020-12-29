using System.Linq;

namespace Tradgardsgolf.Api.Shared
{
    public class PlayerScoreModel
    {
        public PlayerModel Player { get; set; }

        public HoleScoreCollection Scores { get; set; }

        public PlayerScoreModel()
        {

        }

        private PlayerScoreModel(PlayerModel player, HoleScoreCollection scores)
        {
            Player = player;
            Scores = scores;
        }

        public static PlayerScoreModel Create(string name, int holes)
        {
            var scores = new HoleScoreCollection();

            for (int hole = 1; hole <= holes; hole++)
                scores.Add(HoleScoreModel.Create(hole));

            return new PlayerScoreModel(new PlayerModel()
            {
                Name = name
            }, scores);
        }

        public int Total() => Scores.Select(x => x.Score.GetValueOrDefault(0)).Sum();
        public bool MissingScores() => Scores.Any(x => x.Score.HasValue == false);
    }
}
