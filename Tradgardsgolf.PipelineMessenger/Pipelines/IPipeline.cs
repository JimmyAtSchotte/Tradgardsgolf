using Tradgardsgolf.PipelineMessenger.Messaging;

namespace Tradgardsgolf.PipelineMessenger.Pipelines;

public interface IPipeline
{
    Task<HandlerResult> HandleAsync(IMessage message, HandlerResult previousResult);
}