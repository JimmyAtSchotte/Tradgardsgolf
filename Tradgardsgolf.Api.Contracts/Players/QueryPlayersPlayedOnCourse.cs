using System;
using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Players;

public record QueryPlayersPlayedOnCourse : IRequest<IEnumerable<PlayerResponse>>
{
    public Guid CourseId { get; init; }
}