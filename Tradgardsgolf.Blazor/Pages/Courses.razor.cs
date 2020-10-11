using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Data;
using Tradgardsgolf.Blazor.State;

namespace Tradgardsgolf.Blazor.Pages
{
    public class CoursesBase : ComponentBase
    {
        [Inject]
        ICourseServiceAdapter CourseService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }
        
        [Inject]
        ScorecardState ScorecardState { get; set; }        

        protected IEnumerable<Course> Courses;

        protected override async Task OnInitializedAsync()
        {
            Courses = await CourseService.ListAll();
        }

        protected async Task SetupRound(Course course)
        {
            await ScorecardState.SetSelectedCourseAsync(course);
            NavigationManager.NavigateTo("SetupRound");
        }
    }

}
