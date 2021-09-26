using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Tradgardsgolf.Api.Shared;
using Tradgardsgolf.Contracts.Course;

namespace Tradgardsgolf.Blazor.Wasm.State
{
    public class ScorecardState : BaseState
    {
        private ScorecardState()
        {
        }
        public static ScorecardState Create(Course courseModel, params PlayerScores[] playerScores)
        {
            return new ()
            {
                Course = courseModel,
                PlayerScores = playerScores.ToList(),
            };
        }

        public ScorecardState(Course course, IEnumerable<PlayerScores> playerScores)
        {
            Course = course;
            PlayerScores = playerScores.ToList();
        }
        
        [JsonProperty] 
        public Course Course { get; private set; }
        
        [JsonProperty] 
        public List<PlayerScores> PlayerScores { get; private set; }

        public async Task AddPlayer(ComponentBase source, string name)
        {
            var player = Api.Shared.PlayerScores.Create(name, Course.Holes);
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