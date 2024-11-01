using Tradgardsgolf.PipelineMessenger.Handlers;
using Tradgardsgolf.PipelineMessenger.Messaging;

namespace Tradgardsgolf.PipelineMessenger.Pipelines;

public class Pipeline : IPipeline
{
    private readonly IHandler[] _handlerFactories;
    
    public Pipeline(IHandler[] handlerFactories)
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
        
        return handler.Handle(message, previousResult);
    }
}