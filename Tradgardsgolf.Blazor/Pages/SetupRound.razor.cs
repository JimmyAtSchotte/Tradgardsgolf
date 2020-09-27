using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Data;

namespace Tradgardsgolf.Blazor.Pages
{
    public class SetupRoundBase : ComponentBase
    {
        [Inject]
        ScorecardState ScorecardState { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected Course SelectedCourse { get; private set; } 

        protected string PlayerName { get; set; } 

        protected List<string> SelectedPlayers { get; set; }

        protected List<string> AvailablePlayers { get; set; }


        public SetupRoundBase()
        {
            SelectedPlayers = new List<string>();
            AvailablePlayers = new List<string>();
            SelectedCourse = new Course();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
                return;
            
            SelectedCourse = await ScorecardState.GetSelectedCourseAsync();
            SelectedPlayers = (await ScorecardState.GetPlayersAsync()).ToList();
            
            //TODO: Call a service to get players played on course
            AvailablePlayers = new List<string>()
            {
                "Jimmy", "Hanna"
            };

            AvailablePlayers = AvailablePlayers.Except(SelectedPlayers).ToList();

            StateHasChanged();            
        }

        protected async Task AddPlayer(string player)
        {
            if (string.IsNullOrWhiteSpace(player))
                return;

            SelectedPlayers.Add(player);
            AvailablePlayers.Remove(player);
            PlayerName = string.Empty;

            await ScorecardState.SetPlayersAsync(SelectedPlayers);
        }

        protected async Task RemovePlayer(string player)
        {
            AvailablePlayers.Add(player);
            SelectedPlayers.Remove(player);

            await ScorecardState.SetPlayersAsync(SelectedPlayers);
        }

        protected async Task Play()
        {
            if (!SelectedPlayers.Any())
                return;

            await ScorecardState.SetPlayersAsync(SelectedPlayers);
            NavigationManager.NavigateTo("Scorecard");
        }
    }
}
