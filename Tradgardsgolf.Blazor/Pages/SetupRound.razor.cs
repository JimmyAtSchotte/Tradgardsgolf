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

        protected List<PlayerScore> SelectedPlayers { get; set; }

        private List<Player> _availablePlayers;
        protected List<Player> AvailablePlayers => _availablePlayers.Where(x => SelectedPlayers.All(player => player.Player.Name != x.Name)).ToList();


        public SetupRoundBase()
        {
            SelectedPlayers = new List<PlayerScore>();
            SelectedCourse = new Course();
            _availablePlayers = new List<Player>();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
                return;
            
            SelectedCourse = await ScorecardState.GetSelectedCourseAsync();
            SelectedPlayers = (await ScorecardState.GetPlayersAsync()).ToList();

            //TODO: Call a service to get players played on course
            _availablePlayers = new List<Player>()
            {
                new Player(){
                    Name="Jimmy"
                }, new Player(){
                    Name = "Hanna" 
                }
            };

            StateHasChanged();            
        }

        protected async Task AddPlayer(string player)
        {
            if (string.IsNullOrWhiteSpace(player))
                return;

            SelectedPlayers.Add(PlayerScore.Create(player, SelectedCourse.Holes));
            PlayerName = string.Empty;

            await ScorecardState.SetPlayersAsync(SelectedPlayers);

            StateHasChanged();
        }

        protected async Task RemovePlayer(string player)
        {
            SelectedPlayers.RemoveAll(x => x.Player.Name == player);
            await ScorecardState.SetPlayersAsync(SelectedPlayers);

            StateHasChanged();
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
