using FluentAssertions;
using Tradgardsgolf.Contracts;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Cqrs.Tests.Pipelines.Domain;
using Tradgardsgolf.Cqrs.Tests.Pipelines.Messages;
using Tradgardsgolf.Cqrs.Tests.Pipelines.Repository;
using Tradgardsgolf.Cqrs.Tests.Pipelines.Responses;

namespace Tradgardsgolf.Cqrs.Tests;

public class Tests
{
    [Test]
    public void RealWorld()
    {
        var repositoryPipeline = new Pipeline<BaseEntity>([new CourseById()]);
        var domainPipeline = new Pipeline<BaseEntity>([new UpdateCoursePositionHandler()]);
        var responsePipeline = new Pipeline<IResponse>([new CourseResponseHandler()]);
        
        var handler = new MessagePipeline([repositoryPipeline, domainPipeline, responsePipeline]);

        var command = new UpdateCoursePositionMessage()
        {
            CourseId = Guid.NewGuid(),
            Latitude = 12.3d,
            Longitude = 13.4d,
        };
        
        var result = handler.Handle(command);
        result.Should().BeOfType<CourseResponse>();
        result.Id.Should().Be(command.CourseId);
        result.Longitude.Should().Be(command.Longitude);
        result.Latitude.Should().Be(command.Latitude);
    }
}