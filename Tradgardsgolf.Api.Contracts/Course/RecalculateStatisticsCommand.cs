using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public class RecalculateStatisticsCommand : IRequest<Unit>
{
    public Guid CourseId { get; init; }
}