namespace Tradgardsgolf.PipelineMessenger;

public interface IPipeline
{
    HandlerResult Handle(IMessage message, HandlerResult previousResult);
}