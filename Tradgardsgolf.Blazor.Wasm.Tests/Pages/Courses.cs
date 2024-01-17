using AspNetMonsters.Blazor.Geolocation;
using Bunit;
using Moq;
using Tradgardsgolf.BlazorWasm.ApiServices;
using Tradgardsgolf.BlazorWasm.Components.Course;
using Tradgardsgolf.BlazorWasm.Pages;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Settings;

namespace Tradgardsgolf.Blazor.Wasm.Tests.Pages;

[TestFixture]
public class CoursesTests
{
    [Test]
    public void ListCourses()
    {
        using var contextBuilder = new TestContextBuilder();
        
        contextBuilder.UseMock<IApiDispatcher>(apiMock =>
        {
            apiMock
                .Setup(x => x.Dispatch(It.IsAny<AllowPlayDistanceCommand>()))
                .ReturnsAsync(new SettingResponse<int>()
                {
                    Value = 1
                });
        
            apiMock
                .Setup(x => x.Dispatch(It.IsAny<ListAllCoursesCommand>()))
                .ReturnsAsync(new []
                {
                    new CourseResponse()
                });
        });
        
        var courses = contextBuilder
            .Build()
            .RenderComponent<Courses>(parameters =>
        {
            parameters.Add(p => p.Location, new Location());
        });

        var courseCards = courses.FindComponents<CascadingCourse>();
            
        Assert.That(courseCards.Count, Is.EqualTo(1));
    }
}