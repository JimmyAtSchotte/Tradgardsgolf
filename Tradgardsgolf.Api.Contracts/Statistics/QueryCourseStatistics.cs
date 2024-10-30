using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Statistics;

public record QueryCourseStatistics : IRequest<CourseStatisticResponse>
{
    public Guid CourseId { get; init; }
}