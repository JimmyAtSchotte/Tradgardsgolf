using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public class UpdatePlayerNameCommand : IRequest<Unit>
{
    public string NewName { get; init; } = string.Empty;
    public Guid CourseId { get; init; }
    public string OldName { get; init; } = string.Empty;
}