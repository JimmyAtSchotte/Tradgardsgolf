using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tradgardsgolf.Blazor.Wasm.ApiServices;
using Tradgardsgolf.Blazor.Wasm.Data;
using Tradgardsgolf.Blazor.Wasm.State;

namespace Tradgardsgolf.Blazor.Wasm.Pages
{
    public class CoursesBase : ComponentBase
    {
        [Inject]
        ICourseApiService CourseApiService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }
        
        [Inject]
        ScorecardState ScorecardState { get; set; }        

        protected IEnumerable<Course> Courses;

        protected override async Task OnInitializedAsync()
        {
            Courses = await CourseApiService.ListAll();
        }

        protected async Task SetupRound(Course course)
        {
            await ScorecardState.SetSelectedCourseAsync(course);
            NavigationManager.NavigateTo("SetupRound");
        }
    }

}
