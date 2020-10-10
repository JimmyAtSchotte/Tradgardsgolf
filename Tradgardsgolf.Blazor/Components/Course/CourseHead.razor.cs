using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Data;

namespace Tradgardsgolf.Blazor.Components
{
    public class CourseHeadBase : ComponentBase
    {
        [CascadingParameter]
        public Course Course { get; set; }

    }
}
