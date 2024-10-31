namespace Tradgardsgolf.Cqrs;


public abstract class BaseMessage<TResult> : IMessage<TResult>
{
    public bool IsOfType<T>() => typeof(T).IsAssignableFrom(GetType());
}

public interface IMessage
{
    public bool IsOfType<T>();
}
public interface IMessage<TResult> : IMessage;
