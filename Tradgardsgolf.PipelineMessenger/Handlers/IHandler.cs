using Tradgardsgolf.PipelineMessenger.Messaging;

namespace Tradgardsgolf.PipelineMessenger.Handlers;

public interface IHandler
{
    bool HandlerAppliesTo(IMessage message, HandlerResult previousResult);
    HandlerResult Handle(IMessage message, HandlerResult previousResult);
}
