using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public class ResetCourseScoreCommand : IRequest<CourseResponse>
{
    public Guid CourseId { get; init; }
    public DateTime ResetDate { get; init; }
}