﻿using FluentAssertions;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;
using Tradgardsgolf.Infrastructure.Database;

namespace Tradgardsgolf.Application.Infrastructure.Tests.Database.Specifications;

[TestFixture]
public class ScorecardSpecifications
{
    [Test]
    public async Task ShouldFindScorecardsByCourse()
    {
        var course = Course.Create(Guid.NewGuid());
        var scorecard = course.CreateScorecard();

        var context = TradgardsgolfContextFactory.CreateTradgardsgolfContext();
        context.Add(course);
        await context.SaveChangesAsync();
        
        var specification = Specs.Scorecard.ByCourse(course.Id);
        var repository = new Repository(context);
        var scorecards = await repository.ListAsync(specification, CancellationToken.None);

        scorecards.Should().HaveCount(1);
        scorecards.Should().Contain(scorecard);
    }
    
    [Test]
    public async Task ShouldNotFindAnyScorecardsOnUnknownCourse()
    {
        var course = Course.Create(Guid.NewGuid());
        course.CreateScorecard();

        var context = TradgardsgolfContextFactory.CreateTradgardsgolfContext();
        context.Add(course);
        await context.SaveChangesAsync();
        
        var specification = Specs.Scorecard.ByCourse(Guid.NewGuid());
        var repository = new Repository(context);
        var scorecards = await repository.ListAsync(specification, CancellationToken.None);

        scorecards.Should().HaveCount(0);
    }
    
    
    [Test]
    public async Task ShouldFindScorecardById()
    {
        var course = Course.Create(Guid.NewGuid());
        var scorecard = course.CreateScorecard();

        var context = TradgardsgolfContextFactory.CreateTradgardsgolfContext();
        context.Add(course);
        await context.SaveChangesAsync();
        
        var specification = Specs.ById<Scorecard>(scorecard.Id);
        var repository = new Repository(context);
        var result = await repository.FirstOrDefaultAsync(specification, CancellationToken.None);

        result.Should().NotBeNull();
    }
    
    [Test]
    public async Task ShouldNotFindAnUnknownScorecardById()
    {
        var course = Course.Create(Guid.NewGuid());
        course.CreateScorecard();

        var context = TradgardsgolfContextFactory.CreateTradgardsgolfContext();
        context.Add(course);
        await context.SaveChangesAsync();
        
        var specification = Specs.ById<Scorecard>(Guid.NewGuid());
        var repository = new Repository(context);
        var result = await repository.FirstOrDefaultAsync(specification, CancellationToken.None);

        result.Should().BeNull();
    }
    
    [Test]
    public async Task ShouldFindScorecardsByTournament()
    {
        var tournament = Tournament.Create("Test");
        
        var context = TradgardsgolfContextFactory.CreateTradgardsgolfContext();
        context.Add(tournament);
        
        var course = Course.Create(Guid.NewGuid());
        var scorecard = course.CreateScorecard();
        scorecard.TournamentId = tournament.Id;
        context.Add(course);
        
        await context.SaveChangesAsync();
        
        var specification = Specs.Scorecard.ByTournament(tournament.Id);
        var repository = new Repository(context);
        var scorecards = await repository.ListAsync(specification, CancellationToken.None);

        scorecards.Should().HaveCount(1);
    }
}