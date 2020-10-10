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

        public bool IsModalOpened { get; set; }
        public string SelectedButton { get; set; }

        protected PlayerScore EditPlayerScore { get; set; }
        protected int EditHole { get; set; } 

        protected int ModalMaxScore { get; set; }

        public ScorecardBase()
        {
            Players = new List<PlayerScore>();
            Course = new Course();
            ModalMaxScore = 12;
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

        protected void SetScore(PlayerScore playerScore, int hole)
        {
            if (IsModalOpened)
                return;

            EditPlayerScore = playerScore;
            EditHole = hole;

            IsModalOpened = true;

            StateHasChanged();
        }

        protected void OnClose(string value)
        {
            EditPlayerScore.Scores[EditHole].Score = Convert.ToInt32(value);

            StateHasChanged();
        }

        protected void AddModalMaxScore()
        {
            ModalMaxScore += 3;

            StateHasChanged();
        }
    }
}
