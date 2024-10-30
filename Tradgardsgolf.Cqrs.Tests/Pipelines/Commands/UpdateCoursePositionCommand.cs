using Tradgardsgolf.Contracts.Course;

namespace Tradgardsgolf.Cqrs.Tests.Pipelines.Commands;

public class UpdateCoursePositionCommand : BaseCommand<CourseResponse>
{
    public Guid CourseId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}