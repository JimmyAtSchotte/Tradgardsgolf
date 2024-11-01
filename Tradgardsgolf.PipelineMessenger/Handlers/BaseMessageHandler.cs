using Tradgardsgolf.PipelineMessenger.Messaging;

namespace Tradgardsgolf.PipelineMessenger.Handlers;

public abstract class BaseMessageHandler<TResult, TMessage> : IHandler
    where TMessage : class, IMessage
{
    public virtual bool HandlerAppliesTo(IMessage message, HandlerResult previousResult)
    {
        return message.IsOfType<TMessage>();
    }

    public async Task<HandlerResult> HandleAsync(IMessage message, HandlerResult previousResult)
    {
        var m = message as TMessage;
        
        if(m == null)
            throw new InvalidOperationException();
        
        var result = await HandleAsync(m);

        return HandlerResult.Success(result);

    }
    
    protected abstract Task<TResult> HandleAsync(TMessage message);
}