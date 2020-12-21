using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tradgardsgolf.Blazor.Wasm.Data;
using Tradgardsgolf.Blazor.Wasm.ServiceAdapters;
using Tradgardsgolf.Blazor.Wasm.State;

namespace Tradgardsgolf.Blazor.Wasm.Pages
{


    public class ScorecardBase : ComponentBase
    {
        [Inject]
        ScorecardState ScorecardState { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        IScorecardServiceAdapter ScorecardService { get; set; }

        protected List<PlayerScore> Players { get; set; }
                
        protected Course Course { get; private set; }
        
        public bool IsModalOpened { get; set; }
        public string SelectedButton { get; set; }

        protected PlayerScore EditPlayerScore { get; set; }
        protected int EditHole { get; set; } 

        protected int ModalMaxScore { get; set; }
        
        protected bool DisableCompleteButton => Players.Any(x => x.MissingScores()) || Saving;
        protected bool Saving { get; set; }

        protected bool DisplayPlayAgain { get; set; }

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
            if (Saving)
            {
                Saving = false;
                await ScorecardState.ResetScores();
            }
            else
                await ScorecardState.SetPlayersAsync(Players);

            NavigationManager.NavigateTo("SetupRound");
        }

        protected void CompleteRound()
        {
            Saving = true;
            StateHasChanged();

            ScorecardService.Add(Course, Players);

            DisplayPlayAgain = true;
            StateHasChanged();
        }

        protected async Task PlayAgain()
        {
            Saving = false;
            DisplayPlayAgain = false;

            await ScorecardState.ResetScores();
            Players = (await ScorecardState.GetPlayersAsync()).ToList();

            StateHasChanged();            
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

        protected async Task OnClose(string value)
        {
            EditPlayerScore.Scores[EditHole].Score = Convert.ToInt32(value);

            await ScorecardState.SetPlayersAsync(Players);

            StateHasChanged();
        }

        protected void AddModalMaxScore()
        {
            ModalMaxScore += 3;

            StateHasChanged();
        }
    }
}
