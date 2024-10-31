using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages;

namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Domain;

public class UpdateCoursePositionHandler : BaseHandler<Course, UpdateCoursePositionMessage, Course>
{
    protected override Course Handle(UpdateCoursePositionMessage message, Course course)
    {
        course.Longitude = message.Longitude;
        course.Latitude = message.Latitude;

        return course;
    }
}