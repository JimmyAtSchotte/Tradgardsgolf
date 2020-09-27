using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Data;

namespace Tradgardsgolf.Blazor.Pages
{
    public class PlayerScore
    {
        public string Name { get; set; }

        public IDictionary<int, int?> Scores { get; set; }

        public int Total => Scores.Select(x => x.Value.GetValueOrDefault(0)).Sum();

        public PlayerScore(string name, IDictionary<int, int?> scores)
        {
            Name = name;
            Scores = scores;
        }
    }

    public class ScorecardBase : ComponentBase
    {
        [Inject]
        ScorecardState ScorecardState { get; set; }

        protected List<PlayerScore> Players { get; set; }
                
        protected Course Course { get; private set; }

        
        public ScorecardBase()
        {
            Players = new List<PlayerScore>();
            Course = new Course();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
                return;

            Course = await ScorecardState.GetSelectedCourseAsync();
            var players = (await ScorecardState.GetPlayersAsync()).ToList();

            foreach (var player in players)
            {
                var scores = new Dictionary<int, int?>();

                for (int hole = 1; hole <= Course.Holes; hole++)                
                    scores.Add(hole, hole+23);  

                Players.Add(new PlayerScore(player, scores));
            }       
              
                        
            StateHasChanged();
        }

    }
}
