namespace Tradgardsgolf.Cqrs;

public class Pipeline<TResult> : IPipeline
{
    private readonly IHandlerFactory[] _handlerFactories;
    
    public Pipeline(IHandlerFactory[] handlerFactories)
    {
        _handlerFactories = handlerFactories;
    }

    public HandlerResult Handle(ICommand command, HandlerResult previousResult)
    {
        var factory = _handlerFactories.FirstOrDefault(x => x.AppliesTo(command, previousResult));
        var generic = factory as IHandlerFactory<TResult>;
        var handler = generic?.Create(command, previousResult);
        
        if(handler == null)
            throw new NotImplementedException($"Handler not implemented: {command.GetType().Name}, {previousResult.GetValueType().Name}");
        
        var result = handler.Handle();
        
        return new HandlerResult(result);
    }
}