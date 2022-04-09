using System.Collections.Generic;
using MediatR;
using Tradgardsgolf.Contracts.Players;

namespace Tradgardsgolf.Contracts.Statistics
{
    public record CourseStatisticCommand : IRequest<CourseStatistic>
    {
        public int CourseId { get; init; }
    }
}