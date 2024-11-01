using Tradgardsgolf.PipelineMessenger.Messaging;

namespace Tradgardsgolf.PipelineMessenger.Handlers;

public interface IHandler
{
    double Score(IMessage message, HandlerResult previousResult);
    HandlerResult Handle(IMessage message, HandlerResult previousResult);
}
