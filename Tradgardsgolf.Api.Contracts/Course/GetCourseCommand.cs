using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public record GetCourseCommand : IRequest<CourseResponse>
{
    public Guid Id { get; set; }
}