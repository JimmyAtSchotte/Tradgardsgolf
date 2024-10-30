namespace Tradgardsgolf.Cqrs.Tests;

public class HandlerFactoryFromCToDFactory : IHandlerFactory<ClassD>
{
    public bool AppliesTo(ICommand command, HandlerResult previousResult)
    {
        return command.GetType() == typeof(CommandCToD) || previousResult.GetValueType() == typeof(CommandCToD);
    }

    public IHandler<ClassD> Create(ICommand command, HandlerResult handlerResult)
    {
        return new HandlerFactoryFromCToD();
    }
}