using Tradgardsgolf.PipelineMessenger.Messaging;

namespace Tradgardsgolf.PipelineMessenger.Handlers;

public abstract class BasePreviousResultHandler<TResult, TPreviousResult> : IHandler
    where TPreviousResult : class
{
    public virtual bool HandlerAppliesTo(IMessage message, HandlerResult previousResult)
    {
        if (previousResult.IsOfType<TPreviousResult>())
            return true;

        if (!message.IsResultArray() || !previousResult.IsArray()) 
            return false;

        return typeof(TPreviousResult) == previousResult.GetValueType().GetElementType();
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
    
    protected abstract TResult Handle(TPreviousResult entity);
}