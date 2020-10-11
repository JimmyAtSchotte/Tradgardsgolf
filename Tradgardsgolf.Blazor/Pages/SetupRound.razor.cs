using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Data;
using Tradgardsgolf.Blazor.State;

namespace Tradgardsgolf.Blazor.Pages
{
    public class SetupRoundBase : ComponentBase
    {
        [Inject]
        ICourseServiceAdapter CourseService { get; set; }

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
            _availablePlayers = (await CourseService.Players(SelectedCourse)).ToList();

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
