namespace Tradgardsgolf.Cqrs;

public interface IPipeline
{
    HandlerResult Handle(IMessage message, HandlerResult previousResult);
}