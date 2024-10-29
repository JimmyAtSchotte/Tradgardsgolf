using System.Security.Claims;
using AspNetMonsters.Blazor.Geolocation;
using Bunit;
using Moq;
using Tradgardsgolf.BlazorWasm.ApiServices;
using Tradgardsgolf.BlazorWasm.Components;
using Tradgardsgolf.BlazorWasm.Components.Course;
using Tradgardsgolf.BlazorWasm.Pages;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Settings;

namespace Tradgardsgolf.Blazor.Wasm.Tests.Pages;

[TestFixture]
public class CoursesTests
{
    private static Action<Mock<IApiDispatcher>> ApiDispatcherMockSetup(int allowPlayDistance,
        params CourseResponse[] courseResponses)
    {
        return apiMock =>
        {
            apiMock
                .Setup(x => x.Dispatch(It.IsAny<QueryAllowPlayDistance>()))
                .ReturnsAsync(new SettingResponse<int>
                {
                    Value = allowPlayDistance
                });

            apiMock
                .Setup(x => x.Dispatch(It.IsAny<QueryAllCourses>()))
                .ReturnsAsync(courseResponses);
        };
    }


    [Test]
    public void ListCourses()
    {
        using var contextBuilder = new TestContextBuilder();

        contextBuilder.UseMock(ApiDispatcherMockSetup(1, new CourseResponse()));

        var courses = contextBuilder
            .Build()
            .RenderComponent<Courses>(parameters => { parameters.Add(p => p.Location, new Location()); });

        var courseCards = courses
            .FindComponents<CascadingCourse>();

        Assert.That(courseCards.Count, Is.EqualTo(1));
    }


    [Test]
    public void CourseOwner()
    {
        var ownerGuid = Guid.NewGuid();

        using var contextBuilder = new TestContextBuilder();
        contextBuilder.UseMock(ApiDispatcherMockSetup(1, new CourseResponse
        {
            OwnerGuid = ownerGuid
        }));

        var courses = contextBuilder
            .UseAuthorization(auth =>
            {
                auth.SetAuthorized("user");
                auth.SetClaims(new Claim("oid", ownerGuid.ToString()));
            })
            .Build()
            .RenderComponent<Courses>(parameters => { parameters.Add(p => p.Location, new Location()); });

        var edit = courses
            .FindComponent<CascadingCourse>()
            .FindComponent<ConditionalComponent>(x => x.Instance.Name == "Edit");

        Assert.That(edit.Markup, Is.Not.Empty);
    }

    [Test]
    public void AuthorizedAsNoneOwner()
    {
        using var contextBuilder = new TestContextBuilder();

        contextBuilder.UseMock(ApiDispatcherMockSetup(1, new CourseResponse
        {
            OwnerGuid = Guid.NewGuid()
        }));

        var courses = contextBuilder
            .UseAuthorization(auth =>
            {
                auth.SetAuthorized("user");
                auth.SetClaims(new Claim("oid", Guid.NewGuid().ToString()));
            })
            .Build()
            .RenderComponent<Courses>(parameters => { parameters.Add(p => p.Location, new Location()); });

        var edit = courses
            .FindComponent<CascadingCourse>()
            .FindComponent<ConditionalComponent>(x => x.Instance.Name == "Edit");

        Assert.That(edit.Markup, Is.Empty);
    }

    [Test]
    public void InRangeToPlay()
    {
        using var contextBuilder = new TestContextBuilder();
        var course = new CourseResponse
        {
            Longitude = 1,
            Latitude = 1
        };

        contextBuilder.UseMock(ApiDispatcherMockSetup(1, course));

        var courses = contextBuilder
            .Build()
            .RenderComponent<Courses>(parameters =>
            {
                parameters.Add(p => p.Location, new Location
                {
                    Longitude = (decimal)course.Longitude,
                    Latitude = (decimal)course.Latitude
                });
            });

        var play = courses
            .FindComponent<CascadingCourse>()
            .FindComponent<ConditionalComponent>(x => x.Instance.Name == "Play");

        Assert.That(play.Markup, Is.Not.Empty);
    }

    [Test]
    public void ToFarAwayToPlay()
    {
        using var contextBuilder = new TestContextBuilder();

        contextBuilder.UseMock(ApiDispatcherMockSetup(1, new CourseResponse
        {
            Longitude = 1,
            Latitude = 1
        }));

        var courses = contextBuilder
            .Build()
            .RenderComponent<Courses>(parameters =>
            {
                parameters.Add(p => p.Location, new Location
                {
                    Latitude = 10,
                    Longitude = 10
                });
            });

        var play = courses
            .FindComponent<CascadingCourse>()
            .FindComponent<ConditionalComponent>(x => x.Instance.Name == "Play");


        Assert.That(play.Markup, Is.Empty);
    }

    [Test]
    public void ClaimOwnerShip()
    {
        using var contextBuilder = new TestContextBuilder();
        var course = new CourseResponse
        {
            Longitude = 1,
            Latitude = 1
        };

        contextBuilder.UseMock(ApiDispatcherMockSetup(1, course));
        contextBuilder.UseAuthorization(auth =>
        {
            auth.SetAuthorized("test");
            auth.SetClaims(new Claim("oid", Guid.NewGuid().ToString()));
        });

        var courses = contextBuilder
            .Build()
            .RenderComponent<Courses>(parameters =>
            {
                parameters.Add(p => p.Location, new Location
                {
                    Longitude = (decimal)course.Longitude,
                    Latitude = (decimal)course.Latitude
                });
            });

        var claimOwnerShip = courses
            .FindComponent<CascadingCourse>()
            .FindComponent<ConditionalComponent>(x => x.Instance.Name == "ClaimOwnerShip");

        Assert.That(claimOwnerShip.Markup, Is.Not.Empty);
    }

    [Test]
    public void InvalidUserId()
    {
        using var contextBuilder = new TestContextBuilder();
        var course = new CourseResponse
        {
            Longitude = 1,
            Latitude = 1
        };

        contextBuilder.UseMock(ApiDispatcherMockSetup(1, course));
        contextBuilder.UseAuthorization(auth => { auth.SetAuthorized("test"); });

        var courses = contextBuilder
            .Build()
            .RenderComponent<Courses>(parameters =>
            {
                parameters.Add(p => p.Location, new Location
                {
                    Longitude = (decimal)course.Longitude,
                    Latitude = (decimal)course.Latitude
                });
            });

        var claimOwnerShip = courses
            .FindComponent<CascadingCourse>()
            .FindComponent<ConditionalComponent>(x => x.Instance.Name == "ClaimOwnerShip");

        var edit = courses
            .FindComponent<CascadingCourse>()
            .FindComponent<ConditionalComponent>(x => x.Instance.Name == "Edit");

        Assert.Multiple(() =>
        {
            Assert.That(claimOwnerShip.Markup, Is.Empty);
            Assert.That(edit.Markup, Is.Empty);
        });
    }
}