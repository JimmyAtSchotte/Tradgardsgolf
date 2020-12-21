using Microsoft.AspNetCore.Components;

namespace Tradgardsgolf.Blazor.Wasm.Components.Player
{
    public class PlayerListItemBase : ComponentBase
    {
        [Parameter]
        public Data.Player Player { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
