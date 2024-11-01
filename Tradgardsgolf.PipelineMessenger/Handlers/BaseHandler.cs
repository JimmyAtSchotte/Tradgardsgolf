using Tradgardsgolf.PipelineMessenger.Messaging;

namespace Tradgardsgolf.PipelineMessenger.Handlers;

public abstract class BaseHandler<TResult, TMessage, TPreviousResult> : IHandler 
    where TPreviousResult : class 
    where TMessage : class, IMessage
{
    public virtual int Score(IMessage message, HandlerResult previousResult)
    {
        return (message.IsOfType<TMessage>() ? 1 : 0) +
                (previousResult.IsOfType<TPreviousResult>() ? 1 : 0);
    }

    public HandlerResult Handle(IMessage message, HandlerResult previousResult)
    {
        if(message is not TMessage m)
            throw new InvalidOperationException();

        if (previousResult.TryGetValue<TPreviousResult>(out var previousResultValue))
        {
            var result = Handle(m, previousResultValue);
            return HandlerResult.Success(result);
        }
            
        throw new InvalidOperationException();
    }
    
    protected abstract TResult Handle(TMessage message, TPreviousResult course);
}