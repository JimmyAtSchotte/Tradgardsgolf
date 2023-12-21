using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tradgardsgolf.Contracts.Course;

namespace Tradgardsgolf.Blazor.Wasm.State
{
    public class ScorecardState : BaseState
    {
        public ScorecardState()
        {
        }
        
        public static ScorecardState Create(CourseResponse courseResponseModel, params PlayerScores[] playerScores)
        {
            return new ()
            {
                CourseResponse = courseResponseModel,
                PlayerScores = playerScores.ToList(),
            };
        }

        private ScorecardState(CourseResponse courseResponse, IEnumerable<PlayerScores> playerScores)
        {
            CourseResponse = courseResponse;
            PlayerScores = playerScores.ToList();
        }
        
        [JsonPropertyName("courseResponse")] 
        public CourseResponse CourseResponse { get; set; }
        
        [JsonPropertyName("playerScores")]
        public List<PlayerScores> PlayerScores { get; set; }

        public async Task AddPlayer(ComponentBase source, string name)
        {
            var player = State.PlayerScores.Create(name, CourseResponse.Holes);
            PlayerScores.Add(player);
            await Task.Delay(1);
            base.NotifyStateChange(source, nameof(PlayerScores));
        }
        
        public async Task RemovePlayer(ComponentBase source, string name)
        {
            PlayerScores.RemoveAll(x => x.PlayerResponse.Name == name);
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
            var player = PlayerScores.FirstOrDefault(x => x.PlayerResponse.Name == name);
            if (player == null)
                return;

            player.Scores[hole].Score = score;
            await Task.Delay(1);
            base.NotifyStateChange(source, nameof(PlayerScores));
        }
    }
}