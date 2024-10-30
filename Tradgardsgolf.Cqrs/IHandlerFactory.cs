namespace Tradgardsgolf.Cqrs;


public interface IHandlerFactory
{
    bool AppliesTo(ICommand command, HandlerResult previousResult);
}

public interface IHandlerFactory<out TResult> : IHandlerFactory
{
    IHandler<TResult> Create(ICommand command, HandlerResult handlerResult);
}