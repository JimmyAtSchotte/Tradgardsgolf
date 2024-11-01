namespace Tradgardsgolf.PipelineMessenger;

public interface IHandler
{
    int Score(IMessage message, HandlerResult previousResult);
    HandlerResult Handle(IMessage message, HandlerResult previousResult);
}
