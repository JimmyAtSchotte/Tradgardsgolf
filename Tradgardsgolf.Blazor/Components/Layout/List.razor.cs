using Microsoft.AspNetCore.Components;

namespace Tradgardsgolf.Blazor.Components.Layout
{
    public class ListBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}