using Tradgardsgolf.PipelineMessenger.Handlers;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Entities;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages;

namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.RepositoryHandlers;

public class EntityAByIdHandler : BaseMessageHandler<TestEntityA, IEntityByIdMessage>
{
    protected override TestEntityA Handle(IEntityByIdMessage message)
    {
        return new TestEntityA(message.EntityId);
    }
}