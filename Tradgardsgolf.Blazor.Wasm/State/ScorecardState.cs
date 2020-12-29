using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Tradgardsgolf.Api.Shared;

namespace Tradgardsgolf.Blazor.Wasm.State
{
    public class ScorecardState : BaseState
    {
        private ScorecardState()
        {
        }
        public static ScorecardState Create(CourseModel courseModel, params PlayerScoreModel[] playerScores)
        {
            return new ()
            {
                CourseModel = courseModel,
                PlayerScores = playerScores.ToList(),
            };
        }

        public ScorecardState(CourseModel courseModel, IEnumerable<PlayerScoreModel> playerScores)
        {
            CourseModel = courseModel;
            PlayerScores = playerScores.ToList();
        }
        
        [JsonProperty] 
        public CourseModel CourseModel { get; private set; }
        
        [JsonProperty] 
        public List<PlayerScoreModel> PlayerScores { get; private set; }

        public async Task AddPlayer(ComponentBase source, string name)
        {
            var player = PlayerScoreModel.Create(name, CourseModel.Holes);
            PlayerScores.Add(player);
            await Task.Delay(1);
            base.NotifyStateChange(source, nameof(PlayerScores));
        }
        
        public async Task RemovePlayer(ComponentBase source, string name)
        {
            PlayerScores.RemoveAll(x => x.Player.Name == name);
            await Task.Delay(1);
            base.NotifyStateChange(source, nameof(PlayerScores));
        }

        public async Task ResetScores(ComponentBase source)
        {
            foreach (var score in PlayerScores.SelectMany(player => player.Scores))
                score.Score = null;
            
            await Task.Delay(1);
        }

        public async Task SetScore(ComponentBase source, string name, int hole, int score)
        {
            var player = PlayerScores.FirstOrDefault(x => x.Player.Name == name);
            if (player == null)
                return;

            player.Scores[hole].Score = score;
            await Task.Delay(1);
            base.NotifyStateChange(source, nameof(PlayerScores));
        }
    }
}