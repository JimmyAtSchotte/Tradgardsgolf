using Microsoft.AspNetCore.Components;

namespace Tradgardsgolf.Blazor.Components.Course
{
    public class CourseHeadBase : ComponentBase
    {
        [CascadingParameter]
        public Data.Course Course { get; set; }

    }
}
