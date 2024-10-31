using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Tradgardsgolf.Contracts;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Domain;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Repository;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Responses;

namespace Tradgardsgolf.PipelineMessenger.Tests;

public class Tests
{
    [Test]
    public void RealWorld()
    {
        var repositoryPipeline = new Pipeline<BaseEntity>([new CourseByIdHandler()]);
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
    
    [Test]
    public void ServiceProviderAddConcretePipelines()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMessagePipeline(options =>
        {
            options.AddPipeline(new Pipeline<BaseEntity>([new CourseByIdHandler()]));
            options.AddPipeline(new Pipeline<BaseEntity>([new UpdateCoursePositionHandler()]));
            options.AddPipeline(new Pipeline<IResponse>([new CourseResponseHandler()]));
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
            options.AddPipeline<BaseEntity>(typeof(CourseByIdHandler));
            options.AddPipeline<BaseEntity>(typeof(UpdateCoursePositionHandler));
            options.AddPipeline<IResponse>(typeof(CourseResponseHandler));
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
}