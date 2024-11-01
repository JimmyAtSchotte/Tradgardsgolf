using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Tradgardsgolf.PipelineMessenger.DependencyInjection;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.DomainHandlers;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Entities;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.RepositoryHandlers;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.ResponseHandlers;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Responses;

namespace Tradgardsgolf.PipelineMessenger.Tests;

public class Tests
{
    private MessagePipeline _messagePipeline;
    
    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMessagePipeline(builder =>
        {
            builder.AddPipeline(typeof(EntityAByIdHandler), typeof(QueryAllEntityAHandler));
            builder.AddPipeline(typeof(UpdateEntityANameHandler));
            builder.AddPipeline(typeof(SingleTestEntityAResponseHandler), typeof(MultiTestEntityAResponseHandler));
        });
        
        var provider = serviceCollection.BuildServiceProvider();
        _messagePipeline = provider.GetRequiredService<MessagePipeline>();
    }
    
    
    [Test]
    public void ShouldMutateEntityInPipeline()
    {
        var command = new UpdateEntityANameMessage()
        {
            EntityId = Guid.NewGuid(),
            Name = "Test"
        };
        
        var result = _messagePipeline.Handle(command);
        result.Should().BeOfType<TestEntityAResponse>();
        result.Id.Should().Be(command.EntityId);
        result.Name.Should().Be(command.Name);
    }
    
    [Test]
    public void ShouldSkipPipelinesWithNoHandlers()
    {
        var command = new QueryTestEntityA()
        {
            EntityId = Guid.NewGuid()
        };
        
        var result = _messagePipeline.Handle(command);
        result.Should().BeOfType<TestEntityAResponse>();
    }
    
    [Test]
    public void ShouldInvokeMatchingHandlerMultipleTimesToCreateAnArray()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMessagePipeline(options =>
        {
            options.AddPipeline(typeof(QueryAllEntityAHandler));
            options.AddPipeline(typeof(SingleTestEntityAResponseHandler));
        });
        
        var provider = serviceCollection.BuildServiceProvider();
        var messagePipeline = provider.GetRequiredService<MessagePipeline>();
        
        var command = new QueryAllTestEntityA()
        {
            
        };
        
        var result = messagePipeline.Handle(command);
        result.Should().BeOfType<TestEntityAResponse[]>();
    }

    [Test]
    public void ShouldApplyToHandlerWhenResultIsAnArrayButExpectsSingleResult()
    {
        var handler = new SingleTestEntityAResponseHandler();
        
       handler
           .HandlerAppliesTo(new QueryAllTestEntityA(), HandlerResult.Success(new[] { new TestEntityA() }))
           .Should().BeTrue();
    }
}