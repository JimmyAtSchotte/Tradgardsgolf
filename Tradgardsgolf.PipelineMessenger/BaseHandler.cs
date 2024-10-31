namespace Tradgardsgolf.PipelineMessenger;

public abstract class BaseHandler<TOutput, TMessage, TPreviousResult> : IHandler<TOutput> 
    where TPreviousResult : class 
    where TMessage : class, IMessage
{
    public virtual int Score(IMessage message, HandlerResult previousResult)
    {
        return  (message.IsOfType<TMessage>() ? 1 : 0) +
                (previousResult.IsOfType<TPreviousResult>() ? 1 : 0);
    }

    public TOutput Handle(IMessage message, HandlerResult previousResult)
    {
        var p = previousResult.GetValue<TPreviousResult>();
        var m = message as TMessage;
        
        if(p == null)
            throw new InvalidOperationException();
        
        if(m == null)
            throw new InvalidOperationException();

        return Handle(m, p);
    }
    
    protected abstract TOutput Handle(TMessage message, TPreviousResult course);
}