using Tradgardsgolf.PipelineMessenger.Messaging;

namespace Tradgardsgolf.PipelineMessenger.Handlers;

public interface IHandler
{
    int Score(IMessage message, HandlerResult previousResult);
    HandlerResult Handle(IMessage message, HandlerResult previousResult);
}
