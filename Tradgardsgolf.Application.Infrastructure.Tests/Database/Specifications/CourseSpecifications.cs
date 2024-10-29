using FluentAssertions;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Course;
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
        
        var specification = Specs.Course.ById(course.Id);
        var repository = new Repository<Course>(context);

        var result = await repository.FirstOrDefaultAsync(specification);
        
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
        
        var specification = Specs.Course.ById(Guid.NewGuid());
        var repository = new Repository<Course>(context);

        var result = await repository.FirstOrDefaultAsync(specification);
        result.Should().BeNull();
    }
}