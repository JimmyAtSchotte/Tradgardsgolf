using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public record QueryAllCourses : IRequest<IEnumerable<CourseResponse>>;