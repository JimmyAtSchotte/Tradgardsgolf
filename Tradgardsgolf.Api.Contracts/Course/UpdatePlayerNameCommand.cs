using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public class UpdatePlayerNameCommand : IRequest<Unit>
{
    public string NewName { get; set; }
    public Guid CourseId { get; set; }
    public string OldName { get; set; }
}