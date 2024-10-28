using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public class UpdateCourseLocationCommand : IRequest<CourseResponse>
{
    public Guid Id { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}