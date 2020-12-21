using Microsoft.AspNetCore.Components;

namespace Tradgardsgolf.Blazor.Wasm.Components.Player
{
    public class AvatarBase : ComponentBase
    {
        [Parameter]
        public Data.Player Player { get; set; }
    }
}
