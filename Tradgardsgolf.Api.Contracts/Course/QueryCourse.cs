using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public record QueryCourse : IRequest<CourseResponse>
{
    public Guid Id { get; set; }
}