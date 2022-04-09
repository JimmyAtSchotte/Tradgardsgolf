using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Course
{
    public record RequestCourseList : IRequest<IEnumerable<Course>>
    {
    }
}