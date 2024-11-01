using Tradgardsgolf.PipelineMessenger.Handlers;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Entities;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages;

namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.DomainHandlers;

public class UpdateEntityANameHandler : BaseHandler<TestEntityA, UpdateEntityANameMessage, TestEntityA>
{
    protected override Task<TestEntityA> Handle(UpdateEntityANameMessage message, TestEntityA entity)
    {
        entity.Name = message.Name;
        
        return Task.FromResult(entity);
    }
}