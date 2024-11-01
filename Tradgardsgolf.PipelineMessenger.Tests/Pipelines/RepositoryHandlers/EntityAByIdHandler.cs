using Tradgardsgolf.PipelineMessenger.Handlers;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Entities;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages;

namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.RepositoryHandlers;

public class EntityAByIdHandler : BaseMessageHandler<TestEntityA, IEntityByIdMessage>
{
  
    protected override Task<TestEntityA> HandleAsync(IEntityByIdMessage message)
    {
         return Task.FromResult(new TestEntityA(message.EntityId));
    }
}