using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Tournament;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Tournament;

[TestFixture]
public class QueryTournamentScores
{
    
    [Test]
    public async Task ShouldHaveNoScorecards()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid());
        var tournament = Core.Entities.Tournament.Create("Tournament");
        tournament.Id = Guid.NewGuid();

        var scorecards = new List<Core.Entities.Scorecard>();
        
        var arrange = Arrange.Dependencies<QueryTournamentScoresHandler, QueryTournamentScoresHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository<Core.Entities.Scorecard>>(mock =>
            {
                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByTournament(tournament.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(scorecards);
            });
        });
        
        var handler = arrange.Resolve<QueryTournamentScoresHandler>();
        var command = new Contracts.Tournament.QueryTournamentScores()
        {
            TournamentId = tournament.Id,
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().BeEmpty();
    }
    
    [Test]
    public async Task ShouldTransformScorecardsToTournamentScores()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid());
        var tournament = Core.Entities.Tournament.Create("Tournament");
        tournament.Id = Guid.NewGuid();
        
        var scorecards = new List<Core.Entities.Scorecard>();
        var rounds = 4;
        
        for (int i = 0; i < rounds; i++)
        {
            var scorecard = course.CreateScorecard();
            scorecard.TournamentId = tournament.Id;
            scorecard.AddPlayerScores("Player A", 3, 4, 2);
            scorecard.AddPlayerScores("Player B", 5, 6, 4);
            scorecards.Add(scorecard);
        }
        
        var arrange = Arrange.Dependencies<QueryTournamentScoresHandler, QueryTournamentScoresHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository<Core.Entities.Scorecard>>(mock =>
            {
                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByTournament(tournament.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(scorecards);
            });
        });
        
        var handler = arrange.Resolve<QueryTournamentScoresHandler>();
        var command = new Contracts.Tournament.QueryTournamentScores()
        {
            TournamentId = tournament.Id,
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().HaveCount(2);
        result.First(x => x.Name == "Player A").Score.Should().Be(rounds * 9);
        result.First(x => x.Name == "Player B").Score.Should().Be(rounds * 15);


    }
}