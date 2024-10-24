﻿using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Infrastructure.Database;

namespace Tradgardsgolf.Application.Infrastructure.Tests.Database.Specifications;

[TestFixture]
public class TournamentSpecifications
{
    [Test]
    public async Task ShouldFindTournamentsByCourse()
    {
        var course = Course.Create(Guid.NewGuid()); 
        
        var tournament = Tournament.Create("Test");
        tournament.AddCourseDate(course, DateTime.Today);
        
        var context = TradgardsgolfContextFactory.CreateTradgardsgolfContext();
        context.Add(course);
        context.Add(tournament);
        
        await context.SaveChangesAsync();
        
        var specification = new Core.Specifications.Tournament.TournamentsOnCourse(course.Id, DateTime.Today);
        var repository = new Repository<Tournament>(context);
        var tournaments = await repository.ListAsync(specification);
        tournaments.Should().HaveCount(1);
    }
    
    [Test]
    public async Task ShouldNotFindTournamentsOnDifferentDate()
    {
        var course = Course.Create(Guid.NewGuid()); 
        
        var tournament = Tournament.Create("Test");
        tournament.AddCourseDate(course, DateTime.Now.AddDays(1).Date);
        
        var context = TradgardsgolfContextFactory.CreateTradgardsgolfContext();
        context.Add(course);
        context.Add(tournament);
        
        await context.SaveChangesAsync();
        
        var specification = new Core.Specifications.Tournament.TournamentsOnCourse(course.Id, DateTime.Today);
        var repository = new Repository<Tournament>(context);
        var tournaments = await repository.ListAsync(specification);
        tournaments.Should().HaveCount(0);
    }
}