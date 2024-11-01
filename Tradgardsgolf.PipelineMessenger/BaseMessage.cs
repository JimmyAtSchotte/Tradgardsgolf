namespace Tradgardsgolf.PipelineMessenger;

public abstract class BaseMessage<TResult> : IMessage<TResult>
{
    public bool IsOfType<T>() => IsOfType(typeof(T));
    public bool IsOfType(Type type) => type.IsAssignableFrom(GetType());
}