using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public record UpdateCourseCommand : IRequest<CourseResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
}