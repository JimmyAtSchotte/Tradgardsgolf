using Tradgardsgolf.Contracts.Course;

namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages;

public interface ICourseByIdMessage : IMessage
{
    Guid CourseId { get; }
}

public class UpdateCoursePositionMessage :  BaseMessage<CourseResponse>, ICourseByIdMessage
{
    public Guid CourseId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}