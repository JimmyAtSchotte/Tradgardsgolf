using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public record GetCourseCommand : IRequest<CourseResponse>
{
    public int Id { get; set; }
}