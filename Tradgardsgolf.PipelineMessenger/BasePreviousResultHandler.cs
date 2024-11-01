namespace Tradgardsgolf.PipelineMessenger;

public abstract class BasePreviousResultHandler<TResult, TPreviousResult> : IHandler
    where TPreviousResult : class
{
    public virtual int Score(IMessage message, HandlerResult previousResult)
    {
        var score = (previousResult.IsOfType<TPreviousResult>() ? 1 : 0);
        
        if (score > 0)
            return score;

        var iMessageInterface = message.GetType()
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMessage<>));

        if (iMessageInterface == null)
            return score;
        
        var resultType = iMessageInterface.GetGenericArguments()[0];
        if (!resultType.IsArray || !previousResult.IsArray()) 
            return score;
        
        score = typeof(TPreviousResult) == previousResult.GetValueType().GetElementType()  ? 1 : 0;

        return score;
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