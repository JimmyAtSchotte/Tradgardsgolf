namespace Tradgardsgolf.PipelineMessenger;

public class HandlerResult
{
    private readonly object _result;

    private HandlerResult(object result)
    {
        _result = result;
    }
    
    public TResult? GetValue<TResult>() where TResult : class
    {
        return _result as TResult;
    }

    public Type GetValueType()
    {
        return _result?.GetType() ?? typeof(object);
    }
    
    public bool IsOfType<T>() => _result?.GetType() == typeof(T);

    public static HandlerResult Empty() => new HandlerResult(null);
    public static HandlerResult Success(object obj) => new HandlerResult(obj);
}