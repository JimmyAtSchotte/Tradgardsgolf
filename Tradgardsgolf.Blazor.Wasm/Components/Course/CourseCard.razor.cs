using Microsoft.AspNetCore.Components;

namespace Tradgardsgolf.Blazor.Wasm.Components.Course
{
    public class CourseCardBase : ComponentBase
    {
        [Parameter] 
        public RenderFragment ChildContent { get; set; }


        [Parameter]
        public Data.Course Course { get; set; }
    }
}
