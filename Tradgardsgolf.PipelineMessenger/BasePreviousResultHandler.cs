namespace Tradgardsgolf.PipelineMessenger;

public abstract class BasePreviousResultHandler<TOutput, TPreviousResult> : IHandler<TOutput> 
    where TPreviousResult : class
{
    public virtual int Score(IMessage message, HandlerResult previousResult)
    {
        return  (previousResult.IsOfType<TPreviousResult>() ? 1 : 0);
    }

    public TOutput Handle(IMessage message, HandlerResult previousResult)
    {
        var p = previousResult.GetValue<TPreviousResult>();
        
        if(p == null)
            throw new InvalidOperationException();

        return Handle(p);
    }
    
    protected abstract TOutput Handle(TPreviousResult course);
}