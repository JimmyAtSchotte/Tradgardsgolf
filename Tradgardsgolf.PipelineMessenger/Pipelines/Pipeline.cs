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

    public async Task<HandlerResult> HandleAsync(IMessage message, HandlerResult previousResult)
    {
        var handler = _handlerFactories.FirstOrDefault(x => x.HandlerAppliesTo(message, previousResult));

        if (handler == null)
            return previousResult;
        
        return await handler.HandleAsync(message, previousResult);
    }
}