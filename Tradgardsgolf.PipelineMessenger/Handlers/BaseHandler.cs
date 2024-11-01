using Tradgardsgolf.PipelineMessenger.Messaging;

namespace Tradgardsgolf.PipelineMessenger.Handlers;

public abstract class BaseHandler<TResult, TMessage, TPreviousResult> : IHandler 
    where TPreviousResult : class 
    where TMessage : class, IMessage
{
    public virtual bool HandlerAppliesTo(IMessage message, HandlerResult previousResult)
    {
        return message.IsOfType<TMessage>() && previousResult.IsOfType<TPreviousResult>();
    }

    public async Task<HandlerResult> HandleAsync(IMessage message, HandlerResult previousResult)
    {
        if(message is not TMessage m)
            throw new InvalidOperationException($"{GetType().Name} expected a message of type {typeof(TMessage)}, but got {message.GetType()}");

        if (previousResult.TryGetValue<TPreviousResult>(out var previousResultValue))
        {
            var result = await Handle(m, previousResultValue);
            return HandlerResult.Success(result);
        }
            
        throw new InvalidOperationException();
    }
    
    protected abstract Task<TResult> Handle(TMessage message, TPreviousResult entity);
}