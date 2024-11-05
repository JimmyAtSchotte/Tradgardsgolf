using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Course;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Exceptions;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Course;

[TestFixture]
public class UpdatePlayerName
{
    [Test]
    public async Task ShouldUpdatePlayerName()
    {
        var repositorySpy = default(Mock<IRepository>);
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = course.CreateScorecard();
        scorecard.AddPlayerScores("Test", 1,1,1,1,1,1);
        
        var arrange = Arrange.Dependencies<UpdatePlayerNameHandler, UpdatePlayerNameHandler>(dependencies =>
        {
            dependencies.UseMock(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
                
                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course.Scorecards);
                
            }, out repositorySpy);
            
            dependencies.UseMock<IAuthenticationService>(mock => mock.Setup(x => x.RequireAuthenticatedUser()).Returns(
            new AuthenticatedUser()
            {
                UserId = course.OwnerGuid
            }));
        });
        
        var handler = arrange.Resolve<UpdatePlayerNameHandler>();
        var command = new UpdatePlayerNameCommand()
        {
            CourseId = course.Id,
            OldName = "Test",
            NewName = "Jimmy"
        };
        
        await handler.Handle(command, CancellationToken.None);
        
        repositorySpy.Verify(x => x.UpdateRangeAsync(It.Is<Core.Entities.Scorecard[]>(a => a.Any(s => s.Scores.ContainsKey(command.NewName))), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Test]
    public async Task ShouldThrowForbbidenWhenNotTheCourseOwner()
    {
        var repositorySpy = default(Mock<IRepository>);
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = course.CreateScorecard();
        scorecard.AddPlayerScores("Test", 1,1,1,1,1,1);
        
        var arrange = Arrange.Dependencies<UpdatePlayerNameHandler, UpdatePlayerNameHandler>(dependencies =>
        {
            dependencies.UseMock(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
                
                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course.Scorecards);
                
            }, out repositorySpy);

            dependencies.UseMock<IAuthenticationService>(mock => mock.Setup(x => x.RequireAuthenticatedUser()).Returns(
            new AuthenticatedUser()
            {
                UserId = Guid.NewGuid()
            }));
        });
        
        var handler = arrange.Resolve<UpdatePlayerNameHandler>();
        var command = new UpdatePlayerNameCommand()
        {
            CourseId = course.Id,
            OldName = "Test",
            NewName = "Jimmy"
        };
        
        await handler.Invoking(x => x.Handle(command, CancellationToken.None)).Should().ThrowAsync<ForbiddenException>();
    }
}