using System.Security.Claims;
using AspNetMonsters.Blazor.Geolocation;
using Blazorise;
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
    
    [Test]
    public void CourseOwner()
    {
        var ownerGuid = Guid.NewGuid();
        
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
                    {
                        OwnerGuid = ownerGuid
                    }
                });
        });
        
        var courses = contextBuilder
            .UseAuthorization(auth =>
            {
                auth.SetAuthorized("user");
                auth.SetClaims(new Claim("oid", ownerGuid.ToString()));
            })
            .Build()
            .RenderComponent<Courses>(parameters =>
            {
                parameters.Add(p => p.Location, new Location());
            });

        var courseCard = courses.FindComponents<CascadingCourse>().FirstOrDefault();
        var buttons = courseCard.FindComponent<CourseButtons>();
        
        Assert.That(buttons.HasComponent<Dropdown>(), Is.True);
    }
    
    [Test]
    public void AuthorizedAsNoneOwner()
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
                    {
                        OwnerGuid = Guid.NewGuid()
                    }
                });
        });
        
        var courses = contextBuilder
            .UseAuthorization(auth =>
            {
                auth.SetAuthorized("user");
                auth.SetClaims(new Claim("oid", Guid.NewGuid().ToString()));
            })
            .Build()
            .RenderComponent<Courses>(parameters =>
            {
                parameters.Add(p => p.Location, new Location());
            });

        var courseCard = courses.FindComponents<CascadingCourse>().FirstOrDefault();
        var buttons = courseCard.FindComponent<CourseButtons>();
        
        Assert.That(buttons.HasComponent<Dropdown>(), Is.False);
    }
    
    [Test]
    public void InRangeToPlay()
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

        var courseCard = courses.FindComponents<CascadingCourse>().FirstOrDefault();
        var buttons = courseCard.FindComponent<CourseButtons>();
        
        Assert.That(buttons.HasComponent<Button>(), Is.True);
    }
    
    [Test]
    public void ToFarAwayToPlay()
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
                    {
                        Longitude = 1,
                        Latitude = 1
                    }
                });
        });
        
        var courses = contextBuilder
            .Build()
            .RenderComponent<Courses>(parameters =>
            {
                parameters.Add(p => p.Location, new Location()
                {
                    Latitude = 10,
                    Longitude = 10
                });
            });

        var courseCard = courses.FindComponents<CascadingCourse>().FirstOrDefault();
        var buttons = courseCard.FindComponent<CourseButtons>();
        
        Assert.That(buttons.HasComponent<Button>(), Is.False);
    }
}