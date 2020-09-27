using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Data;
using Tradgardsgolf.Core.Services.Course;

namespace Tradgardsgolf.Blazor.Pages
{
    public class CoursesBase : ComponentBase
    {
        [Inject]
        ICourseService CourseService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }
        
        [Inject]
        ScorecardState ScorecardState { get; set; }
        

        protected IEnumerable<Course> Courses;

        protected override async Task OnInitializedAsync()
        {
            Courses = await Task.Run(() => CourseService.ListAll().Select(x => Course.Create(x)));
        }

        protected async Task SetupRound(Course course)
        {
            await ScorecardState.SetSelectedCourseAsync(course);
            NavigationManager.NavigateTo("SetupRound");
        }
    }
}
