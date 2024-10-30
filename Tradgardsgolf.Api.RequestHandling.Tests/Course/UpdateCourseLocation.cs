using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Course;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Types;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Exceptions;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Course;

[TestFixture]
public class UpdateCourseLocation
{
    [Test]
    public async Task ShouldUpdateLocation()
    {
        var ownerId = Guid.NewGuid();
        var course = Core.Entities.Course.Create(ownerId, p => p.Id = Guid.NewGuid());

        var updatedCourses = new List<Core.Entities.Course>();
        
        var arrange = Arrange.Dependencies<UpdateCourseLocationHandler, UpdateCourseLocationHandler>(dependencies =>
        {
            dependencies.UseImplementation<IResponseFactory<CourseResponse, Core.Entities.Course>, CourseResponseFactory>();
            dependencies.UseImplementation<IResponseFactory<ImageReference, Core.Entities.Course>, ImageReferenceResponseFactory>();

            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>())).ReturnsAsync(course);
                mock.Setup(x => x.UpdateAsync(It.IsAny<Core.Entities.Course>(), It.IsAny<CancellationToken>()))
                    .Callback((Core.Entities.Course c, CancellationToken t) => updatedCourses.Add(c));
                
            });
            
            dependencies.UseMock<IAuthenticationService>(mock => mock.Setup(x => x.RequireAuthenticatedUser()).Returns(
            new AuthenticatedUser
            {
                UserId = ownerId
            }));
        });

        var handler = arrange.Resolve<UpdateCourseLocationHandler>();
        var command = new UpdateCourseLocationCommand()
        {
            Id = course.Id,
            Longitude = 54.10010,
            Latitude = 46.13010,
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.Longitude.Should().Be(command.Longitude);
        result.Latitude.Should().Be(command.Latitude);
        updatedCourses.Should().HaveCount(1);
        updatedCourses.First().Longitude.Should().Be(command.Longitude);
        updatedCourses.First().Latitude.Should().Be(command.Latitude);
        updatedCourses.First().Id.Should().Be(course.Id);
    }
    
    [Test]
    public async Task ShouldThrowForbiddenWhenNotTheOwner()
    {
        var ownerId = Guid.NewGuid();
        var course = Core.Entities.Course.Create(ownerId, p => p.Id = Guid.NewGuid());

        var updatedCourses = new List<Core.Entities.Course>();
        
        var arrange = Arrange.Dependencies<UpdateCourseLocationHandler, UpdateCourseLocationHandler>(dependencies =>
        {
            dependencies.UseMock<IAuthenticationService>(mock => mock.Setup(x => x.RequireAuthenticatedUser()).Returns(
            new AuthenticatedUser
            {
                UserId = Guid.NewGuid()
            }));
            
            dependencies.UseImplementation<IResponseFactory<CourseResponse, Core.Entities.Course>, CourseResponseFactory>();
            dependencies.UseImplementation<IResponseFactory<ImageReference, Core.Entities.Course>, ImageReferenceResponseFactory>();

            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>())).ReturnsAsync(course);
                mock.Setup(x => x.UpdateAsync(It.IsAny<Core.Entities.Course>(), It.IsAny<CancellationToken>()))
                    .Callback((Core.Entities.Course c, CancellationToken t) => updatedCourses.Add(c));
                
            });
        });

        var handler = arrange.Resolve<UpdateCourseLocationHandler>();
        var command = new UpdateCourseLocationCommand()
        {
            Id = course.Id,
            Longitude = 54.10010,
            Latitude = 46.13010,
        };
        
        await handler.Invoking(x => x.Handle(command, CancellationToken.None)).Should().ThrowAsync<ForbiddenException>();
        
        updatedCourses.Should().HaveCount(0);
    }
}