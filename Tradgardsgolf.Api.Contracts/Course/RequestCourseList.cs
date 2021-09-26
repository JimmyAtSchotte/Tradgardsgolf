using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Course
{
    public class RequestCourseList : IRequest<IEnumerable<Course>>
    {
    }
}