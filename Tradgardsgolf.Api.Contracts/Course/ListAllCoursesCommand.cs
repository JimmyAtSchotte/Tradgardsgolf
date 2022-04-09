using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Course
{
    public record ListAllCoursesCommand : IRequest<IEnumerable<Course>>
    {
    }
}