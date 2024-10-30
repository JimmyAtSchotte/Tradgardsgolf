namespace Tradgardsgolf.Cqrs;

public interface IPipeline
{
    HandlerResult Handle(ICommand command, HandlerResult previousResult);
}