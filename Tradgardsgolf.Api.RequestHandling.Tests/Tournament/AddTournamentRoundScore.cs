using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Tournament;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Tournament;

[TestFixture]
public class AddTournamentRoundScore
{
    [Test]
    public async Task ShouldUpdateScorecard()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = course.CreateScorecard();
        scorecard.Id = Guid.NewGuid();
        
        var tournament = Core.Entities.Tournament.Create("Tournament");
        tournament.Id = Guid.NewGuid();
        
        var updatedScorecards = new List<Core.Entities.Scorecard>();

        var arrange = Arrange.Dependencies<AddTournamentRoundScoreHandler,AddTournamentRoundScoreHandler>(
                dependencies =>
                {
                    dependencies.UseMock<IRepository<Core.Entities.Scorecard>>(mock =>
                    {
                        mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Scorecard>(scorecard.Id), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(scorecard);

                        mock.Setup(x => x.UpdateAsync(It.IsAny<Core.Entities.Scorecard>(), It.IsAny<CancellationToken>()))
                            .Callback((Core.Entities.Scorecard scorecard, CancellationToken _) => updatedScorecards.Add(scorecard));
                    });
                });

        var handler = arrange.Resolve<AddTournamentRoundScoreHandler>();
        var command = new AddTournamentRoundScoreCommand()
        {
            ScorecardId = scorecard.Id,
            TournamentId = tournament.Id
        };
        
        await handler.Handle(command, CancellationToken.None);

        updatedScorecards.Should().HaveCount(1);
        updatedScorecards.First().TournamentId.Should().Be(tournament.Id);
    }
}