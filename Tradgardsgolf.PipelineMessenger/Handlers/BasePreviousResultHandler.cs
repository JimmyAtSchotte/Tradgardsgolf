using Tradgardsgolf.PipelineMessenger.Messaging;

namespace Tradgardsgolf.PipelineMessenger.Handlers;

public abstract class BasePreviousResultHandler<TResult, TPreviousResult> : IHandler
    where TPreviousResult : class
{
    public virtual double Score(IMessage message, HandlerResult previousResult)
    {
        var score = (previousResult.IsOfType<TPreviousResult>() ? 1 : 0);
        
        if (score > 0)
            return score;

        if (!message.IsResultArray() || !previousResult.IsArray()) 
            return score;
        
        return typeof(TPreviousResult) == previousResult.GetValueType().GetElementType() ? 0.5 : 0;
    }

    public HandlerResult Handle(IMessage message, HandlerResult previousResult)
    {
        if (previousResult.TryGetValue<TPreviousResult>(out var previousResultValue))
        {
            var result = Handle(previousResultValue);
            return HandlerResult.Success(result);
        }

        if (previousResult.TryGetArrayValue<TPreviousResult>(out var previousResultArrayValue))
        {
            var result = previousResultArrayValue.Select(Handle).ToArray();
            return HandlerResult.Success(result);
        }
        
        throw new InvalidOperationException();
    }
    
    protected abstract TResult Handle(TPreviousResult course);
}