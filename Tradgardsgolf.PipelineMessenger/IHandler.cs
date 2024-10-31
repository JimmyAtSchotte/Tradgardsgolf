namespace Tradgardsgolf.PipelineMessenger;

public interface IHandler<out TResult>
{
    int Score(IMessage message, HandlerResult previousResult);
    TResult Handle(IMessage message, HandlerResult previousResult);
}