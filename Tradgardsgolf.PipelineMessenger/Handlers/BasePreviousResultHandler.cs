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

    public async Task<HandlerResult> HandleAsync(IMessage message, HandlerResult previousResult)
    {
        if (previousResult.TryGetValue<TPreviousResult>(out var previousResultValue))
        {
            var result = await HandleAsync(previousResultValue);
            return HandlerResult.Success(result);
        }

        if (previousResult.TryGetArrayValue<TPreviousResult>(out var previousResultArrayValue))
        {
            var tasks = previousResultArrayValue.Select(HandleAsync).ToArray();
            await Task.WhenAll(tasks);
            return HandlerResult.Success(tasks.Select(x => x.Result).ToArray());
        }
        
        throw new InvalidOperationException();
    }
    
    protected abstract Task<TResult> HandleAsync(TPreviousResult entity);
}