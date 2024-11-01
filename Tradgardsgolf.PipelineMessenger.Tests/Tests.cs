using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Tradgardsgolf.Contracts;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.PipelineMessenger.DependencyInjection;
using Tradgardsgolf.PipelineMessenger.Pipelines;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Domain;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Repository;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Responses;
using QueryAllCourses = Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages.QueryAllCourses;

namespace Tradgardsgolf.PipelineMessenger.Tests;

public class Tests
{
    [Test]
    public void RealWorld()
    {
        var repositoryPipeline = new Pipeline([new CourseByIdHandler()]);
        var domainPipeline = new Pipeline([new UpdateCoursePositionHandler()]);
        var responsePipeline = new Pipeline([new CourseResponseHandler()]);
        
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
    
    [Test]
    public void ServiceProviderAddConcretePipelines()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMessagePipeline(options =>
        {
            options.AddPipeline(new Pipeline([new CourseByIdHandler()]));
            options.AddPipeline(new Pipeline([new UpdateCoursePositionHandler()]));
            options.AddPipeline(new Pipeline([new CourseResponseHandler()]));
        });
        
        var provider = serviceCollection.BuildServiceProvider();
        var messagePipeline = provider.GetRequiredService<MessagePipeline>();
        
        var command = new UpdateCoursePositionMessage()
        {
            CourseId = Guid.NewGuid(),
            Latitude = 12.3d,
            Longitude = 13.4d,
        };
        
        
        var result = messagePipeline.Handle(command);
        result.Should().BeOfType<CourseResponse>();
        result.Id.Should().Be(command.CourseId);
        result.Longitude.Should().Be(command.Longitude);
        result.Latitude.Should().Be(command.Latitude);
    }
    
    [Test]
    public void ServiceProviderAddPipelinesFromTypes()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<CourseByIdHandler>();
        serviceCollection.AddTransient<UpdateCoursePositionHandler>();
        serviceCollection.AddTransient<CourseResponseHandler>();
        
        serviceCollection.AddMessagePipeline(options =>
        {
            options.AddPipeline(typeof(CourseByIdHandler), typeof(QueryAllCourses));
            options.AddPipeline(typeof(UpdateCoursePositionHandler));
            options.AddPipeline(typeof(CourseResponseHandler));
        });
        
        var provider = serviceCollection.BuildServiceProvider();
        var messagePipeline = provider.GetRequiredService<MessagePipeline>();
        
        var command = new UpdateCoursePositionMessage()
        {
            CourseId = Guid.NewGuid(),
            Latitude = 12.3d,
            Longitude = 13.4d,
        };
        
        var result = messagePipeline.Handle(command);
        result.Should().BeOfType<CourseResponse>();
        result.Id.Should().Be(command.CourseId);
        result.Longitude.Should().Be(command.Longitude);
        result.Latitude.Should().Be(command.Latitude);
    }
    
    [Test]
    public void ShouldHandleArrayResponses()
    {
        var repositoryPipeline = new Pipeline([new QueryAllCoursesHandler()]);
        var responsePipeline = new Pipeline([new CourseResponseHandler()]);
        
        var handler = new MessagePipeline([repositoryPipeline, responsePipeline]);

        var command = new QueryAllCourses()
        {
        };
        
        var result = handler.Handle(command);
        result.Should().BeOfType<CourseResponse[]>();
    }

    [Test]
    public void ShouldScoreWhenResponseIsArrayAndPreviousResultIsArray()
    {
        var handler = new CourseResponseHandler();
        var score = handler.Score(new QueryAllCourses(), HandlerResult.Success(new[] { Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid()) }));
        score.Should().Be(1);
    }
}