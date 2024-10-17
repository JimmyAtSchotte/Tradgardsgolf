using FluentAssertions;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Application.Core.Tests.Entities;

[TestFixture]
public class TournamentTests
{
    [Test]
    public void ShouldCreateTournament()
    {
        var tournament = Tournament.Create("Test");
        tournament.Name.Should().Be("Test");
    }
    
    [Test]
    public void ShouldAddCourseDates()
    {
        var tournament = Tournament.Create("Test");
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var date = DateTime.Today;

        tournament.AddCourseDate(course, date);
        
        tournament.TournamentCourseDates.Should().HaveCount(1);
        tournament.TournamentCourseDates.First().Date.Should().Be(date);
        tournament.TournamentCourseDates.First().Course.Should().Be(course);
        tournament.TournamentCourseDates.First().CourseId.Should().Be(course.Id);
    }
}