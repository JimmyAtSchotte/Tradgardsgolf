using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Tournament;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;
using SUT = Tradgardsgolf.Api.RequestHandling.Tournament.QueryTournamentResultsHandler;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Tournament;

[TestFixture]
public class QueryTournamentResults
{
    [Test]
    public async Task NoTournaments()
    {
        var arrange = Arrange.Dependencies<SUT, SUT>();
        var handler = arrange.Resolve<QueryTournamentResultsHandler>();
        var query = new QueryTournamentResultsCommand();
        
        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().BeEmpty();
    }
    
    [Test]
    public async Task HasTournaments()
    {
        var tournament = Core.Entities.Tournament.Create("Test");
        var course = Core.Entities.Course.Create(Guid.NewGuid(),  p => p.Id = new Guid());
        tournament.AddCourseDate(course.Id, DateTime.Today);
        
        var arrange = Arrange.Dependencies<SUT, SUT>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync<Core.Entities.Tournament>(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Core.Entities.Tournament>() { tournament });
            });
        });
        
        var handler = arrange.Resolve<QueryTournamentResultsHandler>();
        var query = new QueryTournamentResultsCommand();
        
        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().HaveCount(1);
    }
    
    
    [Test]
    public async Task ShouldSumUpScorecard()
    {
        var tournament = Core.Entities.Tournament.Create("Test");
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        tournament.AddCourseDate(course.Id, DateTime.Today);

        var scorecard = Core.Entities.Scorecard.Create(course.Id, course.Revision);
        
        scorecard.AddPlayerScores("player A", 1, 1);
        scorecard.AddPlayerScores("player B", 1, 2);
        scorecard.TournamentId = tournament.Id;
        
        var arrange = Arrange.Dependencies<SUT, SUT>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync<Core.Entities.Tournament>(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Core.Entities.Tournament>() { tournament });

                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByTournament(tournament.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([ scorecard ]);
            });
        });
        
        var handler = arrange.Resolve<QueryTournamentResultsHandler>();
        var query = new QueryTournamentResultsCommand();
        
        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().HaveCount(1);
        result.First().PlayerTournamentScores.ElementAt(0).Name.Should().Be("player A");
        result.First().PlayerTournamentScores.ElementAt(0).Results.ElementAt(0).Should().Be(2);
        result.First().PlayerTournamentScores.ElementAt(0).Total.Should().Be(2);

        result.First().PlayerTournamentScores.ElementAt(1).Name.Should().Be("player B");
        result.First().PlayerTournamentScores.ElementAt(1).Results.ElementAt(0).Should().Be(3);
        result.First().PlayerTournamentScores.ElementAt(1).Total.Should().Be(3);
    }
    
    
    [Test]
    public async Task ShouldSumUpMulipleScorecards()
    {
        var tournament = Core.Entities.Tournament.Create("Test");
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        tournament.AddCourseDate(course.Id, DateTime.Today);
        var scorecard1 = Core.Entities.Scorecard.Create(course.Id, course.Revision);
        scorecard1.AddPlayerScores("player A", 1, 1);
        scorecard1.AddPlayerScores("player B", 1, 2);
        scorecard1.TournamentId = tournament.Id;
        
        var scorecard2 =  Core.Entities.Scorecard.Create(course.Id, course.Revision);
        scorecard2.AddPlayerScores("player A", 1, 1);
        scorecard2.AddPlayerScores("player B", 1, 2);
        scorecard2.TournamentId = tournament.Id;
        
        var arrange = Arrange.Dependencies<SUT, SUT>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync<Core.Entities.Tournament>(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Core.Entities.Tournament>() { tournament });

                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByTournament(tournament.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new[] { scorecard1, scorecard2 });
            });
        });
        
        var handler = arrange.Resolve<QueryTournamentResultsHandler>();
        var query = new QueryTournamentResultsCommand();
        
        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().HaveCount(1);
        result.First().PlayerTournamentScores.ElementAt(0).Name.Should().Be("player A");
        result.First().PlayerTournamentScores.ElementAt(0).Results.ElementAt(0).Should().Be(2);
        result.First().PlayerTournamentScores.ElementAt(0).Results.ElementAt(1).Should().Be(2);
        result.First().PlayerTournamentScores.ElementAt(0).Total.Should().Be(4);

        result.First().PlayerTournamentScores.ElementAt(1).Name.Should().Be("player B");
        result.First().PlayerTournamentScores.ElementAt(1).Results.ElementAt(0).Should().Be(3);
        result.First().PlayerTournamentScores.ElementAt(1).Results.ElementAt(1).Should().Be(3);
        result.First().PlayerTournamentScores.ElementAt(1).Total.Should().Be(6);
    }
    
    [Test]
    public async Task ShouldOrderByCompletedScoreCardsThenLowestTotalScore()
    {
        var tournament = Core.Entities.Tournament.Create("Test");
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        tournament.AddCourseDate(course.Id, DateTime.Today);
        var scorecard1 =  Core.Entities.Scorecard.Create(course.Id, course.Revision);
        scorecard1.AddPlayerScores("player A", 1, 2);
        scorecard1.AddPlayerScores("player B", 1, 1);
        scorecard1.AddPlayerScores("player C", 1, 1);
        scorecard1.TournamentId = tournament.Id;
        
        var scorecard2 =  Core.Entities.Scorecard.Create(course.Id, course.Revision);
        scorecard2.AddPlayerScores("player A", 1, 2);
        scorecard2.AddPlayerScores("player B", 1, 1);
        scorecard2.TournamentId = tournament.Id;
        
        var arrange = Arrange.Dependencies<SUT, SUT>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync<Core.Entities.Tournament>(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Core.Entities.Tournament>() { tournament });

                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByTournament(tournament.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new[] { scorecard1, scorecard2 });
            });
        });
        
        var handler = arrange.Resolve<QueryTournamentResultsHandler>();
        var query = new QueryTournamentResultsCommand();
        
        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().HaveCount(1);
        result.First().PlayerTournamentScores.ElementAt(0).Name.Should().Be("player B");
        result.First().PlayerTournamentScores.ElementAt(0).Total.Should().Be(4);

        result.First().PlayerTournamentScores.ElementAt(1).Name.Should().Be("player A");
        result.First().PlayerTournamentScores.ElementAt(1).Total.Should().Be(6);
        
        result.First().PlayerTournamentScores.ElementAt(2).Name.Should().Be("player C");
        result.First().PlayerTournamentScores.ElementAt(2).Total.Should().Be(2);
    }
}