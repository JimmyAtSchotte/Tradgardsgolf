﻿using Microsoft.AspNetCore.Components;

namespace Tradgardsgolf.Blazor.Components.Player
{
    public class PlayerListItemBase : ComponentBase
    {
        [Parameter]
        public Data.Player Player { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
