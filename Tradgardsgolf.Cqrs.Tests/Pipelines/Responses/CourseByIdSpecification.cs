using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Cqrs.Tests.Pipelines.Responses;

public class CourseResponseHandler : BasePreviousResultHandler<CourseResponse, Course>
{
    protected override CourseResponse Handle(Course course)
    {
        return new CourseResponse()
        {
            Id = course.Id,
            Longitude = course.Longitude,
            Latitude = course.Latitude,
        };
    }
}