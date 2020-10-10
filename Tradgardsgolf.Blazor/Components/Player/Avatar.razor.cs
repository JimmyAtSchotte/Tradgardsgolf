using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tradgardsgolf.Blazor.Components.Player
{
    public class AvatarBase : ComponentBase
    {
        [Parameter]
        public Data.Player Player { get; set; }
    }
}
