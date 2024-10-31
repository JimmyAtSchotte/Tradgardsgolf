namespace Tradgardsgolf.PipelineMessenger;

public class Pipeline<TResult> : IPipeline
{
    private readonly IHandler<TResult>[] _handlerFactories;
    
    public Pipeline(IHandler<TResult>[] handlerFactories)
    {
        _handlerFactories = handlerFactories;
    }

    public HandlerResult Handle(IMessage message, HandlerResult previousResult)
    {
        var handler = _handlerFactories
            .Select(x => new { Score = x.Score(message, previousResult), Handler = x })
            .Where(x => x.Score > 0)
            .OrderByDescending(x => x.Score)
            .Select(x => x.Handler)
            .FirstOrDefault();
        
        if(handler == null)
            throw new NotImplementedException($"Handler not implemented: {message.GetType().Name}, {previousResult.GetValueType().Name}");
        
        var result = handler.Handle(message, previousResult);
        
        return HandlerResult.Success(result);
    }
}