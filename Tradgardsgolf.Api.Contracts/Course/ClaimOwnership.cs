using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public class ClaimOwnership : IRequest<CourseResponse>
{
    public int Id { get; set; }
    public Guid Owner { get; set; }
}