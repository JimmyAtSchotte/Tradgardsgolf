using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Scorecard;
using Tradgardsgolf.Contracts.Scorecard;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Scorecard;

[TestFixture]
public class SaveScorecard
{
    [Test]
    public async Task ShouldSaveScorecard()
    {
        var addedScorecards = new List<Core.Entities.Scorecard>();
        
        var arrange = Arrange.Dependencies<SaveScorecardHandler, SaveScorecardHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.AddAsync(It.IsAny<Core.Entities.Scorecard>(), It.IsAny<CancellationToken>()))
                    .Callback((Core.Entities.Scorecard c, CancellationToken t) =>
                    {
                        c.Id = Guid.NewGuid();
                        addedScorecards.Add(c);
                    });
            });
        });
        
        var handler = arrange.Resolve<SaveScorecardHandler>();
        var scores = new List<int>() { 2, 3, 5, 2, 4, 1 };
        var command = new SaveScorecardCommand()
        {
            CourseId = Guid.NewGuid(),
            Revision = 1,
            PlayerScores = new List<PlayerScore>()
            {
                new PlayerScore()
                {
                    Name = "Player A",
                    HoleScores = scores
                }
            }
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        addedScorecards.Should().HaveCount(1);
        addedScorecards.First().Scores.Should().ContainKey("Player A");
        addedScorecards.First().Scores["Player A"].Should().BeEquivalentTo(scores);

        result.PlayerScores.Should().BeEquivalentTo(command.PlayerScores);
        result.Id.Should().Be(addedScorecards.First().Id);
    }
}