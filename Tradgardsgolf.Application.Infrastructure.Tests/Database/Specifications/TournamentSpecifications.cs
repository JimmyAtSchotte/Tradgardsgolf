using FluentAssertions;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Tournament;
using Tradgardsgolf.Infrastructure.Database;

namespace Tradgardsgolf.Application.Infrastructure.Tests.Database.Specifications;

[TestFixture]
public class TournamentSpecifications
{
    [Test]
    public async Task ShouldFindTournamentsByCourse()
    {
        var courseId = Guid.NewGuid();
        
        var tournament = Tournament.Create("Test");
        tournament.AddCourseDate(courseId, DateTime.Today);
        
        var context =  await TradgardsgolfContextFactory.CreateTradgardsgolfContext();
        context.Add(tournament);
        
        await context.SaveChangesAsync();
        
        var specification = Specs.Tournament.ByCourseAndDate(courseId, DateTime.Today);
        var repository = new Repository(context);
        var tournaments = await repository.ListAsync(specification, CancellationToken.None);
        tournaments.Should().HaveCount(1);
    }
    
    [Test]
    public async Task ShouldNotFindTournamentsOnDifferentDate()
    {
        var courseId = Guid.NewGuid();
        
        var tournament = Tournament.Create("Test");
        tournament.AddCourseDate(courseId, DateTime.Now.AddDays(1).Date);
        
        var context =  await TradgardsgolfContextFactory.CreateTradgardsgolfContext();
        context.Add(tournament);
        
        await context.SaveChangesAsync();
        
        var specification = Specs.Tournament.ByCourseAndDate(courseId, DateTime.Today);
        var repository = new Repository(context);
        var tournaments = await repository.ListAsync(specification, CancellationToken.None);
        tournaments.Should().HaveCount(0);
    }
}