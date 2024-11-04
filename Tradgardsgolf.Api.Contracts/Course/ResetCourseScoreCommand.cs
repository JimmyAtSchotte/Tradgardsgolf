using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public class ResetCourseScoreCommand : IRequest<CourseResponse>
{
    public Guid CourseId { get; set; }
    public DateTime ResetDate { get; set; }
}