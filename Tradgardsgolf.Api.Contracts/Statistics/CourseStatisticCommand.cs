using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Statistics;

public record CourseStatisticCommand : IRequest<CourseStatisticResponse>
{
    public Guid CourseId { get; init; }
}