using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.PipelineMessenger.Handlers;

namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Responses;

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