using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Course;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.CourseSeason;
using Tradgardsgolf.Core.Specifications.PlayerStatistic;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Course;

[TestFixture]
public class RecalculateStatistics
{
    [Test]
    public async Task NoScoreReset()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = Core.Entities.Scorecard.Create(course.Id, 0);
        scorecard.AddPlayerScores("Player 1", 1, 1, 1);

        var repositorySpy = default(Mock<IRepository>);

        var arrange = Arrange.Dependencies<RecalculateStatisticsHandler, RecalculateStatisticsHandler>(dependencies =>
        {
            dependencies.UseMock(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
                
                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([scorecard]);
                
                mock.Setup(x => x.ListAsync(Specs.PlayerStatistic.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([]);
                
                mock.Setup(x => x.ListAsync<Core.Entities.CourseSeason>(Specs.CourseSeason.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([]);
                
            }, out repositorySpy);

            dependencies.UseMock<IAuthenticationService>(mock =>
            {
                mock.Setup(x => x.RequireAuthenticatedUser())
                    .Returns(new AuthenticatedUser() { UserId = course.OwnerGuid });
            });
        });
        
        var handler = arrange.Resolve<RecalculateStatisticsHandler>();
        var command = new RecalculateStatisticsCommand()
        {
            CourseId = course.Id,
        };
        
        await handler.Handle(command, CancellationToken.None);
        
        repositorySpy.Verify(x => x.AddRangeAsync(It.Is<Core.Entities.PlayerStatistic[]>(entities => entities.Length == 1), It.IsAny<CancellationToken>()), Times.Once);
        repositorySpy.Verify(x => x.AddRangeAsync(It.Is<Core.Entities.CourseSeason[]>(entities => entities.Length == 1), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Test]
    public async Task MultipleScorecardsForPlayer()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard1 = Core.Entities.Scorecard.Create(course.Id, 0);
        scorecard1.AddPlayerScores("Player 1", 1, 1, 1);
        
        var scorecard2 = Core.Entities.Scorecard.Create(course.Id, 0);
        scorecard2.AddPlayerScores("Player 1", 1, 1, 1);

        var repositorySpy = default(Mock<IRepository>);

        var arrange = Arrange.Dependencies<RecalculateStatisticsHandler, RecalculateStatisticsHandler>(dependencies =>
        {
            dependencies.UseMock(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
                
                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([scorecard1, scorecard2]);
                
                mock.Setup(x => x.ListAsync(Specs.PlayerStatistic.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([]);
                
                mock.Setup(x => x.ListAsync<Core.Entities.CourseSeason>(Specs.CourseSeason.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([]);
                
            }, out repositorySpy);

            dependencies.UseMock<IAuthenticationService>(mock =>
            {
                mock.Setup(x => x.RequireAuthenticatedUser())
                    .Returns(new AuthenticatedUser() { UserId = course.OwnerGuid });
            });
        });
        
        var handler = arrange.Resolve<RecalculateStatisticsHandler>();
        var command = new RecalculateStatisticsCommand()
        {
            CourseId = course.Id,
        };
        
        await handler.Handle(command, CancellationToken.None);
        
        
        
        repositorySpy.Verify(x => x.AddRangeAsync(It.Is<Core.Entities.PlayerStatistic[]>(entities => entities.Length == 1), It.IsAny<CancellationToken>()), Times.Once);
        repositorySpy.Verify(x => x.AddRangeAsync(It.Is<Core.Entities.CourseSeason[]>(entities => entities.Length == 1), It.IsAny<CancellationToken>()), Times.Once);
        
    }
    
    
    [Test]
    public async Task MultipleScorecardsForPlayerWithDifferentRevisions()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard1 = Core.Entities.Scorecard.Create(course.Id, course.Revision);
        scorecard1.AddPlayerScores("Player 1", 1, 1, 1);
        scorecard1.Date = DateTime.Now.AddDays(-1);

        course.ResetScore(DateTime.Today);
        
        var scorecard2 = Core.Entities.Scorecard.Create(course.Id, course.Revision);
        scorecard2.AddPlayerScores("Player 1", 1, 1, 1);

        var repositorySpy = default(Mock<IRepository>);

        var arrange = Arrange.Dependencies<RecalculateStatisticsHandler, RecalculateStatisticsHandler>(dependencies =>
        {
            dependencies.UseMock(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
                
                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([scorecard1, scorecard2]);
                
                mock.Setup(x => x.ListAsync(Specs.PlayerStatistic.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([]);
                
                mock.Setup(x => x.ListAsync<Core.Entities.CourseSeason>(Specs.CourseSeason.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([]);
                
            }, out repositorySpy);

            dependencies.UseMock<IAuthenticationService>(mock =>
            {
                mock.Setup(x => x.RequireAuthenticatedUser())
                    .Returns(new AuthenticatedUser() { UserId = course.OwnerGuid });
            });
        });
        
        var handler = arrange.Resolve<RecalculateStatisticsHandler>();
        var command = new RecalculateStatisticsCommand()
        {
            CourseId = course.Id,
        };
        
        await handler.Handle(command, CancellationToken.None);
        
        repositorySpy.Verify(x => x.AddRangeAsync(It.Is<Core.Entities.PlayerStatistic[]>(entities => entities.Length == 2), It.IsAny<CancellationToken>()), Times.Once);
        repositorySpy.Verify(x => x.AddRangeAsync(It.Is<Core.Entities.CourseSeason[]>(entities => entities.Length == 1), It.IsAny<CancellationToken>()), Times.Once);

    }
    
    [Test]
    public async Task LegacyScoreReset()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard1 = Core.Entities.Scorecard.Create(course.Id, course.Revision);
        scorecard1.AddPlayerScores("Player 1", 1, 1, 1);
        scorecard1.Date = DateTime.Now.AddDays(-1);
        course.ScoreReset = DateTime.Today;
        
        var scorecard2 = Core.Entities.Scorecard.Create(course.Id, course.Revision);
        scorecard2.AddPlayerScores("Player 1", 1, 1, 1);

        var repositorySpy = default(Mock<IRepository>);

        var arrange = Arrange.Dependencies<RecalculateStatisticsHandler, RecalculateStatisticsHandler>(dependencies =>
        {
            dependencies.UseMock(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
                
                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([scorecard1, scorecard2]);
                
                mock.Setup(x => x.ListAsync(Specs.PlayerStatistic.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([]);
                
                mock.Setup(x => x.ListAsync<Core.Entities.CourseSeason>(Specs.CourseSeason.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([]);
                
            }, out repositorySpy);

            dependencies.UseMock<IAuthenticationService>(mock =>
            {
                mock.Setup(x => x.RequireAuthenticatedUser())
                    .Returns(new AuthenticatedUser() { UserId = course.OwnerGuid });
            });
        });
        
        var handler = arrange.Resolve<RecalculateStatisticsHandler>();
        var command = new RecalculateStatisticsCommand()
        {
            CourseId = course.Id,
        };
        
        await handler.Handle(command, CancellationToken.None);
        
        
        repositorySpy.Verify(x => x.AddRangeAsync(It.Is<Core.Entities.PlayerStatistic[]>(entities => entities.Length == 2), It.IsAny<CancellationToken>()), Times.Once);
        repositorySpy.Verify(x => x.AddRangeAsync(It.Is<Core.Entities.CourseSeason[]>(entities => entities.Length == 1), It.IsAny<CancellationToken>()), Times.Once);
        repositorySpy.Verify(x => x.UpdateRangeAsync(It.Is<Core.Entities.Scorecard[]>(entities => entities.Length == 1), It.IsAny<CancellationToken>()), Times.Once);
    }
}