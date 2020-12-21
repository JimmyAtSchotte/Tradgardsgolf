using Microsoft.AspNetCore.Components;

namespace Tradgardsgolf.Blazor.Wasm.Components.Course
{
    public class CourseButtonsBase : ComponentBase
    {
        [CascadingParameter]
        public Data.Course Course { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Visible { get; set; }

        public CourseButtonsBase()
        {
            Visible = true;
        }
    }
}
