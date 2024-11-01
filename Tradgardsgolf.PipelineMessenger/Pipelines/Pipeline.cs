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
        var handler = _handlerFactories.FirstOrDefault(x => x.HandlerAppliesTo(message, previousResult));

        if (handler == null)
            return previousResult;
        
        return handler.Handle(message, previousResult);
    }
}