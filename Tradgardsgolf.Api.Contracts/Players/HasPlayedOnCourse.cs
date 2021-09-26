using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Players
{
    public record HasPlayedOnCourse : IRequest<IEnumerable<Player>>
    {
        public int CourseId { get; init; }
    }
}