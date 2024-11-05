using Ardalis.Specification;
using FluentAssertions;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Infrastructure.Database;

namespace Tradgardsgolf.Application.Infrastructure.Tests.Database.Specifications;

[TestFixture]
public class CourseSpecifications
{
    [Test]
    public async Task ShouldFindExistingCourseById()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Name = "TestCourse");
        var context = TradgardsgolfContextFactory.CreateTradgardsgolfContext();
        context.Add(course);
        await context.SaveChangesAsync();
        
        var specification = Specs.ById<Course>(course.Id);
        var repository = new Repository(context);

        var result = await repository.FirstOrDefaultAsync(specification, CancellationToken.None);
        
        result.Should().NotBeNull();
        result.Name.Should().Be(course.Name);
    }
    
    [Test]
    public async Task ShouldNotFindAnUnknownCourseById()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Name = "TestCourse");
        var context = TradgardsgolfContextFactory.CreateTradgardsgolfContext();
        context.Add(course);
        await context.SaveChangesAsync();
        
        var specification = Specs.ById<Course>(Guid.NewGuid());
        var repository = new Repository(context);

        var result = await repository.FirstOrDefaultAsync(specification, CancellationToken.None);
        result.Should().BeNull();
    }
    
    [Ignore("Including different entitites doesnt work with Cosmos DB")]
    [Test]
    public async Task ShouldIncludeScorecards()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Name = "TestCourse");
        var scorecard = course.CreateScorecard();
        scorecard.AddPlayerScores("test", 1,1,1,1,1,1);
        
        var context = TradgardsgolfContextFactory.CreateTradgardsgolfContext();
        context.Add(course);
        await context.SaveChangesAsync();
        
        var specification = Specs.ById<Course>(course.Id, spec => spec.Include(x => x.Scorecards).AsSplitQuery());
        var repository = new Repository(context);

        var result = await repository.FirstOrDefaultAsync(specification, CancellationToken.None);
        result.Scorecards.Should().HaveCount(1);
    }
}