namespace Tradgardsgolf.Cqrs.Tests;

public class HandlerStrategy
{
    private readonly IHandlerFactory[] _handlerFactories;
    
    public HandlerStrategy(IHandlerFactory[] handlerFactories)
    {
        _handlerFactories = handlerFactories;
    }

    public HandlerResult Handle(ICommand command, HandlerResult previousResult)
    {
        var handler = _handlerFactories.FirstOrDefault(x => x.AppliesTo(command, previousResult))
            ?.Create(command, previousResult);
        
        if(handler == null)
            throw new Exception($"Handler not found: {command.GetType().Name}, {previousResult.GetValueType().Name}");
        
        var obj = handler.Handle();
        
        return new HandlerResult(obj);
    }
}