﻿using System.Linq;
using Tradgardsgolf.Contracts.Players;

namespace Tradgardsgolf.BlazorWasm.State
{
    public class PlayerScores
    {
        public PlayerResponse PlayerResponse { get; set; }

        public HoleScoreCollection Scores { get; set; }

        public PlayerScores()
        {

        }

        private PlayerScores(PlayerResponse playerResponse, HoleScoreCollection scores)
        {
            PlayerResponse = playerResponse;
            Scores = scores;
        }

        public static PlayerScores Create(string name, int holes)
        {
            var scores = new HoleScoreCollection();

            for (int hole = 1; hole <= holes; hole++)
                scores.Add(HoleScoreModel.Create(hole));

            return new PlayerScores(new PlayerResponse()
            {
                Name = name
            }, scores);
        }

        public int Total() => Scores.Select(x => x.Score.GetValueOrDefault(0)).Sum();
        public bool MissingScores() => Scores.Any(x => x.Score.HasValue == false);
    }
}
