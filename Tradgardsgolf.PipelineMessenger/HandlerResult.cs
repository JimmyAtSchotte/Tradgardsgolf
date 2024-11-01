namespace Tradgardsgolf.PipelineMessenger;

public class HandlerResult
{
    private readonly object? _result;

    private HandlerResult(object? result)
    {
        _result = result;
    }
    
    public bool TryGetValue<TResult>(out TResult value) where TResult : class
    {
        value = _result as TResult;
        
        return value != null;
    }
    
    public bool TryGetArrayValue<TResult>(out TResult[] value) where TResult : class
    {
        value = _result as TResult[];
        
        return value != null;
    }
    
    public Type GetValueType()
    {
        return _result?.GetType() ?? typeof(object);
    }
    
    public bool IsOfType<T>() => IsOfType(typeof(T));
    public bool IsOfType(Type type) => _result?.GetType() == type;
    public bool IsArray() => _result is Array;

    public static HandlerResult Empty() => new HandlerResult(null);
    public static HandlerResult Success(object? obj) => new HandlerResult(obj);
}