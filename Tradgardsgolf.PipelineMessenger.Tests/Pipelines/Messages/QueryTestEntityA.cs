
using Tradgardsgolf.PipelineMessenger.Messaging;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Responses;

namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages;

public class QueryTestEntityA :  BaseMessage<TestEntityAResponse>, IEntityByIdMessage
{
    public Guid EntityId { get; set; }
}