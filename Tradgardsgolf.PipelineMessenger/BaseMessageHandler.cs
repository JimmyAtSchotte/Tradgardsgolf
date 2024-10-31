namespace Tradgardsgolf.PipelineMessenger;

public abstract class BaseMessageHandler<TOutput, TMessage> : IHandler<TOutput> 
    where TMessage : class, IMessage
{
    public virtual int Score(IMessage message, HandlerResult previousResult)
    {
        return  (message.IsOfType<TMessage>() ? 1 : 0);
    }

    public TOutput Handle(IMessage message, HandlerResult previousResult)
    {
        var m = message as TMessage;
        
        if(m == null)
            throw new InvalidOperationException();

        return Handle(m);
    }
    
    protected abstract TOutput Handle(TMessage message);
}