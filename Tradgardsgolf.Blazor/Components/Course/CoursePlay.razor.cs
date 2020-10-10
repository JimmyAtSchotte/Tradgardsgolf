using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Data;

namespace Tradgardsgolf.Blazor.Components
{
    public class CoursePlayBase : ComponentBase
    {
        [CascadingParameter]
        public Course Course { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        ScorecardState ScorecardState { get; set; }

        protected async Task SetupRound()
        {
            await ScorecardState.SetSelectedCourseAsync(Course);
            NavigationManager.NavigateTo("SetupRound");
        }

    }
}
