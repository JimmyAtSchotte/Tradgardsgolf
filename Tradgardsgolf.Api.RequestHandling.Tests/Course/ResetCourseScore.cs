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
public class ResetCourseScore
{
    [Test]
    public async Task ShouldNotUpdateWhenNotTheOwner()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var authenticatedUser = new AuthenticatedUser
        {
            UserId = Guid.NewGuid()
        };
        
        var arrange = Arrange.Dependencies<ResetCourseScoreHandler, ResetCourseScoreHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
            });
            
            dependencies.UseMock<IAuthenticationService>(mock => mock.Setup(x => x.RequireAuthenticatedUser()).Returns(authenticatedUser));
        });
        
        var handler = arrange.Resolve<ResetCourseScoreHandler>();
        var command = new ResetCourseScoreCommand
        {
            CourseId = course.Id,
            ResetDate = DateTime.Today
        };
        
        await handler.Invoking(h => h.Handle(command, CancellationToken.None)).Should().ThrowAsync<ForbiddenException>();
        course.ScoreReset.Should().BeNull();
    }
    
    [Test]
    public async Task ShouldResetScore()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var authenticatedUser = new AuthenticatedUser
        {
            UserId = course.OwnerGuid
        };
        
        var repositorySpy = default(Mock<IRepository>);
        
        var arrange = Arrange.Dependencies<ResetCourseScoreHandler, ResetCourseScoreHandler>(dependencies =>
        {
            dependencies.UseMock(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
            }, out repositorySpy);
            
            dependencies.UseMock<IAuthenticationService>(mock => mock.Setup(x => x.RequireAuthenticatedUser()).Returns(authenticatedUser));
            
            
            dependencies.UseImplementation<IResponseFactory<CourseResponse, Core.Entities.Course>, CourseResponseFactory>();
            dependencies.UseImplementation<IResponseFactory<ImageReference?, Core.Entities.Course>, ImageReferenceResponseFactory>();
        });
        
        var handler = arrange.Resolve<ResetCourseScoreHandler>();
        var command = new ResetCourseScoreCommand
        {
            CourseId = course.Id,
            ResetDate = DateTime.Today
        };
        
        var response = await handler.Handle(command, CancellationToken.None);
        course.ScoreReset.Should().Be(command.ResetDate);
        course.GetRevision().Should().Be(1);
        
        repositorySpy!.Verify(spy => spy.UpdateAsync(course, It.IsAny<CancellationToken>()), Times.Once);
        response.ScoreReset.Should().Be(command.ResetDate);
    }
    

    
}