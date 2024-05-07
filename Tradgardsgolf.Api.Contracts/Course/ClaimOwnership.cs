using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public class ClaimOwnership : IRequest<CourseResponse>
{
    public Guid Id { get; set; }
    
}