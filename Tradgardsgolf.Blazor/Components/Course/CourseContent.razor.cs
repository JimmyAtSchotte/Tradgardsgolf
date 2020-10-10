using Microsoft.AspNetCore.Components;

namespace Tradgardsgolf.Blazor.Components.Course
{
    public class CourseContentBase : ComponentBase
    {
        [CascadingParameter]
        public Data.Course Course { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Title { get; set; }


        [Parameter]
        public bool Visible { get; set; }


        public CourseContentBase()
        {
            Visible = true;
        }

    }
}
