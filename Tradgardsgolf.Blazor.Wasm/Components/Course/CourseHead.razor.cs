using Microsoft.AspNetCore.Components;

namespace Tradgardsgolf.Blazor.Wasm.Components.Course
{
    public class CourseHeadBase : ComponentBase
    {
        [CascadingParameter]
        public Data.Course Course { get; set; }

    }
}
