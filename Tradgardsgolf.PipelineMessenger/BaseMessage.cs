namespace Tradgardsgolf.PipelineMessenger;

public abstract class BaseMessage<TResult> : IMessage<TResult>
{
    public bool IsOfType<T>() => typeof(T).IsAssignableFrom(GetType());
}