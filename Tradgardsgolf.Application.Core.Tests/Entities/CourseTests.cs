using FluentAssertions;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Application.Core.Tests.Entities;

[TestFixture]
public class CourseTests
{
    [Test]
    public void ShouldCreateCourseWithPropertiesSet()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Name = "Test");
        course.Name.Should().Be("Test");
    }
    
    [Test]
    public void ShouldHaveEmptyScorecards()
    {
        var course = Course.Create(Guid.NewGuid());
        course.Scorecards.Should().BeEmpty();
    }
    

    
}