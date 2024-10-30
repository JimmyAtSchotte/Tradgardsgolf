using Ardalis.Specification;
using FluentAssertions;
using Tradgardsgolf.Contracts;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Cqrs.Tests.Pipelines;
using Tradgardsgolf.Cqrs.Tests.Pipelines.Commands;
using Tradgardsgolf.Cqrs.Tests.Pipelines.Repository;
using Tradgardsgolf.Cqrs.Tests.Pipelines.Responses;
using Tradgardsgolf.Cqrs.Tests.Pipelines.Specifications;

namespace Tradgardsgolf.Cqrs.Tests;

public class Tests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public void ConvertFromAToB()
    {
        var handler = new CqrsPipeline([new Pipeline<ClassB>([new HandlerFactoryFromAToBFactory()])]);
        var result = handler.Handle(new CommandAToB());
        result.Should().BeOfType<ClassB>();
    }
    
    [Test]
    public void ConvertFromCToD()
    {
        var handler = new CqrsPipeline([new Pipeline<ClassD>([new HandlerFactoryFromCToDFactory()])]);
        var result = handler.Handle(new CommandCToD());
        result.Should().BeOfType<ClassD>();
    }
    
    [Test]
    public void ConvertFromAToD()
    {
        var stage1 = new Pipeline<BaseEntity>([new HandlerFactoryFromAToBFactory(), new HandlerFactoryFromCToDFactory()]);
        var stage2 = new Pipeline<CommandCToD>([new HandlerFactoryFromBToCFactory()]);
        var stage3 = new Pipeline<BaseEntity>([new HandlerFactoryFromCToDFactory()]);
        
        var handler = new CqrsPipeline([stage1, stage2, stage3]);
        var result = handler.Handle(new CommandAToD());
        result.Should().BeOfType<ClassD>();
    }
    
    
    
    [Test]
    public void RealWorld()
    {
        var repositoryPipeline = new Pipeline<BaseEntity>([new FetchCourseByIdFactory()]);
        var domainPipeline = new Pipeline<BaseEntity>([new UpdateCoursePositionFactory()]);
        var responsePipeline = new Pipeline<IResponse>([new CourseResponseFactory()]);
        
        var handler = new CqrsPipeline([repositoryPipeline, domainPipeline, responsePipeline]);

        var command = new UpdateCoursePositionCommand()
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