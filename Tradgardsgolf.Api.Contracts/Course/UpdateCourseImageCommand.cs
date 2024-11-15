using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public class UpdateCourseImageCommand : IRequest<CourseResponse>
{
    public Guid Id { get; init; }
    public string ImageBase64 { get; init; } = string.Empty;
    public string Extension { get; init; } = string.Empty;
}