using Tradgardsgolf.PipelineMessenger.Messaging;

namespace Tradgardsgolf.PipelineMessenger.Pipelines;

public interface IPipeline
{
    HandlerResult Handle(IMessage message, HandlerResult previousResult);
}