using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public class UpdateCourseLocationCommand : IRequest<CourseResponse>
{
    public Guid Id { get; init; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }
}