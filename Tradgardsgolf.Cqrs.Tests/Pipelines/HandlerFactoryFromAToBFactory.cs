namespace Tradgardsgolf.Cqrs.Tests.Pipelines;

public class HandlerFactoryFromAToBFactory : IHandlerFactory<ClassB>
{
    public bool AppliesTo(ICommand command, HandlerResult previousResult)
    {
        return command.GetType() == typeof(CommandAToB) || command.GetType() == typeof(CommandAToD);
    }

    public IHandler<ClassB> Create(ICommand command, HandlerResult handlerResult)
    {
        return new HandlerFactoryFromAToB();
    }
}