namespace Tradgardsgolf.Cqrs.Tests;

public class HandlerFactoryFromBToCFactory : IHandlerFactory<CommandCToD>
{
    public bool AppliesTo(ICommand command, HandlerResult previousResult)
    {
        return previousResult.GetValueType() == typeof(ClassB);
    }

    public IHandler<CommandCToD> Create(ICommand command, HandlerResult handlerResult)
    {
        return new HandlerFactoryFromBToC();
    }
}