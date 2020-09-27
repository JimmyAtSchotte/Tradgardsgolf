using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Data;

namespace Tradgardsgolf.Blazor.Pages
{
   

    public class ScorecardBase : ComponentBase
    {
        [Inject]
        ScorecardState ScorecardState { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected List<PlayerScore> Players { get; set; }
                
        protected Course Course { get; private set; }

        protected bool ScoresMissing => Players.Any(x => x.MissingScores());

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
            Players =(await ScorecardState.GetPlayersAsync()).ToList();  
                        
            StateHasChanged();
        }

        protected async Task ChangePlayers()
        {
            await ScorecardState.SetPlayersAsync(Players);

            NavigationManager.NavigateTo("SetupRound");
        }
    }
}
