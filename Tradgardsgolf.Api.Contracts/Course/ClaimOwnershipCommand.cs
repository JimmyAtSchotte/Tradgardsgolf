using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public class ClaimOwnershipCommand : IRequest<CourseResponse>
{
    public Guid Id { get; set; }
}