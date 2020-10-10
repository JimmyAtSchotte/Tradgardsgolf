using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Data;

namespace Tradgardsgolf.Blazor.Components
{
    public class CourseCardBase : ComponentBase
    {
        [Parameter] 
        public RenderFragment ChildContent { get; set; }


        [Parameter]
        public Course Course { get; set; }
    }
}
